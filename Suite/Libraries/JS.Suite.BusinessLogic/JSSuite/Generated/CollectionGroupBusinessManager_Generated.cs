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
    /// Generated CollectionGroup Business Manager
    /// </summary>
    /// <typeparam name="TBusinessManager">The type of the business manager.</typeparam>
    public class CollectionGroupBusinessManager_Generated<TBusinessManager> : SingletonBase<TBusinessManager> where TBusinessManager : class
    {
        ///// <summary>
        ///// Select
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="filter">The filter.</param>
        ///// <returns></returns>
        //public List<CollectionGroup> Select(IConnectionInfo connectionInfo, CollectionGroup filter)
        //{
        //    return CollectionGroupManager.Current.Select(connectionInfo, filter);
        //}

        /// <summary>
        /// Select Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<CollectionGroup>> SelectAsync(IConnectionInfo connectionInfo, CollectionGroup filter)
        {
            return await CollectionGroupManager.Current.SelectAsync(connectionInfo, filter);
        }

        ///// <summary>
        ///// Insert
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Insert(IConnectionInfo connectionInfo, CollectionGroup entity)
        //{
        //    return CollectionGroupManager.Current.Insert(connectionInfo, entity);
        //}

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> InsertAsync(IConnectionInfo connectionInfo, CollectionGroup entity)
        {
            return await CollectionGroupManager.Current.InsertAsync(connectionInfo, entity);
        }

        ///// <summary>
        ///// Update
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Update(IConnectionInfo connectionInfo, CollectionGroup entity)
        //{
        //    return CollectionGroupManager.Current.Update(connectionInfo, entity);
        //}

        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> UpdateAsync(IConnectionInfo connectionInfo, CollectionGroup entity)
        {
            return await CollectionGroupManager.Current.UpdateAsync(connectionInfo, entity);
        }

        ///// <summary>
        ///// Delete
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="entity">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Delete(IConnectionInfo connectionInfo, CollectionGroup entity)
        //{
        //    return CollectionGroupManager.Current.Delete(connectionInfo, entity);
        //}

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> DeleteAsync(IConnectionInfo connectionInfo, CollectionGroup entity)
        {
            return await CollectionGroupManager.Current.DeleteAsync(connectionInfo, entity);
        }
    }
}