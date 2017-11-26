using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Base Class for Entities
    /// </summary>
    public abstract class EntityBase<T> : SerializationBase<T>, IEntity
        where T : class, new()
    {

        private HashSet<string> _ModifiedColumns;
        /// <summary>
        /// List of Modified Columns in the Entity
        /// </summary>
        [DBIgnore]
        private HashSet<string> ModifiedColumns
        {
            get
            {
                if (_ModifiedColumns == null)
                {
                    _ModifiedColumns = new HashSet<string>();
                }

                return _ModifiedColumns;
            }
        }

        /// <summary>
        /// Gets a List of the Modified Columns in the Entity
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetModifiedColumns()
        {
            return ModifiedColumns;
        }

        /// <summary>
        /// Marks the Provided Column as Modified
        /// </summary>
        /// <param name="columnName"></param>
        protected void MarkColumnModified([CallerMemberName] string columnName = "")
        {
            ModifiedColumns.Add(columnName);
        }

        /// <summary>
        /// Clears the empty modified columns.
        /// </summary>
        public void ClearEmptyModifiedColumns()
        {
            List<string> removedColumns = new List<string>();

            foreach (string column in ModifiedColumns)
            {
                object columnValue = ReflectionHelper.Current.GetPropertyValue(this, column);

                if (columnValue == null || String.IsNullOrEmpty(columnValue.ToString()))
                {
                    removedColumns.Add(column);
                }
            }

            foreach (var column in removedColumns)
            {
                ModifiedColumns.Remove(column);
            }
        }

        /// <summary>
        /// Clears the Modified Columns and Starts Tracking Changes
        /// </summary>
        public void StartTrackingChanges()
        {
            ModifiedColumns.Clear();
        }

        private int _InsertedBy;
        /// <summary>
        /// User Id of the User who Inserted this Entity
        /// </summary>
        public virtual int InsertedBy
        {
            get
            {
                return _InsertedBy;
            }
            set
            {
                if (_InsertedBy != value)
                {
                    _InsertedBy = value;

                    MarkColumnModified();
                }
            }
        }

        private DateTime _InsertedOn;
        /// <summary>
        /// Time that the Entity was Inserted
        /// </summary>
        public virtual DateTime InsertedOn
        {
            get
            {
                return _InsertedOn;
            }
            set
            {
                if (_InsertedOn != value)
                {
                    _InsertedOn = value;

                    MarkColumnModified();
                }
            }
        }

        private int _UpdatedBy;
        /// <summary>
        /// User Id of the User who Updated this Entity Most Recently
        /// </summary>
        public virtual int UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                if (_UpdatedBy != value)
                {
                    _UpdatedBy = value;

                    MarkColumnModified();
                }
            }
        }

        private DateTime _UpdatedOn;
        /// <summary>
        /// Time that the Entity was Updated
        /// </summary>
        public virtual DateTime UpdatedOn
        {
            get
            {
                return _UpdatedOn;
            }
            set
            {
                if (_UpdatedOn != value)
                {
                    _UpdatedOn = value;

                    MarkColumnModified();
                }
            }
        }
    }
}
