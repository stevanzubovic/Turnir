using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnirWeb.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }

        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Updated at")]
        public DateTime? UpdatedAt { get; set; }

        [Display(Name = "Deleted at")]
        public DateTime? DeletedAt { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
    }
}
