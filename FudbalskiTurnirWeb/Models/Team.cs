using System.ComponentModel.DataAnnotations.Schema;

namespace FudbalskiTurnirWeb.Models
{
    public class Team : Entity
    {
        [Column(TypeName = "nvarchar(60)")]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; } = new List<Player>();

        public virtual List<Match>? AwayTeams { get; set; }

        public List<Match>? HomeTeams { get; set; }

    }
}
