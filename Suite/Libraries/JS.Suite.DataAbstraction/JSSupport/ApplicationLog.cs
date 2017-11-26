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
    /// Editable Class for the ApplicationLog Table
    /// </summary>
    public class ApplicationLog : ApplicationLog_Generated, IQueryContextContainer
    {
        public ApplicationLog()
        {
            QueryContext = new QueryContext();
        }

        /// <summary>
        /// MessageTypeId
        /// </summary>
        [Display(Name = "Message Type")]
        public override int MessageTypeId
        {
            get
            {
                return base.MessageTypeId;
            }
            set
            {
                base.MessageTypeId = value;
            }
        }

        /// <summary>
        /// TraceLevelId
        /// </summary>
        [Display(Name = "Trace Level")]
        public override int TraceLevelId
        {
            get
            {
                return base.TraceLevelId;
            }
            set
            {
                base.TraceLevelId = value;
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
        /// Message
        /// </summary>
        [DBContains]
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                base.Message = value;
            }
        }

        /// <summary>
        /// Gets the message display.
        /// </summary>
        /// <value>
        /// The message display.
        /// </value>
        [DataType(DataType.MultilineText)]
        [Display(Name = "Message")]
        [DBIgnore]
        public string MessageDisplay
        {
            get
            {
                return base.Message;
            }
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        [DBStartsWith]
        public override string Subject
        {
            get
            {
                return base.Subject;
            }
            set
            {
                base.Subject = value;
            }
        }

        /// <summary>
        /// StackTrace
        /// </summary>
        [DataType(DataType.MultilineText)]
        [Display(Name = "Stack Trace")]
        public override string StackTrace
        {
            get
            {
                return base.StackTrace;
            }
            set
            {
                base.StackTrace = value;
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
        /// Gets or sets the message type text.
        /// </summary>
        /// <value>
        /// The message type text.
        /// </value>
        [DBIgnore]
        [Display(Name = "Message Type")]
        public string MessageTypeText { get; set; }

        /// <summary>
        /// Gets or sets the application text.
        /// </summary>
        /// <value>
        /// The application text.
        /// </value>
        [DBIgnore]
        [Display(Name = "Application")]
        public string ApplicationName { get; set; }

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
        /// Gets or sets the query context.
        /// </summary>
        /// <value>
        /// The query context.
        /// </value>
        [DBIgnore]
        public QueryContext QueryContext { get; set; }
    }
}
