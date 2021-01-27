using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressLookup.Models
{
    public class CurrentAddress
    {
        public int Code
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string ZipCode
        {
            get;
            set;
        }
    }
}