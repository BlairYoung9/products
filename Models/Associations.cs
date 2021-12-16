using System;
using System.ComponentModel.DataAnnotations;

namespace ProductsNCategories.Models
{
    public class Association
    {
        [Key]
        public int AssociationId { get; set; }
        
        public int ProductId { get; set; }
        public Product ProductWithCat { get; set; }
        
        public int CategoryId { get; set; }
        
        public Category Category2 { get; set; }
        


        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}