﻿using JS.Core.Foundation.Data;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//************************
// GENERATED CLASS
// DO NOT EDIT THIS FILE
//************************

namespace JS.Suite.DataAbstraction.JSSupport.Generated
{
    /// <summary>
    /// Generated ApplicationLog Manager
    /// </summary>
    public class ApplicationLogManager_Generated<TManager> : EntityManagerBase<TManager> where TManager : class
    {
        /// <summary>
        /// Database Name
        /// </summary>
        public override string DatabaseName
        {
            get
            {
                return Databases.JSSupport;
            }
        }

        ///// <summary>
        ///// Select
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="filter">The filter.</param>
        ///// <returns></returns>
        //public List<ApplicationLog> Select(IConnectionInfo connectionInfo, ApplicationLog filter)
        //{
        //    return SelectInternal(connectionInfo, filter).Result;
        //}

        /// <summary>
        /// Select Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<ApplicationLog>> SelectAsync(IConnectionInfo connectionInfo, ApplicationLog filter)
        {
            return await SelectInternal(connectionInfo, filter);
        }

        ///// <summary>
        ///// Insert
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="ApplicationLog">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Insert(IConnectionInfo connectionInfo, ApplicationLog entity)
        //{
        //    return InsertInternal(connectionInfo, entity).Result;
        //}

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> InsertAsync(IConnectionInfo connectionInfo, ApplicationLog entity)
        {
            return await InsertInternal(connectionInfo, entity);
        }

        ///// <summary>
        ///// Update
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="ApplicationLog">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Update(IConnectionInfo connectionInfo, ApplicationLog entity)
        //{
        //    return UpdateInternal(connectionInfo, entity).Result;
        //}

        /// <summary>
        /// Update Async.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> UpdateAsync(IConnectionInfo connectionInfo, ApplicationLog entity)
        {
            return await UpdateInternal(connectionInfo, entity);
        }

        ///// <summary>
        ///// Delete
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="ApplicationLog">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Delete(IConnectionInfo connectionInfo, ApplicationLog entity)
        //{
        //    return DeleteInternal(connectionInfo, entity).Result;
        //}

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> DeleteAsync(IConnectionInfo connectionInfo, ApplicationLog entity)
        {
            return await DeleteInternal(connectionInfo, entity);
        }
    }
}
