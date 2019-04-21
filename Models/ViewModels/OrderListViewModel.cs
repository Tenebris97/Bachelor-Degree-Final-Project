using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class OrderListViewModel
    {
        [Display(Name = "شماره سفارش")]
        public int OrderId { get; set; }

        [Display(Name = "وضعیت")]
        public byte Status { get; set; }

        [Display(Name = "تاریخ تحویل")]
        public string DeliveryDate { get; set; }

        [Display(Name = "تاریخ ثبت سفارش")]
        public string OrderDate { get; set; }

        [Display(Name = "قیمیت نهایی")]
        public int Price { get; set; }
    }
}
