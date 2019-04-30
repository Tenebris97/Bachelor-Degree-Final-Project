using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public string TransactionNo { get; set; }

        [Display(Name = "تاریخ تراکنش")]
        public string TransactionDate { get; set; }

        [Display(Name = "زمان تراکنش")]
        public string TransactionTime { get; set; }

        [Display(Name = "مبلغ تراکنش")]
        public int Amount { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Orders { get; set; }
    }
}
