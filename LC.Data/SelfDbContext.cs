using LC.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Data
{
    public class SelfDbContext : DbContext, IDbContext
    {
        public SelfDbContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// On model creating
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public bool ProxyCreationEnabled { get; set; }
        public bool AutoDetectChangesEnabled { get; set; }
    }
}
