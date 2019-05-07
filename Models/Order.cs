using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public byte Flag { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public int Price { get; set; }
        public int AddressId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }
    }
}
