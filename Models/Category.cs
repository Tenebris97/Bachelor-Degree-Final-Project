using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Display(Name = "نام دسته‌بندی")]
        [Required(ErrorMessage = "لطفا نام دسته‌بندی را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "توضیحات دسته‌بندی")]
        [Required(ErrorMessage = "لطفا توضیحات دسته‌بندی را وارد کنید")]
        public string Description { get; set; }

    }
}
