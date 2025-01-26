using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

using static ProductHub.Models.Constants.DataConstants;
using static ProductHub.Models.Constants.MessageConstants;

namespace ProductHub.Utility.ViewModels.ShopingCart
{
    public class ShopingCartFormModel
    {
        public int? Id { get; set; }

        public int ProductId { get; set; }
        [ValidateNever]
        public ProductHub.DataAccess.Entities.Product Product { get; set; } = null!;
     
        [Required]
        [Range(ProductMinCount, ProductMaxCount, ErrorMessage=RangeMessage)]
        [Display(Name = "Count of product")]
        public int Count { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;
        
    }
}
