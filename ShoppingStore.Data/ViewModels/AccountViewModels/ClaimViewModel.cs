using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Data.ViewModels.AccountViewModels
{
    public class ClaimViewModel
    {
        [Required]
        public string UserId { get; set; }
        public List<ClaimModel> Claims { get; set; }


    }

    public class ClaimModel
    {
        [Required]
        public string ClaimType { get; set; }
        [Required]
        public string ClaimValue { get; set; }
    }
}
