using System.Collections.Generic;

namespace ProductsNCategories.Models
{
    public class CategoryView
    {
        public Category ToRender {get;set;}
        public List<Product> ToAdd {get;set;}

        public Association AddForm {get;set;}
    }
}