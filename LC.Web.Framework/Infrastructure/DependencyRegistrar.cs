using Autofac;
using LC.Core.Common;
using LC.Core.Configuration;
using LC.Core.Data;
using LC.Core.Infrastructure.DependencyManagement;
using LC.Core.Infrastructure.TypeFinder;
using LC.Data;
using LC.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.Web.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order { get; }

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, LCConfig config)
        {
            //公共
            builder.RegisterType<CommonHelper>().SingleInstance();

            //数据
            builder.RegisterType<SelfDbContext>().As<IDbContext>().InstancePerLifetimeScope();

            //数据操作
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //服务
            //builder.RegisterType<AccountService>().InstancePerLifetimeScope();
            builder.RegisterType<AccountService>().As<IAccountService>().InstancePerLifetimeScope();
        }
    }
}
