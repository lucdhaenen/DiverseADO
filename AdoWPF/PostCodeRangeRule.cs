using AdoGemeenschap;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace AdoWPF
{
    public class PostCodeRangeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Brouwer brouwer = (value as BindingGroup).Items[0] as Brouwer;
            if ((brouwer.Postcode < 1000) || (brouwer.Postcode > 9999))
            {
                return new ValidationResult(false, "Postcode moet tussen 1000 en 9999 zijn");
            }
            else { return ValidationResult.ValidResult; }
        }
    }
}
