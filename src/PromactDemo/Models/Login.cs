using System.ComponentModel.DataAnnotations;

namespace PromactDemo.Models
{
    public class Login
    {
        [Required, Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
