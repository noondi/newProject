using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace newProject.Models
{
    public class FutureDateAttribute : ValidationAttribute, IClientModelValidator
    {
        private DateTime _today;

        public void AddValidation(ClientModelValidationContext context)
        {
            // throw new NotImplementedException();
        }

        public FutureDateAttribute()
        {
            _today = DateTime.Today;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Activity activity = (Activity)validationContext.ObjectInstance;

            if (activity.ActivityDate < _today)
            {
                return new ValidationResult("Date must be in the future.");
            }

            return ValidationResult.Success;
        }
    }
}