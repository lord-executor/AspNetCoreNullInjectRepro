using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Ninject;
using Ninject.Web.AspNetCore;
using Ninject.Web.AspNetCore.Hosting;
using Ninject.Web.Common.SelfHost;

namespace AspNetCoreNullInjectRepro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostConfiguration = new AspNetCoreHostConfiguration(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .BlockOnStart();

            var host = new NinjectSelfHostBootstrapper(CreateKernel, hostConfiguration);
            host.Start();
        }

        public static IKernel CreateKernel()
        {
            var settings = new NinjectSettings();
            settings.LoadExtensions = false;

            var kernel = new AspNetCoreKernel(settings);

            kernel.Load(typeof(AspNetCoreHostConfiguration).Assembly);

            // This is the "workaround" for the IActionContextAccessor issue. Since the PageRequestDelegateFactory class is internal, the only
            // option is to actually _provide_ a default implementation in the form of a NullActionContextAccessor
            //kernel.Bind<IActionContextAccessor>().ToConstant(new NullActionContextAccessor());

            return kernel;
        }

        public static IWebHostBuilder CreateWebHostBuilder()
        {
            return new DefaultWebHostConfiguration(null)
                .ConfigureAll()
                .GetBuilder();
        }

        private class NullActionContextAccessor : IActionContextAccessor
        {
            public ActionContext ActionContext
            {
                get
                {
                    return null;
                }
                set
                {
                }
            }
        }
    }
}
