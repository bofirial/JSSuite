using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// String Helper
    /// </summary>
    public class StringHelper : SingletonBase<StringHelper>
    {
        private bool IsCamelCaseCharacter(char c)
        {
            return char.IsUpper(c) || char.IsNumber(c);
        }

        /// <summary>
        /// Converts a String from CamelCase to a Seperated String using the Provided Seperator
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="seperator">The seperator.</param>
        /// <returns></returns>
        public string CamelCaseToSeperated(string input, string seperator)
        {
            if (String.IsNullOrEmpty(input))
            {
                return input;
            }

            string output = String.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (i > 0 && IsCamelCaseCharacter(input[i]) && !IsCamelCaseCharacter(input[i - 1]))
                {
                    output += seperator;
                }

                output += input[i];
            }

            return output;
        }
    }
}
