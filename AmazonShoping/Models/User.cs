using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonShoping.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Required] public DateTime UpdatedAt { get; set; }

    [Required] public string FirstName { get; set; } = default!;

    [Required] public string LastName { get; set; } = default!;

    [Required] public string Email { get; set; } = default!;

    [Required] public string Username { get; set; } = default!;

    [Required] public string Password { get; set; } = default!;

    [Required] public string Phone { get; set; } = default!;

    [Required] public string Address { get; set; } = default!;

    [Required] public string City { get; set; } = default!;

    [Required] public string State { get; set; } = default!;

    [Required] public string Zip { get; set; } = default!;

    [Required] public string Country { get; set; } = default!;
}