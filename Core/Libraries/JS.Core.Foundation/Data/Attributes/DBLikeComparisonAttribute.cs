using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JS.Core.Foundation.Resource;

namespace JS.Core.Foundation.Data
{
    /// <summary>
    /// Database Like Comparison Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class DBLikeComparisonAttribute : RegularExpressionAttribute 
    {
        private static List<string> dBLikeSpecialCharacters = new List<string>() {
            "%",
            "[",
            "]",
            "_",
            "^"
        };

        private static List<string> regexSpecialCharacters = new List<string>() {
            "]"
        };

        private static string BuildRegexPattern(string wildcard)
        {
            List<string> invalidCharacters = new List<string>(dBLikeSpecialCharacters);

            invalidCharacters.Remove(wildcard);

            invalidCharacters = invalidCharacters.Select(c => regexSpecialCharacters.Contains(c) ? @"\" + c : c).ToList();

            return String.Format(@"^[^{0}]*$", String.Join("", invalidCharacters));
        }

        private string wildcard;

        /// <summary>
        /// Gets or sets the wildcard character.
        /// </summary>
        /// <value>
        /// The wildcard character.
        /// </value>
        public string Wildcard
        {
            get { return wildcard; }
            set { wildcard = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DBLikeComparisonAttribute" /> class.
        /// </summary>
        /// <param name="wildcard">The wildcard.</param>
        public DBLikeComparisonAttribute(string wildcard = null)
            : base(BuildRegexPattern(wildcard))
        {
            this.Wildcard = wildcard;

            this.ErrorMessageResourceName = "TheField_MustNotContainSpecialCharacters";
            this.ErrorMessageResourceType = typeof(CoreLocalization);
        }
    }
}