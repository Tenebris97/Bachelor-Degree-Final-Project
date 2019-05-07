using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class CellphoneAddEdit
    {
        [Key]
        public int CellphoneId { get; set; }

        [Display(Name = "نقد و بررسی")]
        [Required(ErrorMessage = "لطفا نقد و بررسی را وارد کنید")]
        public string Description { get; set; }

        [Display(Name = "ابعاد")]
        [Required(ErrorMessage = "لطفا ابعاد را وارد کنید")]
        public string Size { get; set; }

        [Display(Name = "وزن")]
        public int Weight { get; set; }

        [Display(Name = "توصیحات سیمکارت")]
        [Required(ErrorMessage = "لطفا توصیحات سیمکارت را وارد کنید")]
        public string SimcardDesc { get; set; }

        [Display(Name = "تعداد سیمکارت")]
        public int SimcardCount { get; set; }

        [Display(Name = "تراشه")]
        [Required(ErrorMessage = "لطفا تراشه را وارد کنید")]
        public string CpuChipset { get; set; }

        [Display(Name = "پردازنده‌ی مرکزی")]
        [Required(ErrorMessage = "لطفا پردازنده‌ی مرکزی را وارد کنید")]
        public string CpuCore { get; set; }

        [Display(Name = "نوع پردازنده")]
        [Required(ErrorMessage = "لطفا نوع پردازنده را وارد کنید")]
        public string CpuType { get; set; }

        [Display(Name = "فرکانس پردازنده‌ی مرکزی")]
        [Required(ErrorMessage = "لطفا فرکانس پردازنده‌ی مرکزی را وارد کنید")]
        public string CpuFrequency { get; set; }

        [Display(Name = "پردازنده‌ی گرافیکی")]
        [Required(ErrorMessage = "لطفا پردازنده‌ی گرافیکی را وارد کنید")]
        public string GPU { get; set; }

        //حافظه
        [Display(Name = "مقدار RAM")]
        [Required(ErrorMessage = "لطفا مقدار RAM را وارد کنید")]
        public int RAM { get; set; }

        [Display(Name = "حافظه داخلی")]
        [Required(ErrorMessage = "لطفا حافظه داخلی را وارد کنید")]
        public int Storage { get; set; }

        [Display(Name = "حداکثر ظرفیت کارت حافظه")]
        [Required(ErrorMessage = "لطفا حداکثر ظرفیت کارت حافظه را وارد کنید")]
        public int StorageSupport { get; set; }

        [Display(Name = "پشتیبانی از کارت حافظه جانبی")]
        [Required(ErrorMessage = "لطفا پشتیبانی از کارت حافظه جانبی را وارد کنید")]
        public string StorageType { get; set; }

        //صفحه نمایش
        [Display(Name = "نوع")]
        [Required(ErrorMessage = "لطفا نوع را وارد کنید")]
        public string ScreenType { get; set; }

        [Display(Name = "فناوری")]
        [Required(ErrorMessage = "لطفا فناوری را وارد کنید")]
        public string ScreenTechnology { get; set; }

        [Display(Name = "اندازه")]
        [Required(ErrorMessage = "لطفا اندازه را وارد کنید")]
        public string ScreenSize { get; set; }

        [Display(Name = "محافظ")]
        [Required(ErrorMessage = "لطفا محافظ را وارد کنید")]
        public string ScreenProtector { get; set; }

        [Display(Name = "شبکه‌های ارتباطی")]
        [Required(ErrorMessage = "لطفا شبکه‌های ارتباطی را وارد کنید")]
        public string ConnectionNetworks { get; set; }

        [Display(Name = "فن‌آوری‌های ارتباطی")]
        [Required(ErrorMessage = "لطفا فن‌آوری‌های ارتباطی را وارد کنید")]
        public string ConnectionTechnologies { get; set; }


        [Display(Name = "دوربین")]
        [Required(ErrorMessage = "لطفا دوربین را وارد کنید")]
        public string CameraHas { get; set; }

        [Display(Name = "رزولوشن عکس")]
        [Required(ErrorMessage = "لطفا رزولوشن عکس را وارد کنید")]
        public string CameraResolution { get; set; }

        [Display(Name = "فیلمبرداری")]
        [Required(ErrorMessage = "لطفا فیلمبرداری را وارد کنید")]
        public string CameraRecording { get; set; }

        [Display(Name = "سیستم عامل")]
        [Required(ErrorMessage = "لطفا سیستم عامل را وارد کنید")]
        public string OS { get; set; }

        [Display(Name = "نسخه سیستم عامل")]
        [Required(ErrorMessage = "لطفا نسخه سیستم عامل را وارد کنید")]
        public string OSVersion { get; set; }

        //Dropdown List
        [Display(Name = "کالا")]
        public int ProductId { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
