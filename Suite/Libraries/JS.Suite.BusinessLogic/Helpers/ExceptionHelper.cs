using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.ErrorHandling;
using JS.Suite.BusinessLogic.Messaging;
using JS.Suite.BusinessLogic.Resource;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Helpers
{
    /// <summary>
    /// Exception Helper
    /// </summary>
    public class ExceptionHelper : SingletonBase<ExceptionHelper>
    {
        /// <summary>
        /// Gets the HTML message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public MvcHtmlString GetHtmlMessage(Exception exception)
        {
            TagBuilder exceptionMessage = new TagBuilder("p");

            exceptionMessage.AddCssClass("exceptionMessage");

            StringBuilder sb = new StringBuilder();

            if (exception != null)
            {
                AddExceptionToStringBuilder(exception, sb, Localization.Exception);

                //Not Recursive on Purpose to Avoid and Infinite Loop
                if (exception.InnerException != null)
                {
                    AddExceptionToStringBuilder(exception.InnerException, sb, Localization.InnerException);

                    if (exception.InnerException.InnerException != null)
                    {
                        AddExceptionToStringBuilder(exception.InnerException.InnerException, sb, Localization.InnerInnerException);

                        if (exception.InnerException.InnerException.InnerException != null)
                        {
                            AddExceptionToStringBuilder(exception.InnerException.InnerException.InnerException, sb, Localization.InnerInnerInnerException);
                        } 
                    } 
                } 
            }

            exceptionMessage.InnerHtml = sb.ToString();

            return new MvcHtmlString(exceptionMessage.ToString());
        }

        private static void AddExceptionToStringBuilder(Exception exception, StringBuilder sb, string label)
        {
            string format = "<b>{0}: </b>{1}<br />";

            if (exception != null)
            {
                if (exception is CoreException)
                {
                    sb.AppendFormat(format, String.Format(Localization._Label, label), ((CoreException)exception).Label);
                }

                sb.AppendFormat(format, label, exception.Message);
            }
        }

        /// <summary>
        /// Sends the application message for exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void SendAppMessageForException(Exception exception)
        {
            MessageTypes messageType = MessageTypes.ApplicationError;

            if (exception is SQLException)
            {
                messageType = MessageTypes.DatabaseError;
            }

            Task.WhenAll(AppMessenger.Current.Send(messageType,
                ExceptionHelper.Current.GetHtmlMessage(exception).ToString(),
                exception is CoreException ? ((CoreException)exception).Label : exception.GetType().Name,
                TraceLevels.Error,
                exception.StackTrace));
        }
    }
}
