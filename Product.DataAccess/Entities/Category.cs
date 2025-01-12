using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static ProductHub.Models.Constants.DataConstants;

namespace ProductHub.DataAccess.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        [Comment("Category Name")]
        public string Name { get; set; }=string.Empty;
        [Comment("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
