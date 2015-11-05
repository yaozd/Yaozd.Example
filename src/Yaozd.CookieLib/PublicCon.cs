using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaozd.CookieLib
{
    internal class PublicCon
    {
        private static string AppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
        /// <summary>
        /// Cookie前缀--默认值"_."
        /// </summary>
        /// <returns></returns>
        public static string CookiePrefix()
        {
            var settingVal = AppSetting("Cookie_Prefix")?? "_";
            return settingVal+".";
        }
        /// <summary>
        /// Cookie是否加密--默认值true
        /// </summary>
        /// <returns></returns>
        internal static bool CookieIsEncrypt()
        {
            var settingVal = AppSetting("Cookie_IsEncrypt");
            return settingVal == null || bool.Parse(settingVal); 
        }
    }
}
