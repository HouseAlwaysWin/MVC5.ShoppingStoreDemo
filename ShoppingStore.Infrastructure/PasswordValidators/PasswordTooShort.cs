using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.PasswordValidators
{
    public class PasswordTooShort : ValidationAttribute
    {
        public int RequiredLength { get; set; }

        public PasswordTooShort(int requiredLength)
        {
            RequiredLength = requiredLength;
            ErrorMessageResourceName = "PasswordTooShort";
            ErrorMessageResourceType = typeof(Resource);
        }

        // Override ErrorMessage properties to Resources
        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture, ErrorMessageString, RequiredLength);
        }

        public override bool IsValid(object value)
        {
            return (string.IsNullOrWhiteSpace((string)value) ||
                ((string)value).Length < RequiredLength) ? false : true;
        }
    }
}
