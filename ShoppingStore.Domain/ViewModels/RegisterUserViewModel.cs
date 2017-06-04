using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ShoppingStore.Domain.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string Email { get; set; }

        [Required]
        [Display(Name = "UserName",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "FirstName",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string LastName { get; set; }

        [Display(Name = "RoleName",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string RoleName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "ConfirmedPassword",
            ResourceType = typeof(LanguagesResources.Resource))]
        public string ConfirmedPassword { get; set; }
    }
}
