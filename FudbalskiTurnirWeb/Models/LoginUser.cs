using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnirWeb.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } 
    }
}
