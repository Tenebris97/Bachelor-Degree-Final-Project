using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class LaptopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IHostingEnvironment _appEnvironment;

        public LaptopController(ApplicationDbContext context,
            IServiceProvider iServiceProvider, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            LaptopAddEdit model = new LaptopAddEdit();

            //LINQ to Query string
            //Join on 2 tables
            var query = (from l in _context.laptops
                         join p in _context.products on l.ProductId equals p.ProductId

                         select new LaptopListViewModel()
                         {
                             ProductId = p.ProductId,
                             Name = p.ProductName,
                             LaptopId = l.LaptopId,
                             BatteryType = l.BatteryType,
                             Bluetooth = l.Bluetooth,
                             CpuCache = l.CpuCache,
                             CpuFrequency = l.CpuFrequency,
                             CpuManufactor = l.CpuManufactor,
                             CpuSeries = l.CpuSeries,
                             CpuType = l.CpuType,
                             Description = l.Description,
                             GPUManufactor = l.GPUManufactor,
                             GPUModel = l.GPUModel,
                             GPUSize = l.GPUSize,
                             HDMI = l.HDMI,
                             Modem = l.Modem,
                             ODD = l.ODD,
                             OS = l.OS,
                             OSVersion = l.OSVersion,
                             RAM = l.RAM,
                             RAMType = l.RAMType,
                             ScreenSize = l.ScreenSize,
                             ScreenTechnology = l.ScreenTechnology,
                             ScreenType = l.ScreenType,
                             Size = l.Size,
                             Speaker = l.Speaker,
                             Storage = l.Storage,
                             StorageType = l.StorageType,
                             USB2 = l.USB2,
                             USB3 = l.USB3,
                             VGA = l.VGA,
                             Webcam = l.Webcam,
                             Weight = l.Weight,
                             Wifi = l.Wifi
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            var modelPaging = await PagingList.CreateAsync(query, 10, page);
            return View(modelPaging);
        }

        public async Task<IActionResult> Search(string productSearch, int page = 1)
        {
            var query = (from l in _context.laptops
                         join p in _context.products on l.ProductId equals p.ProductId

                         select new LaptopListViewModel()
                         {
                             ProductId = p.ProductId,
                             Name = p.ProductName,
                             LaptopId = l.LaptopId,
                             BatteryType = l.BatteryType,
                             Bluetooth = l.Bluetooth,
                             CpuCache = l.CpuCache,
                             CpuFrequency = l.CpuFrequency,
                             CpuManufactor = l.CpuManufactor,
                             CpuSeries = l.CpuSeries,
                             CpuType = l.CpuType,
                             Description = l.Description,
                             GPUManufactor = l.GPUManufactor,
                             GPUModel = l.GPUModel,
                             GPUSize = l.GPUSize,
                             HDMI = l.HDMI,
                             Modem = l.Modem,
                             ODD = l.ODD,
                             OS = l.OS,
                             OSVersion = l.OSVersion,
                             RAM = l.RAM,
                             RAMType = l.RAMType,
                             ScreenSize = l.ScreenSize,
                             ScreenTechnology = l.ScreenTechnology,
                             ScreenType = l.ScreenType,
                             Size = l.Size,
                             Speaker = l.Speaker,
                             Storage = l.Storage,
                             StorageType = l.StorageType,
                             USB2 = l.USB2,
                             USB3 = l.USB3,
                             VGA = l.VGA,
                             Webcam = l.Webcam,
                             Weight = l.Weight,
                             Wifi = l.Wifi
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            var modelPaging = await PagingList.CreateAsync(query, 2, page);

            if (productSearch != null)
            {
                modelPaging = await PagingList.CreateAsync(
                    query.Where(m => m.Name.Contains(productSearch)).OrderBy(p => p.ProductId), 10, page);
            }

            return View("Index", modelPaging);
        }

        [HttpGet]
        public IActionResult Add()
        {
            LaptopAddEdit model = new LaptopAddEdit();
            var category = (from c in _context.categories select c).Where(c => c.Name.Contains("لپ")).SingleOrDefault();
            int categoryId = category.CategoryId;

            //Dropdown List - Products
            model.Products = _context.products.Where(p=>p.CategoryId == categoryId).Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();

            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)//Id Az asp-route-id miad inja
        {
            LaptopAddEdit model = new LaptopAddEdit();

            var category = (from c in _context.categories select c).Where(c => c.Name.Contains("لپ")).SingleOrDefault();
            int categoryId = category.CategoryId;

            //Dropdown List - Products
            model.Products = _context.products.Where(p => p.CategoryId == categoryId).Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();

            if (id != 0)
            {
                //Dependency Injection
                using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    Laptop laptop = _context.laptops.Where(p => p.ProductId == id).SingleOrDefault();
                    if (laptop != null)
                    {
                        model.ProductId = laptop.ProductId;
                        model.LaptopId = laptop.LaptopId;
                        model.BatteryType = laptop.BatteryType;
                        model.Bluetooth = laptop.Bluetooth;
                        model.CpuCache = laptop.CpuCache;
                        model.CpuFrequency = laptop.CpuFrequency;
                        model.CpuManufactor = laptop.CpuManufactor;
                        model.CpuSeries = laptop.CpuSeries;
                        model.CpuType = laptop.CpuType;
                        model.Description = laptop.Description;
                        model.GPUManufactor = laptop.GPUManufactor;
                        model.GPUModel = laptop.GPUModel;
                        model.GPUSize = laptop.GPUSize;
                        model.HDMI = laptop.HDMI;
                        model.Modem = laptop.Modem;
                        model.ODD = laptop.ODD;
                        model.OS = laptop.OS;
                        model.OSVersion = laptop.OSVersion;
                        model.RAM = laptop.RAM;
                        model.RAMType = laptop.RAMType;
                        model.ScreenSize = laptop.ScreenSize;
                        model.ScreenTechnology = laptop.ScreenTechnology;
                        model.ScreenType = laptop.ScreenType;
                        model.Size = laptop.Size;
                        model.Speaker = laptop.Speaker;
                        model.Storage = laptop.Storage;
                        model.StorageType = laptop.StorageType;
                        model.USB2 = laptop.USB2;
                        model.USB3 = laptop.USB3;
                        model.VGA = laptop.VGA;
                        model.Webcam = laptop.Webcam;
                        model.Weight = laptop.Weight;
                        model.Wifi = laptop.Wifi;
                    }
                }
            }
            return View("Add", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int LaptopId, LaptopAddEdit model, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                /***********************/
                if (LaptopId == 0)
                {
                    //Insert
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Laptop laptopModel = AutoMapper.Mapper.Map<LaptopAddEdit, Laptop>(model);
                        db.laptops.Add(laptopModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "جزییات لپ‌تاپ با موفقیت ثبت شد" });
                }
                else
                {
                    //Update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Laptop laptopModel = AutoMapper.Mapper.Map<LaptopAddEdit, Laptop>(model);
                        db.laptops.Update(laptopModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "جزییات لپ‌تاپ  با موفقیت ویراش شد" });
                }
            }

            var category = (from c in _context.categories select c).Where(c => c.Name.Contains("لپ")).SingleOrDefault();
            int categoryId = category.CategoryId;

            //Dropdown List - Products
            model.Products = _context.products.Where(p => p.CategoryId == categoryId).Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();

            //Display Validation with Jquery Ajax
            var list = new List<string>();
            foreach (var validation in ViewData.ModelState.Values)
                list.AddRange(validation.Errors.Select(error => error.ErrorMessage));

            return Json(new { status = "error", error = list });
        }

        public IActionResult Delete(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var model = db.laptops.Where(c => c.LaptopId == id).SingleOrDefault();
                db.laptops.Remove(model);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}