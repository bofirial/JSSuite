using JS.Core.Web;
using JS.Suite.BusinessLogic.Web.Mvc.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Games.Controllers
{
    /// <summary>
    /// Games Base Controller
    /// </summary>
    [ApplicationAuthorize("Games")]
    [SetCurrentApplication("Games")]
    public class GamesBaseController : BaseController
    {
    }
}