using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yaozd.Web.Extendsions;

namespace Yaozd.Web.Filters
{
    ///　<summary>
    ///　权限拦截
    ///　</summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {
        const string ManageLoginUrl = "/Account/Login";
        const string NoAccessUrl = "/Account/NoAccess";
        FilterContextInfo _contextInfo;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                return;
            var context = filterContext.HttpContext.ApplicationInstance.Context;
            if (!IsLogin(context.Request, context.Response))
            {
                RedirectToLogin(filterContext, context);
                return;
            }
            //复杂验证逻辑
            _contextInfo = new FilterContextInfo(filterContext);
            if (!IsAllowAccess(context.Request, context.Response, _contextInfo))
            {
                RedirectToNoAccess(filterContext, context);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
        private static void RedirectToLogin(ActionExecutingContext filterContext, HttpContext context)
        {
            var returnUrl = context.Request.FilePath == null ? "" : "?returnUrl=" + context.Request.FilePath;
            returnUrl = ManageLoginUrl + returnUrl;

            //判断请求的类型,--用于ajax 访问权限的控制
            //application/json
            var acceptTypes = context.Request.AcceptTypes;
            if (acceptTypes != null && acceptTypes.Contains("application/json"))
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { UnAuthorizedRequest = true, State = 1, RedirectToLocal = returnUrl }
                };
                return;
            }
            filterContext.Result = new RedirectResult(returnUrl);
        }
        private static void RedirectToNoAccess(ActionExecutingContext filterContext, HttpContext context)
        {
            //判断请求的类型,--用于ajax 访问权限的控制
            //application/json
            var acceptTypes = context.Request.AcceptTypes;
            if (acceptTypes != null && acceptTypes.Contains("application/json"))
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { UnAuthorizedRequest = true, State = 2, Error = "您没有权限!" }
                };
                return;
            }
            filterContext.Result = new RedirectResult(NoAccessUrl);
        }

        private static bool IsLogin(HttpRequest request, HttpResponse response)
        {
            return HttpRequestExt.IsLogined();
        }

        private bool IsAllowAccess(HttpRequest request, HttpResponse response, FilterContextInfo contextInfo)
        {
            return true;
        }
    }

    public class FilterContextInfo
    {
        public FilterContextInfo(ActionExecutingContext filterContext)
        {
            #region 获取链接中的字符
            // 获取域名
            if (filterContext.HttpContext.Request.Url != null)
                DomainName = filterContext.HttpContext.Request.Url.Authority;

            //获取模块名称
            //  module = filterContext.HttpContext.Request.Url.Segments[1].Replace('/', ' ').Trim();

            //获取 controllerName 名称
            ControllerName = filterContext.RouteData.Values["controller"].ToString();

            //获取ACTION 名称
            ActionName = filterContext.RouteData.Values["action"].ToString();

            #endregion
        }
        /// <summary>
        /// 获取域名
        /// </summary>
        public string DomainName { get; set; }
        /// <summary>
        /// 获取模块名称
        /// </summary>
        //public string Module { get; set; }
        /// <summary>
        /// 获取 controllerName 名称
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 获取ACTION 名称
        /// </summary>
        public string ActionName { get; set; }

    }
}