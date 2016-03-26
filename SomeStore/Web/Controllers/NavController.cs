using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using Domain.DbAccess;
using Domain.Models;

namespace Web.Controllers
{
    public class NavController : Controller
    {
        //
        // GET: /Nav/
        private IGenericRepository<StoreProduct> repository;

        public NavController(IGenericRepository<StoreProduct> ninjectInjectionRepository)
        {
            repository = ninjectInjectionRepository;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = repository
                .GetAll()
                .Select(o => o.Category)
                .Distinct()
                .OrderBy(o => o);
            //string menuVersion = mobileMenu ? "MobileMenu" : "Menu";
            return PartialView("FlexMenu",categories);
        }

    }
}
