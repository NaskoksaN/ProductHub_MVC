using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ProductHub.Models.Constants.DataConstants;

namespace ProductHub.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("User full name")]
        public string Name {  get; set; }=string.Empty;

        [MaxLength(StreetMaxLength)]
        [Comment("User street address")]
        public string? StreetAddress { get; set; }

        [MaxLength(CityMaxLength)]
        [Comment("User city")]
        public string? City { get; set; }

        [MaxLength(PostCodeMaxLength)]
        [Comment("User postal code")]
        public string? PostalCode {  get; set; }

        public int? CompanyId {  get; set; }
        [ForeignKey(nameof(CompanyId))]
        public Company? Company { get; set; }
    }
}
