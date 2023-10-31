using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonShoping.Models {
    public class Product {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required] public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Range(0, Double.MaxValue)][Required] public double Price { get; set; }

        [Range(0, int.MaxValue)][Required] public int Quantity { get; set; }

        [NotMapped] public IFormFile? Image { get; set; }

        // Foreign key property
        [Required] public long CategoryId { get; set; }

        // Navigation property
        [ForeignKey("CategoryId")][NotMapped] public Category? Category { get; set; }

        public string? ImageURL { get; set; }

    }
}