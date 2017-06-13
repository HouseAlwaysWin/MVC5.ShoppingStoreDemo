using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.Data.ViewModels.AccountViewModels
{
    // Models used as parameters to AccountController actions.

    public class AddExternalLoginViewModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}