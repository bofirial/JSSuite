using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Number Helper
    /// </summary>
    public class NumberHelper : SingletonBase<NumberHelper>
    {
        /// <summary>
        /// Returns the number including it's suffix e.g. 2nd
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns></returns>
        public string GetSuffix(int number)
        {
            int lastTwoDigit = number % 100;

            string suffix;

            if (lastTwoDigit > 10 && lastTwoDigit <= 13)
            {
                suffix = "th";
            }
            else
            {
                int lastDigit = number % 10;

                switch (lastDigit)
                {
                    case 1:
                        suffix = "st";
                        break;
                    case 2:
                        suffix = "nd";
                        break;
                    case 3:
                        suffix = "rd";
                        break;
                    default:
                        suffix = "th";
                        break;
                }
            }

            return suffix;
        }
    }
}
