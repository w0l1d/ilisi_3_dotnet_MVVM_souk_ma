using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonShoping.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime UpdatedAt { get; set; }

    [Required]
    public DateTime checkoutAt { get; set; }

    [Required]
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    [Required]
    public string OrderNumber { get; set; } = default!;

    [Required]
    public string UserId { get; set; } = default!;

    [Required]
    public string UserName { get; set; } = default!;

    [Required]
    public string UserAddress { get; set; } = default!;

    [Required]
    public string UserPhone { get; set; } = default!;

    [Required]
    public string UserEmail { get; set; } = default!;

    [Required]
    public string PaymentTransactionId { get; set; } = default!;

    [Required]
    [NotMapped]
    public decimal Total { get; set; }

    [Required]
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

}

//add order status enum
public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}
