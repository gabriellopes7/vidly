using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Min18YearsIfAMember:ValidationAttribute

    {
        //Custom Validation
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipType.PayAsYouGo || customer.MembershipTypeId == MembershipType.Unknown)            
                return ValidationResult.Success;
            if (customer.BirthDate == null)
                return new ValidationResult("Birth date is required.");
            
            var age = DateTime.Today.Year - customer.BirthDate.Value.Year; //Calculo de idade é mais complicado que isto

            return (age >= 18 ? 
                ValidationResult.Success : 
                new ValidationResult("You need to be over the age of 18 to subscribe to this membership"));

        }
    }
}