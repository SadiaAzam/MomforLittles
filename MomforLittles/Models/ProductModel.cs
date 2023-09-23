using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MomforLittles.Models
{
    public class ProductModel
    {
        
        public List<Category> Cat { get; set; }
        public List<Brand> Bnd{ get; set; }
        public List<Product> Pro { get; set; }
       
    }
}