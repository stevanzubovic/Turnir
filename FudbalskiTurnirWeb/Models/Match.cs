using FudbalskiTurnirWeb.Common.Validation;
using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnirWeb.Models
{
    public class Match
    {
        public int Id { get; set; }

        [Display(Name = "Home team")]
        public int HomeTeamId { get; set; }

        [Display(Name = "Away team")]
        public int AwayTeamId { get; set; }
        public virtual Team? HomeTeam { get; set; }

        public virtual Team? AwayTeam { get; set; }

        [Display(Name = "Home team goals")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of goals must be greater than zero")]
        public int? HomeTeamGoals { get; set; }

        [GoalAttribute]
        [Display(Name = "Away team goals")]
        [Range(0, int.MaxValue, ErrorMessage = "Number of goals must be greater than zero")]
        public int? AwayTeamGoals { get;set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
