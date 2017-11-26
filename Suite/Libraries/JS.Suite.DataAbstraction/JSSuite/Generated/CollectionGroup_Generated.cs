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
    /// Generated Class for the Collection Group Enttiy
    /// </summary>
    public class CollectionGroup_Generated : EntityBase<CollectionGroup>
    {
        #region Properties

        private int _CollectionGroupId;
        /// <summary>
        /// Primary Key for the Collection Group Table
        /// </summary>
        [DBPrimaryKey]
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

        private string _UDFLongText1Name;
        /// <summary>
        /// UDFLongText1Name
        /// </summary>
        public virtual string UDFLongText1Name
        {
            get
            {
                return _UDFLongText1Name;
            }
            set
            {
                _UDFLongText1Name = value;

                MarkColumnModified();
            }
        }

        private bool _UDFLongText1ActiveFlag;
        /// <summary>
        /// UDFLongText1ActiveFlag
        /// </summary>
        public virtual bool UDFLongText1ActiveFlag
        {
            get
            {
                return _UDFLongText1ActiveFlag;
            }
            set
            {
                _UDFLongText1ActiveFlag = value;

                MarkColumnModified();
            }
        }

        private string _UDFShortText1Name;
        /// <summary>
        /// UDFShortText1Name
        /// </summary>
        public virtual string UDFShortText1Name
        {
            get
            {
                return _UDFShortText1Name;
            }
            set
            {
                _UDFShortText1Name = value;

                MarkColumnModified();
            }
        }

        private bool _UDFShortText1ActiveFlag;
        /// <summary>
        /// UDFShortText1ActiveFlag
        /// </summary>
        public virtual bool UDFShortText1ActiveFlag
        {
            get
            {
                return _UDFShortText1ActiveFlag;
            }
            set
            {
                _UDFShortText1ActiveFlag = value;

                MarkColumnModified();
            }
        }

        private string _UDFShortText2Name;
        /// <summary>
        /// UDFShortText2Name
        /// </summary>
        public virtual string UDFShortText2Name
        {
            get
            {
                return _UDFShortText2Name;
            }
            set
            {
                _UDFShortText2Name = value;

                MarkColumnModified();
            }
        }

        private bool _UDFShortText2ActiveFlag;
        /// <summary>
        /// UDFShortText2ActiveFlag
        /// </summary>
        public virtual bool UDFShortText2ActiveFlag
        {
            get
            {
                return _UDFShortText2ActiveFlag;
            }
            set
            {
                _UDFShortText2ActiveFlag = value;

                MarkColumnModified();
            }
        }

        private string _UDFShortText3Name;
        /// <summary>
        /// UDFShortText3Name
        /// </summary>
        public virtual string UDFShortText3Name
        {
            get
            {
                return _UDFShortText3Name;
            }
            set
            {
                _UDFShortText3Name = value;

                MarkColumnModified();
            }
        }

        private bool _UDFShortText3ActiveFlag;
        /// <summary>
        /// UDFShortText3ActiveFlag
        /// </summary>
        public virtual bool UDFShortText3ActiveFlag
        {
            get
            {
                return _UDFShortText3ActiveFlag;
            }
            set
            {
                _UDFShortText3ActiveFlag = value;

                MarkColumnModified();
            }
        }

        private string _UDFDateTime1Name;
        /// <summary>
        /// UDFDateTime1Name
        /// </summary>
        public virtual string UDFDateTime1Name
        {
            get
            {
                return _UDFDateTime1Name;
            }
            set
            {
                _UDFDateTime1Name = value;

                MarkColumnModified();
            }
        }

        private bool _UDFDateTime1ActiveFlag;
        /// <summary>
        /// UDFDateTime1ActiveFlag
        /// </summary>
        public virtual bool UDFDateTime1ActiveFlag
        {
            get
            {
                return _UDFDateTime1ActiveFlag;
            }
            set
            {
                _UDFDateTime1ActiveFlag = value;

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
        /// Collection Item Tags
        /// </summary>
        [DBRelationship]
        public List<CollectionItem> CollectionItems { get; set; }

        /// <summary>
        /// Collection Group JS Users.
        /// </summary>
        /// <value>
        /// Collection Group JS Users.
        /// </value>
        [DBRelationship]
        public List<CollectionGroupJSUser> CollectionGroupJSUser { get; set; }
        
        #endregion

    }
}
