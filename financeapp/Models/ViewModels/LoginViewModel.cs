using System.ComponentModel.DataAnnotations;
using financeapp.Data;

namespace financeapp.Models.ViewModels;

public class LoginViewModel
{
    // the attributes are the same as in Models/User.cs
    [Required]
    [MinLength(3)]
    [DataType(DataType.Text)]
    public string Username { get; set; }
    // the attributes are the same as in Models/User.cs
    [Required]
    [MinLength(8)]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    // check if a user with the given username and password exists
    // in the database, return true if so
    public bool ValidateLogin(FinancesContext context)
    {
        try
        {
            using (context)
            {
                var user = context.Users.SingleOrDefault(u => u.Username == Username && u.Password == Password);
                return user != null;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}
