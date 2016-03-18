using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.DbAccess;
using Domain.Models;
using Web.Models;

namespace Web.Controllers
{
    public class StoreProductController : Controller
    {
        private IGenericRepository<StoreProduct> repository;
        public int pageSize = 4;

        public StoreProductController(IGenericRepository<StoreProduct> ninjectInjectionRepository)
        {
            repository = ninjectInjectionRepository;
        }

        //
        // GET: /StoreProduct/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List(string category,int page = 1)
        {
            StoreProductsListViewModel model = new StoreProductsListViewModel
            {
                StoreProducts = repository.GetAll().Where(o=>o.Category==category || category == null)
                .OrderBy(o => o.StoreProductId)
                .Skip((page-1)*pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?

                    repository.GetAll().Count() :
                    repository.GetAll().Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
                
            };
            return View(model);
        }

    }
}
