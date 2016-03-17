using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Models;

namespace Web.Models
{
    public class StoreProductsListViewModel
    {
        public IEnumerable<StoreProduct> StoreProducts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}