using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HairProducts.Models
{
    public class ProductCategory
    {
        [Key]
        public long CategoryId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Category name cannot be more than 100 characters")]
        [MinLength(1, ErrorMessage = "Category name cannot be empty")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Display(Name = "Category Description")]
        public string CategoryDescription { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
