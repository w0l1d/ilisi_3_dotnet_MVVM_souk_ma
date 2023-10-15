using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonShoping.Models;

public class Order
{
    [Key]
    public long Id { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required]
    public DateTime UpdatedAt { get; set; }

    [Required]
    public DateTime checkoutAt { get; set; }

    [Required]
    public string OrderStatus { get; set; } = default!;

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
    public decimal Total { get; set; }

    [Required]
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


}