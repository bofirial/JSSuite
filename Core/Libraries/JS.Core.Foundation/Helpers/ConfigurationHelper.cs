using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Helper for accessing Config Files
    /// </summary>
    public class ConfigurationHelper : SingletonBase<ConfigurationHelper>
    {
        /// <summary>
        /// Returns A Connection String from the Config File
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        public string GetConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        /// <summary>
        /// Gets the environment.
        /// </summary>
        /// <returns></returns>
        public Environments GetEnvironment()
        {
            return EnumHelper.Current.GetFromName<Environments>(GetSetting("Environment"));
        }

        /// <summary>
        /// Gets the setting.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns></returns>
        public string GetSetting(string settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        /// <summary>
        /// Gets the setting and converts it to type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns></returns>
        public T GetSetting<T>(string settingName)
        {
            string settingValue = GetSetting(settingName);

            return TypeHelper.Current.Convert<T>(settingValue);
        }
    }
}
