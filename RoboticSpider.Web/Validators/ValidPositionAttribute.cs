using System.ComponentModel.DataAnnotations;
using RoboticSpider.Application;
using RoboticSpider.Application.Helpers;
using RoboticSpider.Domain.Entities;

namespace RoboticSpider.Web.Validators
{
    public class ValidPositionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var positionInput = InputParser.ParsePositionInput((string) value);
            if (!string.IsNullOrEmpty(positionInput.Item4))
            {
                return new ValidationResult(positionInput.Item4);
            }
            var positionResult = Position.Create(positionInput.Item1, positionInput.Item2, positionInput.Item3);
            if (positionResult.IsFailure)
            {
                return new ValidationResult(positionResult.Error);
            }
            return ValidationResult.Success;
        }
    }
}