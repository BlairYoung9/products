using System.Collections.Generic;

namespace ProductsNCategories.Models
{
    public class ProductView
    {
        public Product ToRender {get;set;}
        public List<Category> ToAdd {get;set;}

        public Association AddForm {get;set;}
    }
}