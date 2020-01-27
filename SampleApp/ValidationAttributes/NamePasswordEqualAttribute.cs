using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.Models;

namespace SampleApp.ValidationAttributes
{
    public class NamePasswordEqualAttribute:ValidationAttribute
    {
        public NamePasswordEqualAttribute()
        {
            ErrorMessage = "Имя и пароль не совпадают";
        }

        public override bool IsValid(object value)
        {
            var person = value as Person;

            if (person.Name == person.Password)
            {
                return false;
            }
            return true;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}
