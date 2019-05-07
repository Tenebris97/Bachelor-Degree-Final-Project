using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class News
    {
        [Key]
        public int NewsId { get; set; }

        [Display(Name = "عنوان اطلاعیه")]
        [Required(ErrorMessage = "لطفا عنوان را وارد نمایید")]
        public string NewsTitle { get; set; }

        [Display(Name = "متن اطلاعیه")]
        [Required(ErrorMessage = "لطفا متن را وارد نمایید")]
        public string NewsContent { get; set; }


        [Display(Name = "تاریخ اطلاعیه")]
        public string NewsDate { get; set; }

        [Display(Name = "تصویر")]
        public string NewsImage { get; set; }
    }
}
