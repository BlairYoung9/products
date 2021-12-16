using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProductsNCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        public double Price { get; set; }
        
        //public Category CategoryBelongs {get;set;}
        
        public List<Association> CatOwned {get;set;}
        [NotMapped]
        public List<Product> listofProds { get; set; }
        
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}