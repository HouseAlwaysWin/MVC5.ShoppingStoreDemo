using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Data.ViewModels
{
    public class AudienceViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
