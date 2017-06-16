using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingStore.Infrastructure.LanguagesResources;
using ShoppingStore.Infrastructure.PasswordValidators;

namespace ShoppingStore.Data.ViewModels.AccountViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",
            ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName",
            ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [PasswordTooShort(5)]
        [Display(Name = "Password",
            ResourceType = typeof(Resource))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "ConfirmPassword",
            ResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }
    }
}
