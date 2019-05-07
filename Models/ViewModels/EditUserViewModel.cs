using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید")]
        public string LastName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        //[EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نیست")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "ایمیل وارد شده معتبر نیست")]
        public string Email { get; set; }

        ///For showing roles in dropdown list
        public List<SelectListItem> ApplicationRoles { get; set; }
        [Display(Name = "نقش")]
        public string ApplicationRoleId { get; set; }

        [Display(Name = "جنسیت")]
        public byte Gender { get; set; }
    }
}
