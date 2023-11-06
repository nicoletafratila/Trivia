using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.ViewModels
{
    public class AddUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [MinLength(6, ErrorMessage = "The Password field must be a minimum of 6 characters")]
        [NotMapped]
        public string? ConfirmPassword { get; set; }

        [Required]
        [Range(minimum:1, maximum:1, ErrorMessage = "Agreeing to Terms and Conditions is mandatory.")]
        public bool AreTaCsAggreedTo { get; set; }
    }
}
