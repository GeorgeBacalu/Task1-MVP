using System.Globalization;
using System.Windows.Controls;

namespace Mvp1.Project.Validations
{
    public class WordNameValidation : ValidationRule
    {
        public override ValidationResult Validate(object name, CultureInfo cultureInfo) =>
            string.IsNullOrWhiteSpace(name as string) ? new ValidationResult(false, "Name is required!") : ValidationResult.ValidResult;
    }
}