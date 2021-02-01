using Autofac;
using WeddingPlanner.Infrastructure.IoC.Modules;

namespace WeddingPlanner.Infrastructure.IoC
{
    public class ContainerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AutoMapperModule>();
            builder.RegisterModule<CoreApplicationModule>();
        }
    }
}
