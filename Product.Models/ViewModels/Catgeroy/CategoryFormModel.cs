using System.ComponentModel.DataAnnotations;

using static Product.Models.Constants.DataConstants;
using static Product.Models.Constants.MessageConstants;

namespace Product.Models.ViewModels.Catgeroy
{
    public class CategoryFormModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CategoryNameMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = CategoryNameMinLength)]
        [Display(Name ="Category name")]
        public string Name {  get; set; }=string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name="Display order")]
        [Range(DisplayOrderMinValue,
               DipslayOrderMaxValue,
               ErrorMessage = RangeMessage)]
        public int DisplayOrder {  get; set; }
    }
}
