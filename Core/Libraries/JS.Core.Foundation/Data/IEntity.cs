using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Interface for Entities
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets a List of Modified Columns in the Entity
        /// </summary>
        IEnumerable<string> GetModifiedColumns();

        /// <summary>
        /// Clears the Modified Columns and Starts Tracking Changes
        /// </summary>
        void StartTrackingChanges();

        /// <summary>
        /// Inserted By
        /// </summary>
        int InsertedBy { get; set; }

        /// <summary>
        /// Inserted On
        /// </summary>
        DateTime InsertedOn { get; set; }

        /// <summary>
        /// Updated By
        /// </summary>
        int UpdatedBy { get; set; }

        /// <summary>
        /// Updated On
        /// </summary>
        DateTime UpdatedOn { get; set; }
    }
}
