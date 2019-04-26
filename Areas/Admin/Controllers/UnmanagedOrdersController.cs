using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class UnmanagedOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnmanagedOrdersController(ApplicationDbContext context, IServiceProvider iServiceProvider, 
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
                         where p.Flag != 4 && p.Flag != 3
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

        public IActionResult Delivered(string orderId)
        {
            int id = Convert.ToInt32(orderId);
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var query = (from o in db.Order where o.OrderId == id select o);
                var result = query.SingleOrDefault();

                //Tarikhe Shamsi
                var currentDate = DateTime.Now;
                PersianCalendar persianCalendar = new PersianCalendar();
                int year = persianCalendar.GetYear(currentDate);
                int month = persianCalendar.GetMonth(currentDate);
                int day = persianCalendar.GetDayOfMonth(currentDate);
                string persianDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));

                //پذیرفتن سفارش
                if (query.Count() != 0)
                {
                    result.Flag = 4;
                    result.DeliveryDate = persianDate;
                    db.Order.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(new { status = "success", message = "سفارش به مشتری تحویل داده شد" });
        }

        public IActionResult Canceled(string orderId)
        {
            int id = Convert.ToInt32(orderId);
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var query = (from o in db.Order where o.OrderId == id select o);
                var result = query.SingleOrDefault();

                //لغو سفارش
                if (query.Count() != 0)
                {
                    result.Flag = 3;
                    db.Order.Attach(result);
                    db.Entry(result).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return Json(new { status = "success", message = "سفارش با موفقیت لغو شد" });
        }
    }
}