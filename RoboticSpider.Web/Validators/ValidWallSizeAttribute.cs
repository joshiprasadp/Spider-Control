using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using RoboticSpider.Application;
using RoboticSpider.Application.Helpers;
using RoboticSpider.Domain.Entities;

namespace RoboticSpider.Web.Validators
{
    public class ValidWallSizeAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var size = (string) value;

            var parsedWallSize = InputParser.ParseWallSize(size);
            if (parsedWallSize.Length == 0)
            {
                return new ValidationResult("Invalid wall size");
            }


            var wallResult = Wall.Create(parsedWallSize);
            if (wallResult.IsFailure)
            {
                return new ValidationResult(wallResult.Error);
            }
            

            return ValidationResult.Success;
        }
    }
}