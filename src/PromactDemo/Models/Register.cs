using System.ComponentModel.DataAnnotations;

namespace PromactDemo.Models
{
    public class Register
    {
        [Required, Display(Name = "User Name"), MinLength(5), MaxLength(100)]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password)), Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
