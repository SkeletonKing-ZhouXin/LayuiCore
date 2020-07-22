using LC.Core.Domain.Accounts;
using LC.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Data.Mapping.Accounts
{
    public class AccountMap : ISelfEntityMappingConfiguration
    {
        public void Map(ModelBuilder b)
        {
            b.Entity<Account>().ToTable("Account").HasKey(p => p.Id);
        }
    }
}
