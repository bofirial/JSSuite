using JS.Core.Foundation.Data;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.BusinessLogic.Resource;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Collections.Controllers
{
    /// <summary>
    /// Collection Item Controller
    /// </summary>
    public class CollectionItemController : Controller
    {
        /// <summary>
        /// Mies the collection items grid data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<JsonResult> MyCollectionItemsGridData(CollectionItem filter)
        {
            filter.ClearEmptyModifiedColumns();

            PagedResult<CollectionItem> pagedResult = await CollectionItemBusinessManager.Current.PagingSelectAsync(SecurityManager.Current.ConnectionInfo, filter);

            return Json(pagedResult);
        }
        
        /// <summary>
        /// Adds the edit modal.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> AddEditModal(int collectionGroupId, int? collectionItemId = null)
        {
            CollectionItem model = new CollectionItem();

            if (collectionItemId != null)
            {
                model = (await CollectionItemBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                    new CollectionItem() { CollectionItemId = collectionItemId ?? -1 })).FirstOrDefault();

            }
            else
            {
                model = new CollectionItem();
            }

            model.CollectionGroup = (await CollectionGroupBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new CollectionGroup() { CollectionGroupId = collectionGroupId })).FirstOrDefault();

            model.CollectionGroupId = collectionGroupId;

            return View(model);
        }

        /// <summary>
        /// Adds the edit modal.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddEditModal(CollectionItem model)
        {
            if (ModelState.IsValid)
            {
                if (model.CollectionItemId != default(int))
                {
                    await CollectionItemBusinessManager.Current.UpdateAsync(SecurityManager.Current.ConnectionInfo, model);
                }
                else
                {
                    await CollectionItemBusinessManager.Current.InsertAsync(SecurityManager.Current.ConnectionInfo, model);
                }

                ViewBag.StatusMessage = Localization.SavedSuccessfully;

                ModelState.Clear();
            }

            model.CollectionGroup = (await CollectionGroupBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new CollectionGroup() { CollectionGroupId = model.CollectionGroupId })).FirstOrDefault();

            return View(model);
        }

        /// <summary>
        /// Deletes the specified collection item.
        /// </summary>
        /// <param name="collectionItemId">The collection item identifier.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Delete(int collectionItemId)
        {
            if (ModelState.IsValid)
            {
                IProcessResult result = await CollectionItemBusinessManager.Current.DeleteAsync(SecurityManager.Current.ConnectionInfo, new CollectionItem()
                {
                    CollectionItemId = collectionItemId
                });

                if (result.IsSuccess())
                {
                    return Json(new { Success = true });
                }
            }

            CollectionItem model = (await CollectionItemBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new CollectionItem() { CollectionItemId = collectionItemId })).FirstOrDefault();

            return await AddEditModal(model.CollectionGroupId, collectionItemId);
        }
    }
}