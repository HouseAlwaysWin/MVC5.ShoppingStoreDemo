using ShoppingStore.Infrastructure.LanguagesResources;
using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.Data.ViewModels.AccountViewModels
{

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "CurrentPassword",
            ResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "NewPassword",
            ResourceType = typeof(Resource))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword",
            ResourceType = typeof(Resource))]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}