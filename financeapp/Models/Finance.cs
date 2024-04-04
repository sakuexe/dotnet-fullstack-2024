using System.ComponentModel.DataAnnotations;

namespace financeapp.Models;

public class Finance
{
    public int Id { get; set; }
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    [MaxLength(512)]
    public string Description { get; set; }
    [Required]
    [MaxLength(48)]
    public string Category { get; set; }
    [MaxLength(1)]
    public string Icon { get; set; }
    [Required]
    public int AmountCents { get; set; }
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    // Foreign Key - EF automatically recognizes this as a FK
    public int UserId { get; set; }
    // Navigation Property - used for easy access to related data
    public User User { get; set; }
}
