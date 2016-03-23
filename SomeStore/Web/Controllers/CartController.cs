using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.DbAccess;
using Domain.Models;
using Domain.Models.Abstract;
using Web.Models;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private IGenericRepository<StoreProduct> repository;
        private IOrderProcessor orderProcessor;

        public CartController(IGenericRepository<StoreProduct> ninjectInjectionRepository, IOrderProcessor processor)
        {
            orderProcessor = processor;
            repository = ninjectInjectionRepository;
        }

        //
        // GET: /Cart/

        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int StoreProductId, string returnUrl)
        {
            StoreProduct product = repository.Get(StoreProductId);

            if (product != null)
            {
                cart.AddItem(product,1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int StoreProductId, string returnUrl)
        {
            StoreProduct product = repository.Get(StoreProductId);

            if (product != null)
            {
                cart.RemoveItem(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Items.Count() == 0)
            {
                ModelState.AddModelError("","Your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View();
            }
            else
            {
                return View(shippingDetails);
            }
            
        }
    }
}
