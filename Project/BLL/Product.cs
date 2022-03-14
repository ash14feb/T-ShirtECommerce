using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   
    public class Product
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int Price { get; set; }

        public string Colour { get; set; }
        public string Made { get; set; }

        public string Image { get; set; }

        public string Style { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

    }
    public enum Gender { Unknown, Male, Female };
    public class ProductList
    {
        public List<Product> Data { get; set; }
    }
}
