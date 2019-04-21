using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Laptop
    {
        [Key]
        public int LaptopId { get; set; }

        public string Description { get; set; }

        //مشخصات فیزیکی
        public string Size { get; set; }
        public int Weight { get; set; }

        //پردازنده مرکزی
        public string CpuManufactor { get; set; }
        public string CpuSeries { get; set; }
        public string CpuType { get; set; }
        public int CpuCache { get; set; }
        public string CpuFrequency { get; set; }

        //RAM
        public int RAM { get; set; }
        public string RAMType { get; set; }

        //حافظه داخلی
        public string Storage { get; set; }
        public string StorageType { get; set; }

        //پردازنده گرافیکی
        public string GPUManufactor { get; set; }
        public string GPUModel { get; set; }
        public string GPUSize { get; set; }

        //صفحه نمایش
        public string ScreenType { get; set; }
        public string ScreenTechnology { get; set; }
        public string ScreenSize { get; set; }

        //امکانات
        public string ODD { get; set; }
        public string Webcam { get; set; }
        public string Speaker { get; set; }
        public string Modem { get; set; }
        public string Wifi { get; set; }
        public string Bluetooth { get; set; }
        public string HDMI { get; set; }
        public string VGA { get; set; }
        public string USB3 { get; set; }
        public string USB2 { get; set; }

        //سایرمشخصات
        public string OS { get; set; }
        public string OSVersion { get; set; }
        public string BatteryType { get; set; }

        /*=================================*/
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}
