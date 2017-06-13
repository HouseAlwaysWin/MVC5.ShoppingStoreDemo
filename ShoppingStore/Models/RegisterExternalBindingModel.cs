using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ShoppingStore.Models
{

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}