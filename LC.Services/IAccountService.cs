using LC.Core.Data;
using LC.Core.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LC.Services
{
    public interface IAccountService
    {

        IList<Account> GetAccounts();


        Account GetAccount(string accountName);

    }
}
