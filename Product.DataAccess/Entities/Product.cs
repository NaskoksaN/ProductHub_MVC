using Microsoft.EntityFrameworkCore;
using ProductHub.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ProductHub.Models.Constants.DataConstants;


namespace ProductHub.DataAccess.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Comment("Product name")]
        [MaxLength(ProductNameMaxLength)]
        public string Name { get; set; }=string.Empty;

        [Required]
        [Comment("Product Description")]
        [MaxLength(ProductDescriptionMaxLength)]
        public string Description { get; set; }=string.Empty;
        
        [Comment("Product price")]
        public double Price {  get; set; }

        [Comment("Product amount")]
        public int Amount {  get; set; }

        [Comment("Unit of measurement")]
        public MeasurementUnit MeasurementUnit { get; set; }

        public string ImgUrl { get; set; }
        public int CategoryId {  get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

    }
}
