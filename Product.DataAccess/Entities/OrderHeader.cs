using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static ProductHub.Models.Constants.DataConstants;

namespace ProductHub.DataAccess.Entities
{
    public class OrderHeader
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        [Comment("Order Date")]
        public DateTime OrderDate { get; set; }

        [Comment("Shipping Date")]
        public DateTime ShippingDate { get; set; }

        [Comment("Order Total")]
        public double OrderTotal { get; set; }

        [Comment("Order Status")]
        public string? OrderStatus { get; set; }


        [Comment("PaymentStatus")]
        public string? PaymentStatus { get; set; }

        [Comment("Tracking Number ")]
        public string? TrackingNumber { get; set; }

        [Comment("Carrier")]
        public string? Carrier { get; set; }

        [Comment("Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Comment("Payment DueDate")]
        public DateOnly PaymentDueDate { get; set; }

        [Comment("Payment IntentId")]
        public string? PaymentIntentId { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("User full name")]
        public string Name { get; set; } = string.Empty;

        [Comment("Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(StreetMaxLength)]
        [Comment("User street address")]
        public string StreetAddress { get; set; } = null!;

        [MaxLength(CityMaxLength)]
        [Comment("User city")]
        public string City { get; set; } = null!;

        [MaxLength(PostCodeMaxLength)]
        [Comment("User postal code")]
        public string PostalCode { get; set; } = null!;
    }
}
