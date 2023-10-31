using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonShoping.Models; 

public class OrderItem {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public long ProductId { get; set; }

    [Required]
    public long OrderId { get; set; }

    [ForeignKey("OrderId")]
    [NotMapped]
    public Order Order { get; set; }

    
    [ForeignKey("ProductId")]
    [NotMapped]
    public Product Product { get; set; }
}