using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ApplicationDbContext context, IServiceProvider iServiceProvider,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = (from p in _context.Order
                         join u in _context.Users on p.UserId equals u.Id
                         select new OrderListViewModel()
                         {
                             OrderId = p.OrderId,
                             OrderDate = p.OrderDate,
                             DeliveryDate = p.DeliveryDate,
                             Price = p.Price,
                             Status = p.Flag,
                             CustomerName = u.FirstName + " " + u.LastName
                         }).AsNoTracking().OrderBy(p => p.OrderId);

            var modelPaging = await PagingList.CreateAsync(query, 10, page);
            return View(modelPaging);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("NotFounds");
            }

            var model = (from order in _context.Order
                         join orderDetails in _context.OrderDetails on order.OrderId equals orderDetails.OrderId
                         join product in _context.products on orderDetails.ProductId equals product.ProductId
                         join user in _context.Users on orderDetails.UserId equals user.Id
                         where order.OrderId == id
                         select new OrderDetailListViewModel()
                         {
                             OrderId = order.OrderId,
                             ProductName = product.ProductName,
                             Price = order.Price,
                             ProductPrice = product.ProductPrice,
                             DeliveryDate = order.DeliveryDate,
                             OrderDate = order.OrderDate,
                             Status = order.Flag,
                             Discount = product.ProductDiscount,
                             ProductId = product.ProductId,
                             CustomerName = user.FirstName + " " + user.LastName
                         }).ToList();

            return View(model);
        }
    }
}