using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using financeapp.Data;
using Microsoft.EntityFrameworkCore;

namespace financeapp.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    [Display(Name = "Username")]
    [DataType(DataType.Text)]
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    // validate the password based on if it matches the password field
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    public string SaveToDatabase(FinancesContext context)
    {
        try
        {
            var transaction = context.Database.BeginTransaction();
            context.Users.Add(new User
            {
                Username = Username,
                Email = Email,
                Password = Password
            });
            context.SaveChanges();
            transaction.Commit();
        }
        catch (DbUpdateException e)
        {
            // check for error that came because of unique constraint violation
            if (!e.InnerException?.Message.Contains("UNIQUE") ?? false)
            {
                // log the error
                Debug.WriteLine(e.InnerException?.Message ?? e.Message);
                return "Error creating user";
            }
            // do not tell the user if it was the username or email that created
            // the unique constraint violation. For the sake of security.
            return "Account with that username or email already exists";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return e.Message;
        }
        // if no exception was thrown, return an empty string
        return string.Empty;
    }
}
