using System.ComponentModel.DataAnnotations;

using static ProductHub.Models.Constants.DataConstants;
using static ProductHub.Models.Constants.MessageConstants;

namespace ProductHub.Models.ViewModels.Company
{
    public class CompanyFormModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(NameMaxLength,
            ErrorMessage =LengthMessage, 
            MinimumLength =NameMinLength)]
        [Display(Name ="Company Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(VATMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = VATMinLength)]
        [Display(Name = "Company VAT")]
        public string VAT { get; set; } = string.Empty;

        [StringLength(CityMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = CityMinLength)]
        [Display(Name = "Company City")]
        public string? City { get; set; }

        [StringLength(PostCodeMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = PostCodeMinLength)]
        [Display(Name = "Company Postal Code")]
        public string? PostalCode { get; set; }

        [StringLength(StreetMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = StreetMinLength)]
        [Display(Name = "Company Street Address")]
        public string? StreetAddress { get; set; }

        [StringLength(PhoneMaxLength,
            ErrorMessage = LengthMessage,
            MinimumLength = PhoneMinLength)]
        [Display(Name = "Company Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
