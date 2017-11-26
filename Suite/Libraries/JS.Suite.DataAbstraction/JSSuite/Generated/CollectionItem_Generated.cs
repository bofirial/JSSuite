﻿using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//************************
// GENERATED CLASS
// DO NOT EDIT THIS FILE
//************************

namespace JS.Suite.DataAbstraction.JSSuite.Generated
{
    /// <summary>
    /// Generated Class for the Collection Item Enttiy
    /// </summary>
    public class CollectionItem_Generated : EntityBase<CollectionItem>
    {
        #region Properties

        private int _CollectionItemId;
        /// <summary>
        /// Primary Key for the Collection Item Table
        /// </summary>
        [DBPrimaryKey]
        public virtual int CollectionItemId
        {
            get
            {
                return _CollectionItemId;
            }
            set
            {
                _CollectionItemId = value;

                MarkColumnModified();
            }
        }
        
        private string _Name;
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;

                MarkColumnModified();
            }
        }

        private int _CollectionGroupId;
        /// <summary>
        /// Collection Group Id
        /// </summary>
        public virtual int CollectionGroupId
        {
            get
            {
                return _CollectionGroupId;
            }
            set
            {
                _CollectionGroupId = value;

                MarkColumnModified();
            }
        }


        private int? _CollectionItemTypeId;
        /// <summary>
        /// Collection Item Type Id
        /// </summary>
        public virtual int? CollectionItemTypeId
        {
            get
            {
                return _CollectionItemTypeId;
            }
            set
            {
                _CollectionItemTypeId = value;

                MarkColumnModified();
            }
        }

        private string _UDFLongText1;
        /// <summary>
        /// UDF Long Text
        /// </summary>
        public virtual string UDFLongText1
        {
            get
            {
                return _UDFLongText1;
            }
            set
            {
                _UDFLongText1 = value;

                MarkColumnModified();
            }
        }

        private string _UDFShortText1;
        /// <summary>
        /// UDF Short Text 1
        /// </summary>
        public virtual string UDFShortText1
        {
            get
            {
                return _UDFShortText1;
            }
            set
            {
                _UDFShortText1 = value;

                MarkColumnModified();
            }
        }

        private string _UDFShortText2;
        /// <summary>
        /// UDF Short Text 2
        /// </summary>
        public virtual string UDFShortText2
        {
            get
            {
                return _UDFShortText2;
            }
            set
            {
                _UDFShortText2 = value;

                MarkColumnModified();
            }
        }

        private string _UDFShortText3;
        /// <summary>
        /// UDF Short Text 3
        /// </summary>
        public virtual string UDFShortText3
        {
            get
            {
                return _UDFShortText3;
            }
            set
            {
                _UDFShortText3 = value;

                MarkColumnModified();
            }
        }

        private DateTime? _UDFDateTime1;
        /// <summary>
        /// UDF Date Time 1
        /// </summary>
        public virtual DateTime? UDFDateTime1
        {
            get
            {
                return _UDFDateTime1;
            }
            set
            {
                _UDFDateTime1 = value;

                MarkColumnModified();
            }
        }

        private string _Note;
        /// <summary>
        /// Note
        /// </summary>
        public virtual string Note
        {
            get
            {
                return _Note;
            }
            set
            {
                _Note = value;

                MarkColumnModified();
            }
        }

        #endregion

        #region Relationships

        /// <summary>
        /// Collection Group
        /// </summary>
        [DBRelationship]
        public CollectionGroup CollectionGroup { get; set; } 

        /// <summary>
        /// Collection Item Detail Movies
        /// </summary>
        [DBRelationship]
        public List<CollectionItemDetailMovie> CollectionItemDetailMovies { get; set; } 

        #endregion

    }
}