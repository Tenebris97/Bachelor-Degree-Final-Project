using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Cellphone
    {
        [Key]
        public int CellphoneId { get; set; }

        public string Description { get; set; }
        public string Size { get; set; }
        public int Weight { get; set; }
        public string SimcardDesc { get; set; }
        public int SimcardCount { get; set; }

        public string CpuChipset { get; set; }
        public string CpuCore { get; set; }
        public string CpuType { get; set; }
        public string CpuFrequency { get; set; }
        public string GPU { get; set; }

        public int RAM { get; set; }
        public int Storage { get; set; }
        public int StorageSupport { get; set; }
        public string StorageType { get; set; }

        public string ScreenType { get; set; }
        public string ScreenTechnology { get; set; }
        public string ScreenSize { get; set; }
        public string ScreenProtector { get; set; }

        public string ConnectionNetworks { get; set; }
        public string ConnectionTechnologies { get; set; }

        public string CameraHas { get; set; }
        public string CameraResolution { get; set; }
        public string CameraRecording { get; set; }

        public string OS { get; set; }
        public string OSVersion { get; set; }
        /*=================================*/
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}
