using System.ComponentModel.DataAnnotations;

namespace restingapi.Models
{
    public class Credentials
    {
        [Required]
        [StringLength(48, MinimumLength = 3)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [StringLength(48)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
