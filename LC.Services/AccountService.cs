﻿using LC.Core.Data;
using LC.Core.Domain;
using LC.Core.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LC.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;

        public AccountService(IRepository<Account> accountRepository)
        {
            this._accountRepository = accountRepository;
        }

        //private IQueryable<Account> GetQueryable()
        //{
        //    return _accountRepository.Table;
        //}

        public Account GetAccount(string accountName)
        {
            Account account = null;

            account = _accountRepository.IQueryableTable.FirstOrDefault();

            return account;

        }

        public IList<Account> GetAccounts()
        {
            return _accountRepository.IQueryableTable.ToList();
        }
    }
}
