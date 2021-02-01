using Autofac;
using WeddingPlanner.Core.Repositories;
using WeddingPlanner.Core.Services;

namespace WeddingPlanner.Infrastructure.IoC.Modules
{
    public class CoreApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Register services
            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo<IService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register repositories
            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo<IRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
