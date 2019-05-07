using AutoMapper;
using FinalProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //میگیم این دوتا یکی هستن. اگه بجای همدیگه استفاده شدن گیر نده
            CreateMap<ProductAddEditModel, Product>();
            CreateMap<ProductAddEditModel, ProductListViewModel>();

            CreateMap<LaptopAddEdit, Laptop>();
            CreateMap<LaptopAddEdit, LaptopListViewModel>();

            CreateMap<CellphoneAddEdit, Cellphone>();
            CreateMap<CellphoneAddEdit, CellphoneListViewModel>();

            CreateMap<PromotionAddEdit, Promotion>();
            CreateMap<PromotionAddEdit, PromotionListViewModel>();
        }
    }
}
