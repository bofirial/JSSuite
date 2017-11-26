using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// JS Condition Types
    /// </summary>
    public enum JSConditionTypes
    {
        /// <summary>
        /// Compares if the Target Value Is Equal To the Field's Value
        /// </summary>
        IsEqualToValue = 1,
        /// <summary>
        /// Compares if the Target Value Is NOT Equal To the Field's Value
        /// </summary>
        IsNotEqualToValue = 2,
        /// <summary>
        /// Compares if the Target Value Is Equal To whether the Field is Valid (true or false)
        /// </summary>
        ValidationEqualsValue = 3
    }
}
