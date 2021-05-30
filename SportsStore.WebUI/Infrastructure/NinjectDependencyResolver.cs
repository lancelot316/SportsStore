using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using SportsStore.WebUI.Models.Domain;
using SportsStore.WebUI.Models.Domain.Repository;

namespace SportsStore.WebUI.Infrastructure
{

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<StoreRepository>();
            kernel.Bind<IOrderRepository>().To<StoreRepository>();
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}