using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.PasswordValidators
{

    public class RequireNonLetterOrDigit : ValidationAttribute
    {
        public RequireNonLetterOrDigit()
        {
            ErrorMessageResourceName = "PasswordRequireNonLetterOrDigit";
            ErrorMessageResourceType = typeof(Resource);
        }
        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private bool IsLetterOrDigit(char c)
        {
            return IsLower(c) && IsUpper(c) && IsDigit(c);
        }

        public override bool IsValid(object value)
        {
            return (string.IsNullOrWhiteSpace((string)value)) ||
                ((string)value).All(IsLetterOrDigit) ? false : true;

        }
    }


}
