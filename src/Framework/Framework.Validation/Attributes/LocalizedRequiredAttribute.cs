using System.ComponentModel.DataAnnotations;

namespace Framework.Validation
{
    public class LocalizedRequiredAttribute : RequiredAttribute
    {
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