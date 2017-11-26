using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.DTO.FreeGeoIp;
using JS.Suite.BusinessLogic.Helpers;
using JS.Suite.BusinessLogic.JSSupport;
using JS.Suite.BusinessLogic.Messaging;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.BusinessLogic.Web.Cookies;
using JS.Suite.DataAbstraction.JSSupport;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UAParser;

namespace JS.Suite.BusinessLogic.Web.Mvc.ActionFilters
{
    /// <summary>
    /// Action Filter for Logging to the Traffic Log
    /// </summary>
    public class TrafficLogAttribute : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ClientInfo clientInfo = Parser.GetDefault().Parse(HttpContext.Current.Request.UserAgent);

            JSApplicationCookie cookie = GetApplicationCookie();

            TrafficLogRequest trafficLogRequest = new TrafficLogRequest() 
            {
                ApplicationId = (int) SecurityManager.Current.CurrentApplication,
                Browser = HttpContext.Current.Request.Browser.Browser,
                BrowserVersion = clientInfo.UserAgent.ToString(),
                Cookie = cookie.Serialize(SerializationTypes.Json),
                JSUserId = SecurityManager.Current.UserId,
                OperatingSystem = clientInfo.OS.ToString(),
                Device = clientInfo.Device.ToString(),
                RequestedUrl = HttpContext.Current.Request.RawUrl,
                RequestType = HttpContext.Current.Request.RequestType,
                TrackingGuid = SecurityManager.Current.TrackingGuid,
                TrafficLogGuid = cookie.TrafficLogGuid,
                UserAgent = HttpContext.Current.Request.UserAgent,
                UrlReferrer = HttpContext.Current.Request.UrlReferrer == null ? null : HttpContext.Current.Request.UrlReferrer.OriginalString
            };

            string seperator = "";

            foreach (string key in HttpContext.Current.Request.Form)
            {
                trafficLogRequest.PostedData += String.Format("{0}{1}={2}", seperator, key, HttpContext.Current.Request.Form[key]);

                seperator = "&";
            }

            IConnectionInfo connectionInfo = SecurityManager.Current.ConnectionInfo;

            string ipAddress = HttpContext.Current.Request.UserHostAddress;

            BackgroundProcessHelper.Current.Trigger(async (cancel) =>
            {
                await SetTrafficLogRequestLocation(trafficLogRequest, ipAddress);

                await TrafficLogRequestBusinessManager.Current.InsertAsync(connectionInfo, trafficLogRequest);
            });

            base.OnActionExecuting(filterContext);
        }

        private static async Task SetTrafficLogRequestLocation(TrafficLogRequest trafficLogRequest, string ipAddress)
        {
            const string homeNetworkIp = "192.168.1.1";

            if (!String.IsNullOrEmpty(ipAddress) && ipAddress != "::1" && ipAddress != homeNetworkIp)
            {
                WebRequest request = WebRequest.Create(String.Format("{0}{1}", ConfigurationHelper.Current.GetSetting("IPLocationService"), ipAddress));

                WebResponse response = await request.GetResponseAsync();

                Location location = Location.Deserialize(new StreamReader(response.GetResponseStream()).ReadToEnd(), SerializationTypes.Json);

                trafficLogRequest.Location = String.Join(" ", location.CountryName, location.RegionName, location.City, location.ZipCode);
            }
            else if (!String.IsNullOrEmpty(ipAddress))
            {
                trafficLogRequest.Location = "House Wifi Dayton Ohio";
            }
        }

        private JSApplicationCookie GetApplicationCookie()
        {
            JSApplicationCookie cookie = null;

            if (HttpContext.Current.Request.Cookies[JSCookies.ApplicationCookie] != null)
            {
                try
                {
                    cookie = JSApplicationCookie.Deserialize(HttpContext.Current.Request.Cookies[JSCookies.ApplicationCookie].Value, SerializationTypes.Json);
                }
                catch (Exception e)
                {
                    Task.WhenAll(AppMessenger.Current.Send(MessageTypes.FailedCookieDeserialization,
                        String.Format("{0}: {1}. /r/nCookie Value: {2}", e.GetType().Name, e.Message, HttpContext.Current.Request.Cookies[JSCookies.ApplicationCookie].Value),
                        "Failed to Deserialize the JS Application Cookie",
                        TraceLevels.Error,
                        e.StackTrace));
                }
                
                if (cookie != null && !String.IsNullOrEmpty(cookie.TrafficLogGuid))
                {
                    return cookie;
                }
                else
                {
                    HttpContext.Current.Response.Cookies.Remove(JSCookies.ApplicationCookie);
                }
            }

            cookie = new JSApplicationCookie()
            {
                TrafficLogGuid = Guid.NewGuid().ToString()
            };

            HttpContext.Current.Response.Cookies.Add(new HttpCookie(JSCookies.ApplicationCookie, cookie.Serialize(SerializationTypes.Json)));

            return cookie;
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework after the action result executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            LogResponse();
        }

        private static void LogResponse()
        {
            TrafficLogResponse trafficLogResponse = new TrafficLogResponse()
            {
                TrackingGuid = SecurityManager.Current.TrackingGuid,
                ResponseCode = String.Join(".", HttpContext.Current.Response.StatusCode, HttpContext.Current.Response.SubStatusCode),
                ResponseCodeDescription = HttpContext.Current.Response.StatusDescription,
                RedirectLocation = HttpContext.Current.Response.RedirectLocation
            };

            IConnectionInfo connectionInfo = SecurityManager.Current.ConnectionInfo;

            BackgroundProcessHelper.Current.Trigger(async (cancel) =>
            {
                await TrafficLogResponseBusinessManager.Current.InsertAsync(connectionInfo, trafficLogResponse);
            });
        }

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            LogResponse();
        }
    }
}
