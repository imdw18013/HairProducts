using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HairProducts.Models
{
    public class Product
    {
        [Key]
        public long ProductId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Product name cannot be more than 100 characters")]
        [MinLength(1, ErrorMessage = "Product name cannot be empty")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Required]
        [Display(Name = "Product Price")]
        [NotMapped]
        public IFormFile Upload { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public decimal ProductPrice { get; set; }
        public long ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
