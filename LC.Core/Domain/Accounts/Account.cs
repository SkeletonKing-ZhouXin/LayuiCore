using LC.Core;
using System;

namespace LC.Core.Domain.Accounts
{
    /// <summary>
    /// 账号
    /// </summary>
    public class Account : BaseEntity
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
        /// 登陆时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 注销事件
        /// </summary>
        public DateTime? LogoutTime { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public int AccountState { get; set; }

        /// <summary>
        /// 账号类别
        /// </summary>
        public int AccountType { get; set; }
    }
}
