using System.Collections.Generic;

namespace ProductsNCategories.Models
{
    public class IndexView 
    {
        public List<Product> AllProducts { get; set; }
        public List<Category> AllCats { get; set; }
    }
}