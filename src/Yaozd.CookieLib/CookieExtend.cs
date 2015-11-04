using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaozd.CookieLib
{
    public class CookieExtend
    {
        private static readonly string _prefix = PublicCon.CookiePrefix();
        private static readonly bool _isEncrypt = PublicCon.CookieIsEncrypt();

        #region 获得Cookie的值

        public static string GetCookieValue(CookiesName cookiesName)
        {
            return GetCookieValue(cookiesName, _isEncrypt);
        }
        public static string GetCookieValue(CookiesName cookiename, bool isEncrypt)
        {
            return CookieHelper.GetCookieValue(GetCookieName(cookiename), isEncrypt);
        }

        #endregion
        #region 设置Cookie的值
        public static void SetCookie(CookiesName cookiesName, string cookievalue, int minutes)
        {
            SetCookie(cookiesName, cookievalue, ExpriesTime(minutes), _isEncrypt);
        }
        public static void SetCookie(CookiesName cookiesName, string cookievalue, DateTime? dateTime)
        {
            SetCookie(cookiesName, cookievalue, dateTime, _isEncrypt);
        }
        public static void SetCookie(CookiesName cookiename, string cookievalue, DateTime? expires, bool isEncrypt)
        {
            CookieHelper.SetCookie(GetCookieName(cookiename), cookievalue, expires, isEncrypt);
        }

        #endregion
        #region 删除Cookie的值

        public static void ClearCookie(CookiesName cookiename)
        {
            CookieHelper.ClearCookie(GetCookieName(cookiename));
        }

        public static void ClearCookieAll()
        {
            CookieHelper.ClearCookieAll();
        }
        #endregion
        /// <summary>
        /// 获得CookieName
        /// </summary>
        /// <param name="cookiesName"></param>
        /// <returns></returns>
        private static string GetCookieName(CookiesName cookiesName)
        {
            return _prefix + cookiesName.ToString("D");
        }
        /// <summary>
        /// 过期时间
        /// </summary>
        /// <param name="minutes">分钟</param>
        /// <returns></returns>
        public static DateTime ExpriesTime(int minutes)
        {
            return DateTime.Now.AddMinutes(minutes);
        }
    }
}
