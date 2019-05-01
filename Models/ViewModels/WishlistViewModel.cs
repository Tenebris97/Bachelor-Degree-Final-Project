using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class WishlistViewModel
    {
        
        public int ProductId { get; set; }
        public int WhishId { get; set; }

        [Display(Name = "نام کالا")]
        public string ProductName { get; set; }

    }
}
