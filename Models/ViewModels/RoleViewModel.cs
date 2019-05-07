using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا عنوان نقش را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = ("توضیحات"))]
        [Required(ErrorMessage = "لطفا توضیحات نقش را وارد کنید")]
        public string Description { get; set; }

        public int NumberOfUsers { get; set; }
    }
}
