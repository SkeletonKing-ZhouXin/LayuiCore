using LC.Core.Infrastructure.Engine;
using LC.Core.Infrastructure.Startup;
using LC.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LC.Web.Framework.Infrastructure.Startup
{
    public class DataBaseStartup : ISelfStartup
    {
        public int Order { get; } = 1;

        public void Configure(IApplicationBuilder application)
        {
            var dbContext = EngineContext.Current.ServiceProvider.GetService<SelfDbContext>();
        }

        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDbContext<SelfDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        }
    }
}
