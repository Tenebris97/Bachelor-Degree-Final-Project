using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class PromotionListViewModel
    {
        public int PromotionId { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "نام کالا")]
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        [Display(Name = "دسته‌بندی")]
        public string Category { get; set; }
    }
}
