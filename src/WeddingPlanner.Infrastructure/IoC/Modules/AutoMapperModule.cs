using Autofac;
using WeddingPlanner.Infrastructure.Mapping;

namespace WeddingPlanner.Infrastructure.IoC.Modules
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
        }
    }
}
