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
    /// Generated Class for the Wedding Party Member Enttiy
    /// </summary>
    public class WeddingPartyMember_Generated : EntityBase<WeddingPartyMember>
    {
        #region Properties

        private int _WeddingPartyMemberId;
        /// <summary>
        /// Primary Key for the Wedding Party Member Table
        /// </summary>
        [DBPrimaryKey]
        public virtual int WeddingPartyMemberId
        {
            get
            {
                return _WeddingPartyMemberId;
            }
            set
            {
                    _WeddingPartyMemberId = value;

                    MarkColumnModified();
            }
        }

        private string _NameFirst;
        /// <summary>
        /// NameFirst
        /// </summary>
        public virtual string NameFirst
        {
            get
            {
                return _NameFirst;
            }
            set
            {
                _NameFirst = value;

                MarkColumnModified();
            }
        }

        private string _NameLast;
        /// <summary>
        /// NameLast
        /// </summary>
        public virtual string NameLast
        {
            get
            {
                return _NameLast;
            }
            set
            {
                _NameLast = value;

                MarkColumnModified();
            }
        }

        private int _WeddingSiteId;
        /// <summary>
        /// WeddingSiteId
        /// </summary>
        public virtual int WeddingSiteId
        {
            get
            {
                return _WeddingSiteId;
            }
            set
            {
                _WeddingSiteId = value;

                MarkColumnModified();
            }
        }

        private string _WeddingRole;
        /// <summary>
        /// WeddingRole
        /// </summary>
        public virtual string WeddingRole
        {
            get
            {
                return _WeddingRole;
            }
            set
            {
                _WeddingRole = value;

                MarkColumnModified();
            }
        }

        private string _ImagePath;
        /// <summary>
        /// ImagePath
        /// </summary>
        public virtual string ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;

                MarkColumnModified();
            }
        }

        private int _WeddingPartyMemberTypeId;
        /// <summary>
        /// WeddingPartyMemberTypeId
        /// </summary>
        public virtual int WeddingPartyMemberTypeId
        {
            get
            {
                return _WeddingPartyMemberTypeId;
            }
            set
            {
                _WeddingPartyMemberTypeId = value;

                MarkColumnModified();
            }
        }

        private int _WeddingPartyMemberTypePriority;
        /// <summary>
        /// WeddingPartyMemberTypePriority
        /// </summary>
        public virtual int WeddingPartyMemberTypePriority
        {
            get
            {
                return _WeddingPartyMemberTypePriority;
            }
            set
            {
                _WeddingPartyMemberTypePriority = value;

                MarkColumnModified();
            }
        }

        private int _SummaryPartnerId;
        /// <summary>
        /// SummaryPartnerId
        /// </summary>
        public virtual int SummaryPartnerId
        {
            get
            {
                return _SummaryPartnerId;
            }
            set
            {
                _SummaryPartnerId = value;

                MarkColumnModified();
            }
        }

        private string _Biography;
        /// <summary>
        /// Biography
        /// </summary>
        public virtual string Biography
        {
            get
            {
                return _Biography;
            }
            set
            {
                _Biography = value;

                MarkColumnModified();
            }
        }

        private string _BiographyAuthor;
        /// <summary>
        /// BiographyAuthor
        /// </summary>
        public virtual string BiographyAuthor
        {
            get
            {
                return _BiographyAuthor;
            }
            set
            {
                _BiographyAuthor = value;

                MarkColumnModified();
            }
        }

        private string _UDF1;
        /// <summary>
        /// UDF1
        /// </summary>
        public virtual string UDF1
        {
            get
            {
                return _UDF1;
            }
            set
            {
                _UDF1 = value;

                MarkColumnModified();
            }
        }

        private string _UDF2;
        /// <summary>
        /// UDF2
        /// </summary>
        public virtual string UDF2
        {
            get
            {
                return _UDF2;
            }
            set
            {
                _UDF2 = value;

                MarkColumnModified();
            }
        }

        private string _UDF3;
        /// <summary>
        /// UDF3
        /// </summary>
        public virtual string UDF3
        {
            get
            {
                return _UDF3;
            }
            set
            {
                _UDF3 = value;

                MarkColumnModified();
            }
        }
        
        #endregion

        #region Relationships

        /// <summary>
        /// Summary Partner
        /// </summary>
        [DBRelationship]
        public WeddingPartyMember SummaryPartner { get; set; }

        #endregion
    }
}