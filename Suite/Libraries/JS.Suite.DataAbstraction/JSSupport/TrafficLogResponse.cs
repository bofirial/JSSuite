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
    /// Editable Class for the TrafficLogResponse Table
    /// </summary>
    public class TrafficLogResponse : TrafficLogResponse_Generated
    {
        /// <summary>
        /// ResponseCode
        /// </summary>
        [Display(Name = "Response Code")]
        public override string ResponseCode
        {
            get
            {
                return base.ResponseCode;
            }
            set
            {
                base.ResponseCode = value;
            }
        }

        /// <summary>
        /// ResponseCodeDescription
        /// </summary>
        [Display(Name = "Response Code Description")]
        public override string ResponseCodeDescription
        {
            get
            {
                return base.ResponseCodeDescription;
            }
            set
            {
                base.ResponseCodeDescription = value;
            }
        }

        /// <summary>
        /// RedirectLocation
        /// </summary>
        [Display(Name = "Redirect Location")]
        public override string RedirectLocation
        {
            get
            {
                return base.RedirectLocation;
            }
            set
            {
                base.RedirectLocation = value;
            }
        }
    }
}
