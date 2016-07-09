using Autofac;
using Autofac.Integration.Mvc;
using Nop.Core.Configuration;
using Nop.Core.Domain;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Services;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            //builder.Register<IDbContext>(c => new ObjectContext()).InstancePerLifetimeScope();
            builder.RegisterType<ObjectContext>().As<IDbContext>().InstancePerLifetimeScope();
            //builder.RegisterType<Student>().InstancePerLifetimeScope();
            //builder.Register(o => new Student()).InstancePerLifetimeScope();
            //builder.Register<IRepository<Student>>(c => new EfRepository<Student>()).InstancePerLifetimeScope();
            //builder.RegisterType<EfRepository<Student>>().As<IRepository<Student>>().InstancePerLifetimeScope();
            //builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
