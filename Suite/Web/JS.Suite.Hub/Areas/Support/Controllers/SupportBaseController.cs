using JS.Core.Web;
using JS.Suite.BusinessLogic.Web.Mvc.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Support.Controllers
{
    /// <summary>
    /// Base Controller for the Support Area
    /// </summary>
    [ApplicationAuthorize("Support")]
    [SetCurrentApplication("Support")]
    public class SupportBaseController : BaseController
    {
    }
}