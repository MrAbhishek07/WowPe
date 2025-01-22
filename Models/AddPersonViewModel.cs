using System.ComponentModel.DataAnnotations;

namespace WowPay.Models
{
    public class AddPersonViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [MaxLength(10 )]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be numeric and up to 10 digits.")]
        public string Phone { get; set; }

        public string Address { get; set; }
    }
}
