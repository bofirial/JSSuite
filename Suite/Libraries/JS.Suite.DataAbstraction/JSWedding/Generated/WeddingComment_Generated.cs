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

namespace JS.Suite.DataAbstraction.JSWedding.Generated
{
    /// <summary>
    /// Generated Class for the Wedding Comment Enttiy
    /// </summary>
    public class WeddingComment_Generated : EntityBase<WeddingComment>
    {
        #region Properties

        private int _WeddingCommentId;
        /// <summary>
        /// Primary Key for the Wedding Comment Table
        /// </summary>
        [DBPrimaryKey]
        public virtual int WeddingCommentId
        {
            get
            {
                return _WeddingCommentId;
            }
            set
            {
                    _WeddingCommentId = value;

                    MarkColumnModified();
            }
        }

        private string _UserName;
        /// <summary>
        /// UserName
        /// </summary>
        public virtual string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;

                MarkColumnModified();
            }
        }

        private string _Email;
        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;

                MarkColumnModified();
            }
        }

        private string _Comment;
        /// <summary>
        /// Comment
        /// </summary>
        public virtual string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                _Comment = value;

                MarkColumnModified();
            }
        }

        private bool _HiddenFlag;
        /// <summary>
        /// Hidden Flag
        /// </summary>
        public virtual bool HiddenFlag
        {
            get
            {
                return _HiddenFlag;
            }
            set
            {
                _HiddenFlag = value;

                MarkColumnModified();
            }
        }

        private int _WebSiteId;
        /// <summary>
        /// Web Site Id
        /// </summary>
        public virtual int WebSiteId
        {
            get
            {
                return _WebSiteId;
            }
            set
            {
                _WebSiteId = value;

                MarkColumnModified();
            }
        }


        #endregion

        #region Relationships
        
        #endregion
    }
}