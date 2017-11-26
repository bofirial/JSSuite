using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Web.Mvc.ActionFilters;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.BusinessLogic.Resource;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Collections.Controllers
{
    /// <summary>
    /// Collections Controller
    /// </summary>
    [CurrentMenuItem(SuiteMenuItems.MyCollections)]
    public class CollectionGroupController : CollectionsBaseController
    {
        /// <summary>
        /// List
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            CollectionGroup model = new CollectionGroup();

            return View(model);
        }

        /// <summary>
        /// Mies the collections grid data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<JsonResult>  MyCollectionsGridData(CollectionGroup filter)
        {
            filter.ClearEmptyModifiedColumns();

            filter.JSUserId = SecurityManager.Current.UserId;

            PagedResult<CollectionGroup> pagedResult = await CollectionGroupBusinessManager.Current.PagingSelectAsync(SecurityManager.Current.ConnectionInfo, filter);

            return Json(pagedResult);
        }

        /// <summary>
        /// Adds the edit modal.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AddEditModal(int? collectionGroupId = null)
        {
            CollectionGroup model = new CollectionGroup();

            if (collectionGroupId != null)
            {
                model = (await CollectionGroupBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, 
                    new CollectionGroup() { CollectionGroupId = collectionGroupId ?? -1 })).FirstOrDefault();

                int collectionItems = (await CollectionItemBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                    new CollectionItem() { CollectionGroupId = collectionGroupId ?? -1 })).Count;

                if (collectionItems < 1)
                {
                    ViewBag.AllowDelete = true;
                }

            }

            return View(model);
        }

        /// <summary>
        /// Adds the edit modal.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddEditModal(CollectionGroup model)
        {
            if (ModelState.IsValid)
            {
                if (model.CollectionGroupId != default(int))
                {
                    await CollectionGroupBusinessManager.Current.UpdateAsync(SecurityManager.Current.ConnectionInfo, model); 
                }
                else
                {
                    await CollectionGroupBusinessManager.Current.InsertAsyncWithCollectionGroupJSUser(SecurityManager.Current.ConnectionInfo, model);
                }

                ViewBag.StatusMessage = Localization.SavedSuccessfully;

                ModelState.Clear();
            }

            return View(model);
        }

        /// <summary>
        /// Collection.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> Collection(int id)
        {
            CollectionGroup model = (await CollectionGroupBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new CollectionGroup() {
                CollectionGroupId = id
            })).FirstOrDefault();

            return View(model);
        }

        /// <summary>
        /// Deletes the specified collection group identifier.
        /// </summary>
        /// <param name="collectionGroupId">The collection group identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int collectionGroupId)
        {
            if (ModelState.IsValid)
            {
                List<CollectionGroupJSUser> collectionGroupJSUsers = await CollectionGroupJSUserBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new CollectionGroupJSUser()
                {
                    CollectionGroupId = collectionGroupId
                });

                IProcessResult result = new ProcessResult(ResultCodes.Success);

                foreach (var collectionGroupJSUser in collectionGroupJSUsers)
	            {
                    result = await CollectionGroupJSUserBusinessManager.Current.DeleteAsync(SecurityManager.Current.ConnectionInfo, new CollectionGroupJSUser()
                    {
                        CollectionGroupJSUserId = collectionGroupJSUser.CollectionGroupJSUserId
                    });

                    if (!result.IsSuccess())
                    {
                        break;
                    }
	            }

                if (result.IsSuccess())
                {
                    result = await CollectionGroupBusinessManager.Current.DeleteAsync(SecurityManager.Current.ConnectionInfo, new CollectionGroup()
                    {
                        CollectionGroupId = collectionGroupId
                    });

                    if (result.IsSuccess())
                    {
                        return Json(new { Success = true });
                    }
                }
            }

            return await AddEditModal(collectionGroupId);
        }
    }
}