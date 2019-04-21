using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Areas.User.Models
{
    public class AddressAddEdit
    {
        [Key]
        public int AddressId { get; set; }

        [Display(Name = "آدرس کامل")]
        [Required(ErrorMessage = "لطفا آدرس را وارد کنید")]
        public string FullAddress { get; set; }

        [Display(Name = "کدپستی")]
        [Required(ErrorMessage = "لطفا کدپستی را وارد کنید")]
        public string Postcode { get; set; }

        public string UserId { get; set; }
    }
}
