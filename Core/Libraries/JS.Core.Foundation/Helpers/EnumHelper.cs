using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Enum Helper
    /// </summary>
    public class EnumHelper : SingletonBase<EnumHelper>
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">TEnum must be an enumerated type</exception>
        public string GetName<TEnum>(TEnum enumValue) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            return Enum.GetName(typeof(TEnum), enumValue);
        }

        /// <summary>
        /// Gets the name with spaces.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumValue">The enum value.</param>
        /// <returns></returns>
        public string GetNameWithSpaces<TEnum>(TEnum enumValue) where TEnum : struct, IConvertible
        {
            string enumName = GetName(enumValue);

            return StringHelper.Current.CamelCaseToSeperated(enumName, " ");
        }

        /// <summary>
        /// Gets an enum from it's name.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <param name="enumName">Name of the enum.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">TEnum must be an enumerated type</exception>
        public TEnum GetFromName<TEnum>(string enumName) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            return (TEnum)Enum.Parse(typeof(TEnum), enumName);
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">TEnum must be an enumerated type</exception>
        public IEnumerable<TEnum> GetValues<TEnum>() where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        /// Gets the select list.
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">TEnum must be an enumerated type</exception>
        public IEnumerable<SelectListItem> GetSelectList<TEnum>() where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enumerated type");
            }

            return GetValues<TEnum>().Select(e => new SelectListItem() {
                Text = GetNameWithSpaces(e),
                Value = Convert.ToInt32(e).ToString()
            });
        }
    }
}
