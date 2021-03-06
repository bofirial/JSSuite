﻿using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.JSSupport.Generated

//************************
// GENERATED CLASS
// DO NOT EDIT THIS FILE
//************************
{
    /// <summary>
    /// Generated MessageTypeConfiguration Business Manager
    /// </summary>
    /// <typeparam name="TBusinessManager">The type of the business manager.</typeparam>
    public class MessageTypeConfigurationBusinessManager_Generated<TBusinessManager> : SingletonBase<TBusinessManager> where TBusinessManager : class
    {

        ///// <summary>
        ///// Select
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="filter">The filter.</param>
        ///// <returns></returns>
        //public List<MessageTypeConfiguration> Select(IConnectionInfo connectionInfo, MessageTypeConfiguration filter)
        //{
        //    return MessageTypeConfigurationManager.Current.Select(connectionInfo, filter);
        //}

        /// <summary>
        /// Select Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<List<MessageTypeConfiguration>> SelectAsync(IConnectionInfo connectionInfo, MessageTypeConfiguration filter)
        {
            return await MessageTypeConfigurationManager.Current.SelectAsync(connectionInfo, filter);
        }

        ///// <summary>
        ///// Insert
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="MessageTypeConfiguration">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Insert(IConnectionInfo connectionInfo, MessageTypeConfiguration entity)
        //{
        //    return MessageTypeConfigurationManager.Current.Insert(connectionInfo, entity);
        //}

        /// <summary>
        /// Insert Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> InsertAsync(IConnectionInfo connectionInfo, MessageTypeConfiguration entity)
        {
            return await MessageTypeConfigurationManager.Current.InsertAsync(connectionInfo, entity);
        }

        ///// <summary>
        ///// Update
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="MessageTypeConfiguration">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Update(IConnectionInfo connectionInfo, MessageTypeConfiguration entity)
        //{
        //    return MessageTypeConfigurationManager.Current.Update(connectionInfo, entity);
        //}

        /// <summary>
        /// Update Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> UpdateAsync(IConnectionInfo connectionInfo, MessageTypeConfiguration entity)
        {
            return await MessageTypeConfigurationManager.Current.UpdateAsync(connectionInfo, entity);
        }

        ///// <summary>
        ///// Delete
        ///// </summary>
        ///// <param name="connectionInfo">The connection information.</param>
        ///// <param name="MessageTypeConfiguration">The entity.</param>
        ///// <returns></returns>
        //public IProcessResult Delete(IConnectionInfo connectionInfo, MessageTypeConfiguration entity)
        //{
        //    return MessageTypeConfigurationManager.Current.Delete(connectionInfo, entity);
        //}

        /// <summary>
        /// Delete Async
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public async Task<IProcessResult> DeleteAsync(IConnectionInfo connectionInfo, MessageTypeConfiguration entity)
        {
            return await MessageTypeConfigurationManager.Current.DeleteAsync(connectionInfo, entity);
        }
    }
}
