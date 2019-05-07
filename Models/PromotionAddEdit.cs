using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class PromotionAddEdit
    {
        [Key]
        public int PromotionId { get; set; }
        //Dropdown List
        [Display(Name = "کالا")]
        public int ProductId { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
