using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Abstract;

namespace Domain.Models
{
    public class StoreProduct
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int StoreProductId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter product name, please.")]
        public string Name { get; set; }
        
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter product description, please.")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Enter product category, please.")]
        public string Category { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Enter product price, please.")]
        [Range(0.01,Double.MaxValue,ErrorMessage="Price must be positive number.")]
        public decimal Price { get; set; }
    }
}
