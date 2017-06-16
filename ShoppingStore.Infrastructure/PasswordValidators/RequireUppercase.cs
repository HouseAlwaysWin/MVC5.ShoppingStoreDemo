using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.PasswordValidators
{
    public class RequireUppercase : ValidationAttribute
    {
        public RequireUppercase()
        {
            ErrorMessageResourceName = "PasswordRequireUpper";
            ErrorMessageResourceType = typeof(Resource);
        }
        private bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        public override bool IsValid(object value)
        {
            return (string.IsNullOrWhiteSpace((string)value)) ||
                ((string)value).All(c => !IsUpper(c)) ? false : true;
        }
    }
}
