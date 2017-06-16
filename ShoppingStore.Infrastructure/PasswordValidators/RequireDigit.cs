using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.PasswordValidators
{
    public class RequireDigit : ValidationAttribute
    {
        public RequireDigit()
        {
            ErrorMessageResourceName = "PasswordRequireDigit";
            ErrorMessageResourceType = typeof(Resource);
        }
        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }
        public override bool IsValid(object value)
        {
            return (string.IsNullOrWhiteSpace((string)value)) ||
                ((string)value).All(c => !IsDigit(c)) ? false : true;

        }
    }
}
