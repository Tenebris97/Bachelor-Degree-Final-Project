using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class MultiModels
    {
        public List<ApplicationUser> UserList { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public List<News> LastNews { get; set; }
        public List<Laptop> Laptop { get; set; }
        public List<Cellphone> Cellphone { get; set; }
        public List<Product> Products { get; set; }
        public List<PromotionListViewModel> PromotionsListViewModel { get; set; }
        public Product Product { get; set; }
        public CellphoneDetailViewModel CellphoneDetailViewModel { get; set; }
        public LaptopDetailViewModel LaptopDetailViewModel { get; set; }
        public List<CommentListViewModel> Comments { get; set; }
        public List<Product> SearchedProduct { get; set; }
        public List<Address> AllAddresses { get; set; }
        public Address Address { get; set; }
        public Transaction Transaction { get; set; }

    }
}
