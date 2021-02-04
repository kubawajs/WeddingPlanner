using Autofac;
using System.Collections.Generic;
using System.Linq;
using WeddingPlanner.Core.Repositories;
using WeddingPlanner.Core.Services;

namespace WeddingPlanner.Infrastructure.IoC.Modules
{
    public class CoreApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = GetSolutionAssemblies();

            // Register services
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // Register repositories
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo<IRepository>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

        private static System.Reflection.Assembly[] GetSolutionAssemblies()
        {
            var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            var assembliesList = new List<System.Reflection.Assembly>
            {
                entryAssembly
            };
            assembliesList.AddRange(entryAssembly.GetReferencedAssemblies()
                                                 .Where(x => x.FullName.StartsWith(Core.Constants.SolutionPrefix))
                                                 .Select(System.Reflection.Assembly.Load));
            return assembliesList.ToArray();
        }
    }
}
