using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Misc.GoogleVisionProductTags.Factories;
using Nop.Plugin.Misc.GoogleVisionProductTags.Services;

namespace Nop.Plugin.Misc.GoogleVisionProductTags.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<GoogleVisionApiFactory>().As<IGoogleVisionApiFactory>().InstancePerLifetimeScope();
            builder.RegisterType<GoogleVisionProductTagsGenerator>().As<IGoogleVisionProductTagsGenerator>().InstancePerLifetimeScope();
        }

        public int Order => 1;
    }
}
