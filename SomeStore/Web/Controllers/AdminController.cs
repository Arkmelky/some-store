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
                TempData["message"] = string.Format("Product '{0}' has been updated!", storeProduct.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(storeProduct);
            }
        }

    }
}
