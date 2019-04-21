using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class ProductAddEditModel
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "لطفا نام کالا را وارد کنید")]
        public string ProductName { get; set; }

        [Display(Name = "رنگ")]
        public string ProductColor { get; set; }

        [Display(Name = "گارانتی")]
        public string ProductWarranty { get; set; }

        [Display(Name = "توضیحات کالا")]
        [Required(ErrorMessage = "لطفا توضیحات کالا را وارد کنید")]
        public string ProductDescription { get; set; }

        [Display(Name = "تصویر کالا")]       
        public string ProductImage { get; set; }

        [Display(Name = "برند کالا")]
        [Required(ErrorMessage = "لطفا برند کالا را وارد کنید")]
        public string ProductBrand { get; set; }

        [Display(Name = "تعداد کالا")]
        [Required(ErrorMessage = "لطفا تعداد کالا را وارد کنید")]
        public int ProductStock { get; set; }

        [Display(Name = "قیمت کالا")]
        [Required(ErrorMessage = "لطفا قیمت کالا را وارد کنید")]
        public int ProductPrice { get; set; }

        [Display(Name = "تخفیف کالا")]
        public int ProductDiscount { get; set; }

        public int ProductViews { get; set; }

        //Dropdown List
        [Display(Name = "دسته‌بندی کالا")]
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}
