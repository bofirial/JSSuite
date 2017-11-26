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

namespace JS.Suite.DataAbstraction.JSSupport.Generated
{
    /// <summary>
    /// Generated Class for the Traffic Log Response Enttiy
    /// </summary>
    public class TrafficLogResponse_Generated : EntityBase<TrafficLogResponse>
    {
        #region Properties

        private int _TrafficLogResponseId;
        /// <summary>
        /// Primary Key
        /// </summary>
        [DBPrimaryKey]
        public virtual int TrafficLogResponseId
        {
            get
            {
                return _TrafficLogResponseId;
            }
            set
            {
                _TrafficLogResponseId = value;

                MarkColumnModified();
            }
        }

        private string _TrackingGuid;
        /// <summary>
        /// TrackingGuid
        /// </summary>
        public virtual string TrackingGuid
        {
            get
            {
                return _TrackingGuid;
            }
            set
            {
                _TrackingGuid = value;

                MarkColumnModified();
            }
        }

        private string _ResponseCode;
        /// <summary>
        /// ResponseCode
        /// </summary>
        public virtual string ResponseCode
        {
            get
            {
                return _ResponseCode;
            }
            set
            {
                _ResponseCode = value;

                MarkColumnModified();
            }
        }

        private string _ResponseCodeDescription;
        /// <summary>
        /// ResponseCodeDescription
        /// </summary>
        public virtual string ResponseCodeDescription
        {
            get
            {
                return _ResponseCodeDescription;
            }
            set
            {
                _ResponseCodeDescription = value;

                MarkColumnModified();
            }
        }

        private string _RedirectLocation;
        /// <summary>
        /// RedirectLocation
        /// </summary>
        public virtual string RedirectLocation
        {
            get
            {
                return _RedirectLocation;
            }
            set
            {
                _RedirectLocation = value;

                MarkColumnModified();
            }
        }

        #endregion

        #region Relationships

        #endregion
    }
}
