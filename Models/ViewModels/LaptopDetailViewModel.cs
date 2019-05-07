using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class LaptopDetailViewModel
    {
        public int ProductId { get; set; }
        public int LaptopId { get; set; }

        [Display(Name = "نام کالا")]
        public string Name { get; set; }

        [Display(Name = "نقد و بررسی")]
        public string Description { get; set; }

        //مشخصات فیزیکی
        [Display(Name = "ابعاد")]
        public string Size { get; set; }

        [Display(Name = "وزن")]
        public string Weight { get; set; }

        //پردازنده مرکزی
        [Display(Name = "سازنده پردازنده")]
        public string CpuManufactor { get; set; }

        [Display(Name = "سری پردازنده")]
        public string CpuSeries { get; set; }

        [Display(Name = "مدل پردازنده")]
        public string CpuType { get; set; }

        [Display(Name = "حافظه Cache")]
        public string CpuCache { get; set; }

        [Display(Name = "فرکانس پردازنده")]
        public string CpuFrequency { get; set; }

        //RAM
        [Display(Name = "ظرفیت حافظه RAM")]
        public string RAM { get; set; }

        [Display(Name = "نوع حافظه RAM")]
        public string RAMType { get; set; }

        //حافظه داخلی
        [Display(Name = "ظرفیت حافظه داخلی")]
        public string Storage { get; set; }

        [Display(Name = "نوع حافظه داخلی")]
        public string StorageType { get; set; }

        //پردازنده گرافیکی
        [Display(Name = "سازنده پردازنده گرافیکی")]
        public string GPUManufactor { get; set; }

        [Display(Name = "مدل پردازنده گرافیکی")]
        public string GPUModel { get; set; }

        [Display(Name = "حافظه اختصاصی پردازنده گرافیکی")]
        public string GPUSize { get; set; }

        //صفحه نمایش
        [Display(Name = "نوع صفحه نمایش")]
        public string ScreenType { get; set; }

        [Display(Name = "دقت صفحه نمایش")]
        public string ScreenTechnology { get; set; }

        [Display(Name = "اندازه صفحه نمایش")]
        public string ScreenSize { get; set; }

        //امکانات
        [Display(Name = "درایو نوری")]
        public string ODD { get; set; }

        [Display(Name = "وبکم")]
        public string Webcam { get; set; }

        [Display(Name = "مشخصات اسپیکر")]
        public string Speaker { get; set; }

        [Display(Name = "مودم")]
        public string Modem { get; set; }

        [Display(Name = "شبکه بی سیم Wi-Fi")]
        public string Wifi { get; set; }

        [Display(Name = "بلوتوث")]
        public string Bluetooth { get; set; }

        [Display(Name = "پورت HDMI")]
        public string HDMI { get; set; }

        [Display(Name = "پورت VGA")]
        public string VGA { get; set; }

        [Display(Name = "تعداد پورت USB 3.0")]
        public string USB3 { get; set; }

        [Display(Name = "تعداد پورت USB 2.0")]
        public string USB2 { get; set; }

        //سایرمشخصات
        [Display(Name = "سیستم عامل")]
        public string OS { get; set; }

        [Display(Name = "توضیحات سیستم عامل")]
        public string OSVersion { get; set; }

        [Display(Name = "نوع باتری")]
        public string BatteryType { get; set; }

        /*************************************/

        [Display(Name = "نام")]
        public string ProductName { get; set; }

        [Display(Name = "تصویر")]
        public string ProductImage { get; set; }

        [Display(Name = "برند")]
        public string ProductBrand { get; set; }

        [Display(Name = "دسته‌بندی")]
        public string ProductCategory { get; set; }

        [Display(Name = "تعداد")]
        public int ProductStock { get; set; }

        [Display(Name = "تعداد مشاهده")]
        public int ProductViews { get; set; }

        [Display(Name = "امتیاز")]
        public int ProductLikeCount { get; set; }

        [Display(Name = "قیمت")]
        public int ProductPrice { get; set; }

        [Display(Name = "تخفیف")]
        public int ProductDiscount { get; set; }

        [Display(Name = "رنگ")]
        public string ProductColor { get; set; }

        [Display(Name = "گارانتی")]
        public string ProductWarranty { get; set; }

        public string ProductDescription { get; set; }
    }
}
