using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "نام‌کاربری")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "شماره تلفن ثابت")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        public string Firstname { get; set; }

        [Display(Name = "نام‌خانوادگی")]
        public string Lastname { get; set; }

        [Display(Name = "کدملی")]
        public string NationalCode { get; set; }

        [Display(Name = "شماره موبایل")]
        public string MobileNumber { get; set; }


        [Display(Name = "تاریخ تولد")]
        public string Birthdate { get; set; }

        [Display(Name = "جنسیت")]
        public byte Gender { get; set; }

        public string StatusMessage { get; set; }
    }
}
