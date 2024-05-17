using FansaDBox.data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace FansaDBox
{
    public class FilepathValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            if (!File.Exists((string)value))
            {
                return new ValidationResult(false,
                    "File not found.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
