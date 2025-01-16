using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProductHub.Models.Enums;
using System.ComponentModel.DataAnnotations;
using static ProductHub.Models.Constants.DataConstants;
using static ProductHub.Models.Constants.MessageConstants;

namespace ProductHub.Models.ViewModels.Product
{
    public class ProductFormModel
    {
        
        public int? Id { get; set; }

        [Required]
        [Display(Name ="Product name")]
        [StringLength(ProductNameMaxLength,
            ErrorMessage =LengthMessage,
            MinimumLength=ProductNameMinLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Product description")]
        [StringLength(ProductDescriptionMaxLength,
            ErrorMessage =LengthMessage,
            MinimumLength = ProductDescriptionMinLength)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Product price")]
        [Range(ProductPriceMinValue, ProductPriceMaxValue, ErrorMessage = RangeMessage)]
        public double Price { get; set; }

        [Display(Name = "Product amount")]
        [Range(ProductAmountMinValue, ProductAmountMaxValue, ErrorMessage = RangeMessage)]
        public int Amount { get; set; }

        [Display(Name = "Unit of measurement")]
        public MeasurementUnit MeasurementUnit { get; set; }
        public int CategoryId {  get; set; }

        [ValidateNever]
        public string ImgUrl { get; set; }=string.Empty;

        

    }
}
