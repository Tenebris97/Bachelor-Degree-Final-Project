using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class LaptopAddEdit
    {
        [Key]
        public int LaptopId { get; set; }

        [Display(Name = "نقد و بررسی")]
        [Required(ErrorMessage = "لطفا نقد و بررسی را وارد کنید")]
        public string Description { get; set; }

        //مشخصات فیزیکی
        [Display(Name = "ابعاد")]
        [Required(ErrorMessage = "لطفا ابعاد را وارد کنید")]
        public string Size { get; set; }

        [Display(Name = "وزن")]
        public string Weight { get; set; }

        //پردازنده مرکزی
        [Display(Name = "سازنده پردازنده")]
        [Required(ErrorMessage = "لطفا سازنده پردازنده را وارد کنید")]
        public string CpuManufactor { get; set; }

        [Display(Name = "سری پردازنده")]
        [Required(ErrorMessage = "لطفا سری را وارد کنید")]
        public string CpuSeries { get; set; }

        [Display(Name = "مدل پردازنده")]
        [Required(ErrorMessage = "لطفا مدل پردازنده را وارد کنید")]
        public string CpuType { get; set; }

        [Display(Name = "حافظه Cache")]
        public string CpuCache { get; set; }

        [Display(Name = "فرکانس پردازنده")]
        [Required(ErrorMessage = "لطفا فرکانس پردازنده را وارد کنید")]
        public string CpuFrequency { get; set; }

        //RAM
        [Display(Name = "ظرفیت حافظه RAM")]
        [Required(ErrorMessage = "لطفا ظرفیت حافظه RAM را وارد کنید")]
        public string RAM { get; set; }

        [Display(Name = "نوع حافظه RAM")]
        [Required(ErrorMessage = "لطفا نوع حافظه RAM را وارد کنید")]
        public string RAMType { get; set; }

        //حافظه داخلی
        [Display(Name = "ظرفیت حافظه داخلی")]
        [Required(ErrorMessage = "لطفا ظرفیت حافظه داخلی را وارد کنید")]
        public string Storage { get; set; }

        [Display(Name = "نوع حافظه داخلی")]
        [Required(ErrorMessage = "لطفا نوع حافظه داخلی را وارد کنید")]
        public string StorageType { get; set; }

        //پردازنده گرافیکی
        [Display(Name = "سازنده پردازنده گرافیکی")]
        [Required(ErrorMessage = "لطفا سازنده پردازنده گرافیکی را وارد کنید")]
        public string GPUManufactor { get; set; }

        [Display(Name = "مدل پردازنده گرافیکی")]
        [Required(ErrorMessage = "لطفا مدل پردازنده گرافیکی را وارد کنید")]
        public string GPUModel { get; set; }

        [Display(Name = "حافظه اختصاصی پردازنده گرافیکی")]
        [Required(ErrorMessage = "لطفا حافظه اختصاصی پردازنده گرافیکی را وارد کنید")]
        public string GPUSize { get; set; }

        //صفحه نمایش

        [Display(Name = "نوع صفحه نمایش")]
        [Required(ErrorMessage = "لطفا نوع صفحه نمایش را وارد کنید")]
        public string ScreenType { get; set; }
        [Display(Name = "دقت صفحه نمایش")]

        [Required(ErrorMessage = "لطفا دقت صفحه نمایش را وارد کنید")]
        public string ScreenTechnology { get; set; }

        [Display(Name = "اندازه صفحه نمایش")]
        [Required(ErrorMessage = "لطفا اندازه صفحه نمایش را وارد کنید")]
        public string ScreenSize { get; set; }

        //امکانات
        [Display(Name = "درایو نوری")]
        [Required(ErrorMessage = "لطفا درایو نوری را وارد کنید")]
        public string ODD { get; set; }

        [Display(Name = "وبکم")]
        [Required(ErrorMessage = "لطفا وبکم را وارد کنید")]
        public string Webcam { get; set; }

        [Display(Name = "مشخصات اسپیکر")]
        [Required(ErrorMessage = "لطفا مشخصات اسپیکر را وارد کنید")]
        public string Speaker { get; set; }

        [Display(Name = "مودم")]
        [Required(ErrorMessage = "لطفا مودم را وارد کنید")]
        public string Modem { get; set; }

        [Display(Name = "شبکه بی سیم Wi-Fi")]
        [Required(ErrorMessage = "لطفا شبکه بی سیم Wi-Fi را وارد کنید")]
        public string Wifi { get; set; }

        [Display(Name = "بلوتوث")]
        [Required(ErrorMessage = "لطفا بلوتوث را وارد کنید")]
        public string Bluetooth { get; set; }

        [Display(Name = "پورت HDMI")]
        [Required(ErrorMessage = "لطفا پورت HDMI را وارد کنید")]
        public string HDMI { get; set; }

        [Display(Name = "پورت VGA")]
        [Required(ErrorMessage = "لطفا پورت VGA را وارد کنید")]
        public string VGA { get; set; }

        [Display(Name = "تعداد پورت USB 3.0")]
        [Required(ErrorMessage = "لطفا تعداد پورت USB 3.0 را وارد کنید")]
        public string USB3 { get; set; }

        [Display(Name = "تعداد پورت USB 2.0")]
        [Required(ErrorMessage = "لطفا تعداد پورت USB 2.0 را وارد کنید")]
        public string USB2 { get; set; }

        //سایرمشخصات
        [Display(Name = "سیستم عامل")]
        [Required(ErrorMessage = "لطفا سیستم عامل را وارد کنید")]
        public string OS { get; set; }

        [Display(Name = "توضیحات سیستم عامل")]
        [Required(ErrorMessage = "لطفا توضیحات سیستم عامل را وارد کنید")]
        public string OSVersion { get; set; }

        [Display(Name = "نوع باتری")]
        [Required(ErrorMessage = "لطفا نوع باتری را وارد کنید")]
        public string BatteryType { get; set; }

        //Dropdown List
        [Display(Name = "کالا")]
        public int ProductId { get; set; }
        public List<SelectListItem> Products { get; set; }
    }
}
