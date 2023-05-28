
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnirWeb.Models
{
    public class RegisterUser
    {
        [Required]
        [Display(Name = "User name")]
        public String UserName { get; set; }


        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Doesnt match the password")]
        public String ConfirmPassword { get; set; }
    }
}
