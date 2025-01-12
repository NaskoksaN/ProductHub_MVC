using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductHub.DataAccess.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Comment("Product name")]
        public string Name { get; set; }
        [Required]
        [Comment("Product Description")]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }
        
        [Comment("Product price")]
        public double Price {  get; set; }

        public string ISBN {  get; set; } 
    }
}
