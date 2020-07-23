using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.Services;
using LC.Web.Models.Accounts;
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

            #endregion

            #region 数据验证

            var account = _accountService.GetAccount(loginModel.AccountName);

            if (account == null)
            {
                return Json(GetResult(false, "账号不存在"));
            }

            #endregion

            return Json(GetResult(true));
        }


    }
}