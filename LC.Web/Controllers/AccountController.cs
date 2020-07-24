using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LC.Services;
using LC.Web.Models.Accounts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LC.Web.Controllers
{

    public class AccountController : BaseController
    {

        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginModel loginModel)
        {

            var result = new Dictionary<string, object>();

            #region 非空验证

            if (loginModel == null)
            {
                return Json(GetResult(false, "未提交任何数据"));
            }
            else if (string.IsNullOrWhiteSpace(loginModel.AccountName))
            {
                return Json(GetResult(false, "未填写账号"));
            }
            else if (string.IsNullOrWhiteSpace(loginModel.Password))
            {
                return Json(GetResult(false, "未填写密码"));
            }
            else if (string.IsNullOrWhiteSpace(loginModel.VerifyCode))
            {
                return Json(GetResult(false, "未填写验证码"));
            }
            else
            {
                byte[] byteValidateCode;
                HttpContext.Session.TryGetValue("ValidateCode", out byteValidateCode);

                var validateCode = System.Text.Encoding.Default.GetString(byteValidateCode);

                if (loginModel.VerifyCode != validateCode)
                {
                    return Json(GetResult(false, "验证码填写错误"));
                }
            }

            #endregion

            #region 数据验证

            var account = _accountService.GetAccount(loginModel.AccountName);

            if (account == null)
            {
                return Json(GetResult(false, "账号不存在"));
            }
            else if (account.Password != loginModel.Password)
            {
                return Json(GetResult(false, "密码错误"));
            }

            #endregion

            #region 添加Cookie认证信息

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme); //默认Cookie验证

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, account.AccountName));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            Task.Run(async () =>
            {
                //登录用户，相当于ASP.NET中的FormsAuthentication.SetAuthCookie
                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

                //可以使用HttpContext.SignInAsync方法的重载来定义持久化cookie存储用户认证信息，例如下面的代码就定义了用户登录后60分钟内cookie都会保留在客户端计算机硬盘上，
                //即便用户关闭了浏览器，60分钟内再次访问站点仍然是处于登录状态，除非调用Logout方法注销登录。
                //注意其中的AllowRefresh属性，如果AllowRefresh为true，表示如果用户登录后在超过50%的ExpiresUtc时间间隔内又访问了站点，就延长用户的登录时间（其实就是延长cookie在客户端计算机硬盘上的保留时间），
                //例如本例中我们下面设置了ExpiresUtc属性为60分钟后，那么当用户登录后在大于30分钟且小于60分钟内访问了站点，那么就将用户登录状态再延长到当前时间后的60分钟。但是用户在登录后的30分钟内访问站点是不会延长登录时间的，
                //因为ASP.NET Core有个硬性要求，是用户在超过50%的ExpiresUtc时间间隔内又访问了站点，才延长用户的登录时间。
                //如果AllowRefresh为false，表示用户登录后60分钟内不管有没有访问站点，只要60分钟到了，立马就处于非登录状态（不延长cookie在客户端计算机硬盘上的保留时间，60分钟到了客户端计算机就自动删除cookie）

                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,//这里要注意的是HttpContext.SignInAsync(AuthenticationType,…) 所设置的Scheme一定要与前面的配置一样，这样对应的登录授权才会生效。
                principal,
                new AuthenticationProperties()
                {
                    IsPersistent = true,
                    ExpiresUtc = loginModel.DoNotlogin ? DateTimeOffset.UtcNow.AddDays(1) : DateTimeOffset.UtcNow.AddHours(1),//如果设置免登录，时间为5天，否则有效时间为1小时
                    AllowRefresh = true
                });
            }).Wait();

            #endregion

            return Json(GetResult(true));
        }


    }
}