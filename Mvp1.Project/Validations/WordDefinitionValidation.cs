using System.Globalization;
using System.Windows.Controls;

namespace Mvp1.Project.Validations
{
    public class WordDefinitionValidation : ValidationRule
    {
        public override ValidationResult Validate(object definition, CultureInfo cultureInfo) =>
            string.IsNullOrWhiteSpace(definition as string) ? new ValidationResult(false, "Definition is required!") : ValidationResult.ValidResult;
    }
}