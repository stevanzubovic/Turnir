using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnirWeb.Models
{
    public class Player : Entity
    {
        [Display(Name = "Team")]
        public int? TeamId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        public int JersyNumber { get; set; }

        public virtual Team? Team { get; set; }

        
    }
}
