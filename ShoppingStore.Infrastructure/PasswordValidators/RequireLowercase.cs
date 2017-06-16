using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.PasswordValidators
{
    public class RequireLowercase : ValidationAttribute
    {

        public RequireLowercase()
        {
            ErrorMessageResourceName = "PasswordRequireLower";
            ErrorMessageResourceType = typeof(Resource);
        }
        private bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }
        public override bool IsValid(object value)
        {
            return (string.IsNullOrWhiteSpace((string)value)) ||
                ((string)value).All(c => !IsLower(c)) ? false : true;
        }
    }
}
