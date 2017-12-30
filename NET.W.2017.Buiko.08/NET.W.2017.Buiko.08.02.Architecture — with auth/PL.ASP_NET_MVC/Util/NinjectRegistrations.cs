using DependencyResolver;
using Ninject.Modules;

namespace PL.ASP_NET_MVC.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load() =>
            NInjectDependencyResolver.Configure(this.Kernel);
    }
}