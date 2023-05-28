using FudbalskiTurnirWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace FudbalskiTurnirWeb.Common.Validation
{
    public class GoalAttribute : ValidationAttribute
    {
        //public GoalAttribute(int goals) => Goals = goals;

        public int Goals { get; }

        public string GetErrorMessage() =>
             $"Game can not have more than 5 goals.";

        protected override ValidationResult? IsValid(
       object? value, ValidationContext validationContext)
        {
            var match = (Match)validationContext.ObjectInstance;

            if(match.AwayTeamGoals + match.HomeTeamGoals > 5)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
   
    }

}