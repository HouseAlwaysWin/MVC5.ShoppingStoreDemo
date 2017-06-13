using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.Data.ViewModels.AccountViewModels
{

    public class RegisterExternalViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}