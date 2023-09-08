#nullable disable

using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be maximum {1} characters!")]
        [MaxLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
