using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nop.Web.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<HomeController>().InstancePerLifetimeScope();
            builder.RegisterType<AccountController>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 2; }
        }
    }
}