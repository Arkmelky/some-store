using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.DbAccess;
using Domain.Models;

namespace Web.Controllers
{
    public class AdminController : Controller
    {
        private IGenericRepository<StoreProduct> repository;

        public AdminController(IGenericRepository<StoreProduct> ninjectInjectionRepository)
        {
            repository = ninjectInjectionRepository;
        }

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(repository.GetAll());
        }

        
        public ActionResult Edit(int storeProductId)
        {
            return View(repository.Get(storeProductId));
        }

        [HttpPost]
        public ActionResult Edit(StoreProduct storeProduct)
        {
            if (ModelState.IsValid)
            {
                repository.Add(storeProduct);
                repository.Save();
                TempData["message"] = string.Format("Product '{0}' has been updated!", storeProduct.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(storeProduct);
            }
        }

        public ActionResult Create()
        {
            return View("Edit", new StoreProduct());
        }

        [HttpPost]
        public ActionResult Delete(int storeProductId)
        {
            if (storeProductId > 0)
            {
                var product = repository.Get(storeProductId);
                if (product != null)
                {
                    TempData["message"] = string.Format("Product '{0}' has been removed!",product.Name);

                    repository.Remove(product);
                    repository.Save();

                    return RedirectToAction("Index");
                }
            }
            
            TempData["error"] = string.Format("Product does not exist.");
            

            return RedirectToAction("Index");
        }
    }
}
