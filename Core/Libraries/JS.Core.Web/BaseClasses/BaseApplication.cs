using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Core.Web
{
    /// <summary>
    /// Base Application
    /// </summary>
    public abstract class BaseApplication : HttpApplication
    {
        /// <summary>
        /// Registers the data annotations model validation adapters.
        /// </summary>
        protected virtual void RegisterDataAnnotationsModelValidationAdapters()
        {
            List<Type> regularExpressionAttributes = new List<Type>()
            {
                typeof(DBStartsWithAttribute),
                typeof(DBEndsWithAttribute),
                typeof(DBContainsAttribute)
            };

            foreach (Type type in regularExpressionAttributes)
            {
                DataAnnotationsModelValidatorProvider.RegisterAdapter(
                    type,
                    typeof(RegularExpressionAttributeAdapter)
                ); 
            }
        }

        /// <summary>
        /// Application Start
        /// </summary>
        protected virtual void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterDataAnnotationsModelValidationAdapters();
        }
    }
}
