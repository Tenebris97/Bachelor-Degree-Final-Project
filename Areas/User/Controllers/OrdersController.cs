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

namespace FinalProject.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Policy = "RequireMemberRole")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _iServiceProvider;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider iServiceProvider)
        {
            _context = context;
            _userManager = userManager;
            _iServiceProvider = iServiceProvider;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var query = (from p in _context.Order
                         where p.UserId == _userManager.GetUserId(User)
                         select new OrderListViewModel()
                         {
                             OrderId = p.OrderId,
                             OrderDate = p.OrderDate,
                             DeliveryDate = p.DeliveryDate,
                             Price = p.Price,
                             Status = p.Flag
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
                         where order.UserId == _userManager.GetUserId(User) && orderDetails.OrderId == id
                         select new OrderDetailListViewModel()
                         {
                             OrderId = order.OrderId,
                             ProductName = product.ProductName,
                             Price = order.Price,
                             ProductPrice = product.ProductPrice,
                             DeliveryDate = order.DeliveryDate,
                             OrderDate = order.OrderDate,
                             Status = order.Flag,
                             ProductId = product.ProductId,
                             Discount = product.ProductDiscount
                         }).ToList();

            return View(model);
        }

    }
}