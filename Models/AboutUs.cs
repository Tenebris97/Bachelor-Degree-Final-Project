using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class AboutUs
    {
        [Key]
        public int AboutId { get; set; }

        [Display(Name = "متن درباره ما")]
        public string FullText { get; set; }
    }
}
