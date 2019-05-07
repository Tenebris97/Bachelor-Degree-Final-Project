using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductColor { get; set; }

        public string ProductWarranty { get; set; }

        public string ProductDescription { get; set; }

        public string ProductImage { get; set; }

        public string ProductBrand { get; set; }

        public int ProductStock { get; set; }

        public int ProductViews { get; set; }

        public int ProductLikeCount { get; set; }

        public int ProductPrice { get; set; }

        public int ProductDiscount { get; set; }
        /*=================================*/
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Categories { get; set; }
    }
}
