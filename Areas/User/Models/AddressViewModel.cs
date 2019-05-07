using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.User.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }

        [Display(Name = "آدرس کامل")]
        public string FullAddress { get; set; }

        [Display(Name = "کدپستی")]
        public string Postcode { get; set; }

        public string UserId { get; set; }
    }
}
