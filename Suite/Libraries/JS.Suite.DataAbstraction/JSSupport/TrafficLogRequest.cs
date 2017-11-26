using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSupport.Generated;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSSupport
{
    /// <summary>
    /// Editable Class for the TrafficLogRequest Table
    /// </summary>
    public class TrafficLogRequest : TrafficLogRequest_Generated, IQueryContextContainer
    {

        /// <summary>
        /// RequestedUrl
        /// </summary>
        [DBContains]
        [Display(Name = "Requested Url")]
        public override string RequestedUrl
        {
            get
            {
                return base.RequestedUrl;
            }
            set
            {
                base.RequestedUrl = value;
            }
        }

        /// <summary>
        /// Location
        /// </summary>
        [DBContains]
        [Display(Name = "Location")]
        public override string Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
            }
        }

        /// <summary>
        /// BrowserVersion
        /// </summary>
        [DBStartsWith]
        [Display(Name = "Browser Version")]
        public override string BrowserVersion
        {
            get
            {
                return base.BrowserVersion;
            }
            set
            {
                base.BrowserVersion = value;
            }
        }

        /// <summary>
        /// OperatingSystem
        /// </summary>
        [DBStartsWith]
        [Display(Name = "Operating System")]
        public override string OperatingSystem
        {
            get
            {
                return base.OperatingSystem;
            }
            set
            {
                base.OperatingSystem = value;
            }
        }

        /// <summary>
        /// ApplicationId
        /// </summary>
        [Display(Name = "Application")]
        public override int ApplicationId
        {
            get
            {
                return base.ApplicationId;
            }
            set
            {
                base.ApplicationId = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the js user.
        /// </summary>
        /// <value>
        /// The name of the js user.
        /// </value>
        [DBIgnore]
        [Display(Name = "User Name")]
        public string JSUserName { get; set; }

        /// <summary>
        /// Gets or sets the application text.
        /// </summary>
        /// <value>
        /// The application text.
        /// </value>
        [DBIgnore]
        [Display(Name = "Application Name")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// TrackingGuid
        /// </summary>
        [DBStartsWith]
        [Display(Name = "Tracking Guid")]
        public override string TrackingGuid
        {
            get
            {
                return base.TrackingGuid;
            }
            set
            {
                base.TrackingGuid = value;
            }
        }

        /// <summary>
        /// Gets the inserted on display.
        /// </summary>
        /// <value>
        /// The inserted on display.
        /// </value>
        [DBIgnore]
        [Display(Name = "Inserted On")]
        public string InsertedOnDisplay
        {
            get
            {
                return InsertedOn.ToString();
            }
        }
        private DateTime? _InsertedOn_From;
        /// <summary>
        /// Gets or sets the inserted on_ from.
        /// </summary>
        /// <value>
        /// The inserted on_ from.
        /// </value>
        [DBIgnore]
        [DataType(DataType.Date)]
        [Display(Name = "From")]
        public DateTime? InsertedOn_From
        {
            get
            {
                return _InsertedOn_From;
            }
            set
            {
                _InsertedOn_From = value;

                MarkColumnModified();
            }
        }

        private DateTime? _InsertedOn_To;

        /// <summary>
        /// Gets or sets the inserted on_ to.
        /// </summary>
        /// <value>
        /// The inserted on_ to.
        /// </value>
        [DBIgnore]
        [DataType(DataType.Date)]
        [Display(Name = "To")]
        public DateTime? InsertedOn_To
        {
            get
            {
                return _InsertedOn_To;
            }
            set
            {
                _InsertedOn_To = value;

                MarkColumnModified();
            }
        }

        /// <summary>
        /// UrlReferrer
        /// </summary>
        [Display(Name = "Url Referrer")]
        public override string UrlReferrer
        {
            get
            {
                return base.UrlReferrer;
            }
            set
            {
                base.UrlReferrer = value;
            }
        }

        /// <summary>
        /// PostedData
        /// </summary>
        [Display(Name = "Posted Data")]
        [DataType(DataType.MultilineText)]
        public override string PostedData
        {
            get
            {
                return base.PostedData;
            }
            set
            {
                base.PostedData = value;
            }
        }

        /// <summary>
        /// RequestType
        /// </summary>
        [Display(Name = "Request Type")]
        public override string RequestType
        {
            get
            {
                return base.RequestType;
            }
            set
            {
                base.RequestType = value;
            }
        }

        /// <summary>
        /// TrafficLogGuid
        /// </summary>
        [Display(Name = "Traffic Log Guid")]
        [DBStartsWith]
        public override string TrafficLogGuid
        {
            get
            {
                return base.TrafficLogGuid;
            }
            set
            {
                base.TrafficLogGuid = value;
            }
        }

        /// <summary>
        /// UserAgent
        /// </summary>
        [Display(Name = "User Agent")]
        public override string UserAgent
        {
            get
            {
                return base.UserAgent;
            }
            set
            {
                base.UserAgent = value;
            }
        }

        /// <summary>
        /// Gets or sets the query context.
        /// </summary>
        /// <value>
        /// The query context.
        /// </value>
        [DBIgnore]
        public QueryContext QueryContext { get; set; }
    }
}
