using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace ProductHub.DataAccess.Entities
{
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public int OrderHeaderId {  get; set; }
        [ForeignKey(nameof(OrderHeaderId))]
        [Required]
        public OrderHeader OrderHeader { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [Required]
        public Product Product { get; set; } = null!;

        [Comment("Count")]
        public int Count {  get; set; }

        [Comment("Price")]
        public double Price {  get; set; }

    }
}
