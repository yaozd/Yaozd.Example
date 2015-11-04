using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Yaozd.CookieLib
{
    /// <summary>
    /// 类说明：CookieHelper
    /// 作  者：zd.yao
    /// </summary>
    internal class CookieHelper
    {
        private static readonly string Domain = GetCookieDomain();

        private static string GetCookieDomain()
        {
            var settingDomain = ConfigurationManager.AppSettings["Cookie_domainName"];
            return settingDomain ?? "";
        }
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            IsNullCookieName(cookiename);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Domain = Domain;
                cookie.Expires = DateTime.Now.AddYears(-30);
                HttpContext.Current.Response.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Remove(cookie.Name);
            }
        }
        /// <summary>
        /// 清除所有指定Cookie
        /// </summary>
        public static void ClearCookieAll()
        {
            var cookies = HttpContext.Current.Request.Cookies;
            var count = cookies.Count;
            for (int i = 0; i < count; i++)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[0];
                if (cookie != null)
                {
                    cookie.Domain = Domain;
                    cookie.Expires = DateTime.Now.AddYears(-30);
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Response.Cookies.Remove(cookie.Name);
                }

            }
        }

        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <param name="isencrypt">是否加密</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename, bool isencrypt)
        {
            IsNullCookieName(cookiename);
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = isencrypt ? DEncryptHelper.Decrypt(HttpUtility.UrlDecode(cookie.Value)) : HttpUtility.UrlDecode(cookie.Value);
            }
            return str;
        }

        /// <summary>
        /// 添加一个Cookie有效时间一天（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        /// <param name="isencrypt">是否加密</param>
        public static void SetCookie(string cookiename, string cookievalue, bool isencrypt)
        {
            SetCookie(cookiename, cookievalue, ExpriesTime(60 * 24), isencrypt);
        }
        /// <summary>
        /// 添加一个Cookie不限制日期（浏览器关闭时自动清除）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        /// <param name="isencrypt"></param>
        public static void SetCookieNoDay(string cookiename, string cookievalue, bool isencrypt)
        {
            SetCookie(cookiename, cookievalue, null, isencrypt);
        }

        /// <summary>
        /// 添加或更新一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        /// <param name="isencrypt">是否加密</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime? expires, bool isencrypt)
        {
            IsNullCookieName(cookiename);
            var cookie = new HttpCookie(cookiename)
            {
                Path = "/",//指定统一的Path，比便能通存通取
                HttpOnly = true,//指定客户端脚本是否可以访问[默认为false]
                Domain = Domain,//设置跨域,这样在其它二级域名下就都可以访问到了
                Value = isencrypt ? HttpUtility.UrlEncode(DEncryptHelper.Encrypt(cookievalue)) : HttpUtility.UrlEncode(cookievalue),
            };
            if (expires != null)
            {
                cookie.Expires = (DateTime)expires;//expires==null 浏览器关闭时自动清除
            }
            if (HttpContext.Current.Request.Cookies[cookiename] == null)
            {
                HttpContext.Current.Response.Cookies.Add(cookie); //添加Cookie
            }
            else
            {
                HttpContext.Current.Response.AppendCookie(cookie); //更新Cookie
            }
        }
        /// <summary>
        /// 判断是否存在Cookie表
        /// </summary>
        /// <param name="cookiename"></param>
        /// <returns></returns>
        public static bool HasCookie(string cookiename)
        {
            IsNullCookieName(cookiename);
            bool boolReturnValue = HttpContext.Current.Request.Cookies[cookiename] != null;
            return boolReturnValue;
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
        /// <summary>
        /// 参数cookiename是否为NULL或空
        /// </summary>
        /// <param name="cookiename"></param>
        private static void IsNullCookieName(string cookiename)
        {
            if (string.IsNullOrWhiteSpace(cookiename))
                throw new ArgumentNullException("cookiename");
        }
    }
}
