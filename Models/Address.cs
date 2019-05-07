using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Display(Name = "آدرس کامل")]
        public string FullAddress { get; set; }

        [Display(Name = "کدپستی")]
        public string Postcode { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
