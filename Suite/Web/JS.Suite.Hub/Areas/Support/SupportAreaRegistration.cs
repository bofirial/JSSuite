using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Support
{
    /// <summary>
    /// Support Area Registration
    /// </summary>
    public class SupportAreaRegistration : AreaRegistration 
    {
        /// <summary>
        /// Gets the name of the area to register.
        /// </summary>
        /// <returns>The name of the area to register.</returns>
        public override string AreaName 
        {
            get 
            {
                return "Support";
            }
        }

        /// <summary>
        /// Registers an area in an ASP.NET MVC application using the specified area's context information.
        /// </summary>
        /// <param name="context">Encapsulates the information that is required in order to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Support_default",
                "Support/{controller}/{action}/{id}",
                new { controller = "Support", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}