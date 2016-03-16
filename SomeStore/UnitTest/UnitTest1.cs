using System;
using System.Collections.Generic;
using System.Linq;
using Domain.DbAccess;
using Domain.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Web.Controllers;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PaginationCheck()
        {
            Mock<IGenericRepository<StoreProduct>> mock = new Mock<IGenericRepository<StoreProduct>>();

            mock.Setup(m => m.GetAll()).Returns(new List<StoreProduct>
            {
                new StoreProduct{ Category = "Cat1", Description = "Desc1", Name = "Name1", Price = 123, StoreProductId = 1},
                new StoreProduct{ Category = "Cat2", Description = "Desc2", Name = "Name2", Price = 123, StoreProductId = 2},
                new StoreProduct{ Category = "Cat3", Description = "Desc3", Name = "Name3", Price = 123, StoreProductId = 3},
                new StoreProduct{ Category = "Cat4", Description = "Desc4", Name = "Name4", Price = 123, StoreProductId = 4},
                new StoreProduct{ Category = "Cat5", Description = "Desc5", Name = "Name5", Price = 123, StoreProductId = 5},
            });

            StoreProductController controller = new StoreProductController(mock.Object);
            controller.pageSize = 3;

            IEnumerable<StoreProduct> result = (IEnumerable<StoreProduct>) controller.List(2).Model;

            List<StoreProduct> products = result.ToList();
            Assert.IsTrue(products.Count == 2);
            Assert.AreEqual(products[0].Category, "Cat4");
            Assert.AreEqual(products[1].Category, "Cat5");
        }
    }
}
