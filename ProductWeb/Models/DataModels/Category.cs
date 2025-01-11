using System.ComponentModel.DataAnnotations;
using static ProductWeb.Models.DataConstants;

namespace ProductWeb.Models.DataModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }=string.Empty;
        public int DisplayOrder { get; set; }
    }
}
