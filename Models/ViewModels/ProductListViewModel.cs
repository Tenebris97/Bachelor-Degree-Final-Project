using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.ViewModels
{
    public class ProductListViewModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [Display(Name = "نام")]
        public string ProductName { get; set; }

        [Display(Name = "رنگ")]
        public string ProductColor{ get; set; }

        [Display(Name = "گارانتی")]
        public string ProductWarranty { get; set; }

        [Display(Name = "توضیحات")]
        public string ProductDescription { get; set; }

        [Display(Name = "تصویر")]
        public string ProductImage { get; set; }

        [Display(Name = "برند")]
        public string ProductBrand { get; set; }

        [Display(Name = "دسته‌بندی")]
        public string ProductCategory { get; set; }

        [Display(Name = "تعداد")]
        public int ProductStock { get; set; }

        [Display(Name = "تعداد مشاهده")]
        public int ProductViews { get; set; }

        [Display(Name = "امتیاز")]
        public int ProductLikeCount { get; set; }

        [Display(Name = "قیمت")]
        public int ProductPrice { get; set; }

        [Display(Name = "تخفیف")]
        public int ProductDiscount { get; set; }
    }
}
