using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SeekYourCareer
{
    public class MinimumValueAttribute:ValidationAttribute
    {
        private int m_minimumValue;

    public MinimumValueAttribute(int value)
    {
        m_minimumValue = value;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null) // lets check if we have some value
        {
            if (value is DateTime) // check if it is a valid integer
            {
                DateTime suppliedValue = (DateTime)value;
                DateTime currDate=DateTime.Now;

                int extra=0;
                if((currDate.Month > suppliedValue.Month))
                {extra=1;}
                else if((currDate.Month == suppliedValue.Month) && (currDate.Day >= suppliedValue.Day)){
                    extra=1;
                }
                int diff=(currDate.Year - suppliedValue.Year - 1)+extra;
                if (diff < m_minimumValue)
                {
                    // let the user know about the validation error
                    return new ValidationResult("Minimum value for this field should be " + m_minimumValue);
                }
            }
        }

        return ValidationResult.Success;
    }
}
    }
