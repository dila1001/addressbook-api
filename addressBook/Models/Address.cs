using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Models
{
    public class Address
    {
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [Range(10000, 99999, ErrorMessage = "Postal code must have 5 digits")]
        public int PostalCode { get; set; }
    }
}
