using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressLookup.Models
{
    public class AddressModel
    {
        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Zip Code is required.")]
        [RegularExpression("^[0-9]{5}$|^[0-9]{5}-[0-9]{4}$", ErrorMessage = "Invalid Zip Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public string Code { get; set; }

        public string Status { get; set; }

        public int AddressID { get; set; }
    }
}