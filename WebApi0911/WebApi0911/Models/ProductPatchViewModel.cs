using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi0911.Models
{
    public class ProductPatchViewModel
    {
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Stock { get; set; }
    }
}