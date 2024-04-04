using System.ComponentModel.DataAnnotations;

namespace financeapp.Models.ViewModels;

public class NewExpenseViewModel
{
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    [MaxLength(512)]
    public string? Description { get; set; }
    [Required]
    [MaxLength(48)]
    public string Category { get; set; }
    [MaxLength(2)]
    public string? Icon { get; set; }
    [Required]
    public int AmountCents { get; set; }
}
