﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace LC.Web.Framework.Controllers
{
    /// <summary>
    /// 实现cookie认证授权校验
    /// 参考地址：https://www.cnblogs.com/OpenCoder/p/8341843.html
    /// </summary>
    public class UserAuthorizeAttribute : ActionFilterAttribute
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            bool IsAuthenticated = false;

            var requestURL = context.HttpContext.Request.Path;

            //如果HttpContext.User.Identity.IsAuthenticated为true，
            //或者HttpContext.User.Claims.Count()大于0表示用户已经登录
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                IsAuthenticated = true;
            }


            if (IsAuthenticated)
            {
                //这里通过 HttpContext.User.Claims 可以将我们在Login这个Action中存储到cookie中的所有
                //claims键值对都读出来，比如我们刚才定义的UserName的值admin就在这里读取出来了
                var userName = context.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

                logger.Info($"[{userName}]用户登录校验成功,请求地址[{requestURL}]");
            }
            else
            {
                logger.Info($"没登录访问地址[{requestURL}]");

                context.Result = new RedirectResult("/Account/Login?returnURL=" + requestURL);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
