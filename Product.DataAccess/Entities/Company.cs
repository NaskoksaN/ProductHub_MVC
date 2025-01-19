using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static ProductHub.Models.Constants.DataConstants;

namespace ProductHub.DataAccess.Entities
{
    public class Company
    {
        [Key]
        [Comment("Company Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Company Name")]
        public string Name { get; set; }=string.Empty;

        [Required]
        [MaxLength(VATMaxLength)]
        [Comment("Company VAT")]
        public string VAT {  get; set; }= string.Empty;

        [MaxLength(CityMaxLength)]
        [Comment("Company City")]
        public string? City { get; set; }

        [MaxLength(PostCodeMaxLength)]
        [Comment("Company Postal Code")]
        public string? PostalCode {  get; set; }

        [MaxLength(StreetMaxLength)]
        [Comment("Company Street Address")]
        public string? StreetAddress { get; set; }

        [MaxLength(PhoneMaxLength)]
        [Comment("Company Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
