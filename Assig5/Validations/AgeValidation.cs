using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Assig5.Validations
{

    public class AgeValidation : ValidationRule
    {
        int min;
        int max;

        public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numVal = 0;
            if (!int.TryParse(value.ToString(), out numVal))
            {
                return new ValidationResult(false, "Wrong age");
            }

            if (numVal < min || numVal > max)
            {
                return new ValidationResult(false, "Out of Range. It neds to be between 16 and 60.");
            }

            return ValidationResult.ValidResult;
        }
    }
}