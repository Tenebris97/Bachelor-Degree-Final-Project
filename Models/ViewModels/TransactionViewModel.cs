using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class TransactionViewModel
    {
        [Display(Name = "شماره تراکنش")]
        public string TransactionNo { get; set; }

        [Display(Name = "تاریخ تراکنش")]
        public string TransactionDate { get; set; }

        [Display(Name = "زمان تراکنش")]
        public string TransactionTime { get; set; }

        [Display(Name = "مبلغ تراکنش")]
        public int Amount { get; set; }

        [Display(Name = "نام مشتری")]
        public string CustomerName { get; set; }

        [Display(Name = "شماره سفارش")]
        public int OrderId { get; set; }

    }
}
