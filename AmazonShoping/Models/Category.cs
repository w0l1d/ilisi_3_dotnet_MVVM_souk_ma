using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonShoping.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Type { get; set; }
        
        public long? ParentCategoryId { get; set; }
        [NotMapped]
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }
        
    }
}
