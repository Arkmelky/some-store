using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Enter your name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        [Display(Name = "Main address")]
        public string Address1 { get; set; }
        [Display(Name = "Second address")]
        public string Address2 { get; set; }
        [Display(Name = "Third address")]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Enter your city name")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter you country name")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
        
    }
}
