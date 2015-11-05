using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Utilities.Serialize;
using Yaozd.CookieLib;
using Yaozd.CookieLib.CookieVal;
using Yaozd.Model;

namespace Yaozd.Web.Extendsions
{
    public class HttpRequestExt
    {
        public static bool IsLogined()
        {
            return User()!=null;
        }

        internal static void SetLogin(User user, bool rememberMe)
        {
            var item = new RequestUser { Id = user.Id, Name = user.Name };
            var minutes = rememberMe ? 60 * 24 * 365 : 60;
            var cookievalue = SerializeHelper.ToJson(item);
            CookieExtend.ClearCookieAll();//清空全部
            CookieExtend.SetCookie(CookiesName.UserInfo, cookievalue, minutes);//添加用户信息
        }

        internal static void Logout()
        {
            CookieExtend.ClearCookieAll();//清空全部
        }

        public static RequestUser User()
        {
            var str = CookieExtend.GetCookieValue(CookiesName.UserInfo);
            if (string.IsNullOrWhiteSpace(str))
            {
                return null;
            }
            return SerializeHelper.JsonDeserialize<RequestUser>(str.Trim());
        }
    }
}