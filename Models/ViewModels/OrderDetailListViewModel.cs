using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class OrderDetailListViewModel
    {
        [Display(Name = "شماره سفارش")]
        public int OrderId { get; set; }

        public int OrderDetailsId { get; set; }

        [Display(Name = "نام محصول")]
        public string ProductName { get; set; }

        [Display(Name = "وضعیت")]
        public byte Status { get; set; }

        [Display(Name = "تاریخ دریافت")]
        public string DeliveryDate { get; set; }

        [Display(Name = "تاریخ ثبت سفارش")]
        public string OrderDate { get; set; }

        [Display(Name = "قیمیت نهایی")]
        public int Price { get; set; }

        [Display(Name = "قیمت")]
        public int ProductPrice { get; set; }

        [Display(Name = "تخفیف")]
        public int Discount { get; set; }

        [Display(Name = "نام مشتری")]
        public string CustomerName { get; set; }

        public int ProductId { get; set; }
    }
}
