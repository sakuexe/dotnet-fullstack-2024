using System.ComponentModel.DataAnnotations;

namespace forming.Models
{
	public class RegisterViewModel
	{
		[EmailAddress]  // use a decorator to validate the email
		[Required] // Make sure that the email is required
		public string Email { get; set; }
		[Required]
		[MinLength(3)] // Make sure that the password is at least 6 characters long
		[MaxLength(20)] // Make sure that the password is at most 20 characters long
		public string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Date)]
		public DateOnly Birthdate { get; set; }
	}
}