using System.ComponentModel.DataAnnotations;

namespace Framework.Validation
{
    public class LocalizedRequiredAttribute : RequiredAttribute, IValidationCodeProvider
    {
        public ValidationErrorEnum Error { get; } = ValidationErrorEnum.Required;

        public LocalizedRequiredAttribute()
        {
            this.ErrorMessageResourceType = typeof(ValidationMessages);
            this.ErrorMessageResourceName = nameof(ValidationMessages.Required);
        }

        public LocalizedRequiredAttribute(string resourceName)
        {
            this.ErrorMessageResourceType = typeof(ValidationMessages);
            this.ErrorMessageResourceName = resourceName;
        }
    }
}