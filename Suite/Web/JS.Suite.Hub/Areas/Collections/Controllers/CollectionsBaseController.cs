using JS.Core.Web;
using JS.Suite.BusinessLogic.Web.Mvc.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Collections.Controllers
{
    /// <summary>
    /// Collections Base Controller
    /// </summary>
    [ApplicationAuthorize("Collections")]
    [SetCurrentApplication("Collections")]
    public class CollectionsBaseController : BaseController
    {
    }
}