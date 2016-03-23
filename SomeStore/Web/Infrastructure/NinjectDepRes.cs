using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Abstract;
using Domain.DbAccess;
using Domain.Models;
using Domain.Models.Abstract;
using Domain.Models.Concrete;
using Moq;
using Ninject;

namespace Web.Infrastructure
{
    public class NinjectDepRes : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDepRes(IKernel kernelParam)
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
            //Interface -> realization.
            //Mock<IStoreProductRepository> mock = new Mock<IStoreProductRepository>();
            //mock.Setup(m => m.StoreProducts).Returns(new List<StoreProduct>
            //{
            //    new StoreProduct{ Name = "Product 1", Description = "Product desc", Price = 123,},
            //    new StoreProduct{ Name = "Product 2", Description = "Product desc", Price = 234,},
            //    new StoreProduct{ Name = "Product 3", Description = "Product desc", Price = 345,},
            //    new StoreProduct{ Name = "Product 4", Description = "Product desc", Price = 456,}
            //});

            //kernel.Bind<IStoreProductRepository>().ToConstant(mock.Object);
            
            kernel.Bind<IGenericRepository<StoreProduct>>().To<GenericRepository<StoreProduct>>();

            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };

            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);

        }
    }
}