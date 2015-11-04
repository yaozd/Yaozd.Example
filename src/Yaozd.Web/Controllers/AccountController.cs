using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Utilities.Serialize;
using Yaozd.CookieLib;
using Yaozd.Model;
using Yaozd.Web.Models;
using Yaozd.Web.Service;

namespace Yaozd.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountViewModel.Login model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (ModelState.IsValid)
            {

                var user =  UserManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    SetCookieUserInfo(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("Password", "用户名或密码错误");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
        private void SetCookieUserInfo(User user, bool rememberMe)
        {
            var item = new CookieLib.CookieVal.RequestUser {Id = user.Id, Name = user.Name};
            var minutes = rememberMe ? 60*24*365 : 60;
            var cookievalue = SerializeHelper.ToJson(item);
            CookieExtend.ClearCookieAll();//清空全部
            CookieExtend.SetCookie(CookiesName.UserInfo,cookievalue,minutes);//添加用户信息
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
	}
}