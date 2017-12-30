using DependencyResolver;
using Ninject.Modules;

namespace PL.Web.Utils
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load() =>
            NInjectDependencyResolver.Configure(this.Kernel);
    }
}