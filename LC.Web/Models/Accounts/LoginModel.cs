using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.Web.Models.Accounts
{
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string VerifyCode { get; set; }

        /// <summary>
        /// 免登录
        /// </summary>
        public bool DoNotlogin { get; set; }
    }
}
