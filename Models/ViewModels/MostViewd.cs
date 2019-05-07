using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class MostViewd
    {
        public int ProductId { get; set; }
        [Display(Name = "نام کالا")]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public int Price { get; set; }
    }
}
