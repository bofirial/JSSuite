﻿using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.JSSuite.Generated

//************************
// GENERATED CLASS
// DO NOT EDIT THIS FILE
//************************
{
    /// <summary>
    /// Generated CollectionItem Business Manager
    /// </summary>
    /// <typeparam name="TBusinessManager">The type of the business manager.</typeparam>
    public class CollectionItemBusinessManager_Generated<TBusinessManager> : SingletonBase<TBusinessManager> where TBusinessManager : class
    {
        ///// <summary>
        ///// Select
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="filter">The filter.</param>
        ///// <returns></returns>
        //public List<CollectionItem> Select(IConnectionInfo connectionInfo, CollectionItem filter)
        //{
        //    return CollectionItemManager.Current.Select(connectionInfo, filter);
        //}

        /// <summary>
        /// Select Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<CollectionItem>> SelectAsync(IConnectionInfo connectionInfo, CollectionItem filter)
        {
            return await CollectionItemManager.Current.SelectAsync(connectionInfo, filter);
        }

        ///// <summary>
        ///// Insert
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Insert(IConnectionInfo connectionInfo, CollectionItem entity)
        //{
        //    return CollectionItemManager.Current.Insert(connectionInfo, entity);
        //}

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> InsertAsync(IConnectionInfo connectionInfo, CollectionItem entity)
        {
            return await CollectionItemManager.Current.InsertAsync(connectionInfo, entity);
        }

        ///// <summary>
        ///// Update
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Update(IConnectionInfo connectionInfo, CollectionItem entity)
        //{
        //    return CollectionItemManager.Current.Update(connectionInfo, entity);
        //}

        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> UpdateAsync(IConnectionInfo connectionInfo, CollectionItem entity)
        {
            return await CollectionItemManager.Current.UpdateAsync(connectionInfo, entity);
        }

        ///// <summary>
        ///// Delete
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Delete(IConnectionInfo connectionInfo, CollectionItem entity)
        //{
        //    return CollectionItemManager.Current.Delete(connectionInfo, entity);
        //}

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> DeleteAsync(IConnectionInfo connectionInfo, CollectionItem entity)
        {
            return await CollectionItemManager.Current.DeleteAsync(connectionInfo, entity);
        }
    }
}
