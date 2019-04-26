using System;
using System.Collections.Generic;
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

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class CellphoneController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;

        public CellphoneController(ApplicationDbContext context, IServiceProvider iServiceProvider)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            Cellphone model = new Cellphone();

            //LINQ to Query string
            //Join on 2 tables
            var query = (from c in _context.cellphones
                         join p in _context.products on c.ProductId equals p.ProductId

                         select new CellphoneListViewModel()
                         {
                             ProductId = p.ProductId,
                             Name = p.ProductName,
                             CellphoneId = c.CellphoneId,
                             CameraHas = c.CameraHas,
                             CameraRecording = c.CameraRecording,
                             CameraResolution = c.CameraResolution,
                             ConnectionNetworks = c.ConnectionNetworks,
                             ConnectionTechnologies = c.ConnectionTechnologies,
                             CpuChipset = c.CpuChipset,
                             CpuCore = c.CpuCore,
                             CpuFrequency = c.CpuFrequency,
                             GPU = c.GPU,
                             SimcardCount = c.SimcardCount,
                             CpuType = c.CpuType,
                             ScreenProtector = c.ScreenProtector,
                             SimcardDesc = c.SimcardDesc,
                             StorageSupport = c.StorageSupport,
                             Description = c.Description,
                             OS = c.OS,
                             OSVersion = c.OSVersion,
                             RAM = c.RAM,
                             ScreenSize = c.ScreenSize,
                             ScreenTechnology = c.ScreenTechnology,
                             ScreenType = c.ScreenType,
                             Size = c.Size,
                             Storage = c.Storage,
                             StorageType = c.StorageType,
                             Weight = c.Weight,
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            var modelPaging = await PagingList.CreateAsync(query, 10, page);
            return View(modelPaging);
        }

        public async Task<IActionResult> Search(string productSearch, int page = 1)
        {
            var query = (from c in _context.cellphones
                         join p in _context.products on c.ProductId equals p.ProductId

                         select new CellphoneListViewModel()
                         {
                             ProductId = p.ProductId,
                             Name = p.ProductName,
                             CellphoneId = c.CellphoneId,
                             CameraHas = c.CameraHas,
                             CameraRecording = c.CameraRecording,
                             CameraResolution = c.CameraResolution,
                             ConnectionNetworks = c.ConnectionNetworks,
                             ConnectionTechnologies = c.ConnectionTechnologies,
                             CpuChipset = c.CpuChipset,
                             CpuCore = c.CpuCore,
                             CpuFrequency = c.CpuFrequency,
                             GPU = c.GPU,
                             SimcardCount = c.SimcardCount,
                             CpuType = c.CpuType,
                             ScreenProtector = c.ScreenProtector,
                             SimcardDesc = c.SimcardDesc,
                             StorageSupport = c.StorageSupport,
                             Description = c.Description,
                             OS = c.OS,
                             OSVersion = c.OSVersion,
                             RAM = c.RAM,
                             ScreenSize = c.ScreenSize,
                             ScreenTechnology = c.ScreenTechnology,
                             ScreenType = c.ScreenType,
                             Size = c.Size,
                             Storage = c.Storage,
                             StorageType = c.StorageType,
                             Weight = c.Weight,
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            var modelPaging = await PagingList.CreateAsync(query, 10, page);

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
            CellphoneAddEdit model = new CellphoneAddEdit();
            var category = (from c in _context.categories select c).Where(c => c.Name.Contains("موبایل")).SingleOrDefault();
            int categoryId = category.CategoryId;

            //Dropdown List - Products
            model.Products = _context.products.Where(p => p.CategoryId == categoryId).Select(c => new SelectListItem
            {
                Text = c.ProductName,
                Value = c.ProductId.ToString()
            }).ToList();

            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)//Id Az asp-route-id miad inja
        {
            CellphoneAddEdit model = new CellphoneAddEdit();

            var category = (from c in _context.categories select c).Where(c => c.Name.Contains("موبایل")).SingleOrDefault();
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
                    Cellphone cellphone = _context.cellphones.Where(p => p.ProductId == id).SingleOrDefault();
                    if (cellphone != null)
                    {
                        model.ProductId = cellphone.ProductId;
                        model.CellphoneId = cellphone.CellphoneId;
                        model.CameraHas = cellphone.CameraHas;
                        model.CameraRecording = cellphone.CameraRecording;
                        model.CameraResolution = cellphone.CameraResolution;
                        model.ConnectionNetworks = cellphone.ConnectionNetworks;
                        model.ConnectionTechnologies = cellphone.ConnectionTechnologies;
                        model.CpuChipset = cellphone.CpuChipset;
                        model.CpuCore = cellphone.CpuCore;
                        model.CpuFrequency = cellphone.CpuFrequency;
                        model.GPU = cellphone.GPU;
                        model.SimcardCount = cellphone.SimcardCount;
                        model.CpuType = cellphone.CpuType;
                        model.ScreenProtector = cellphone.ScreenProtector;
                        model.SimcardDesc = cellphone.SimcardDesc;
                        model.StorageSupport = cellphone.StorageSupport;
                        model.Description = cellphone.Description;
                        model.OS = cellphone.OS;
                        model.OSVersion = cellphone.OSVersion;
                        model.RAM = cellphone.RAM;
                        model.ScreenSize = cellphone.ScreenSize;
                        model.ScreenTechnology = cellphone.ScreenTechnology;
                        model.ScreenType = cellphone.ScreenType;
                        model.Size = cellphone.Size;
                        model.Storage = cellphone.Storage;
                        model.StorageType = cellphone.StorageType;
                        model.Weight = cellphone.Weight;
                    }
                }
            }
            return View("Add", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int CellphoneId, CellphoneAddEdit model, IEnumerable<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                /***********************/
                if (CellphoneId == 0)
                {
                    //Insert
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Cellphone cellphoneModel = AutoMapper.Mapper.Map<CellphoneAddEdit, Cellphone>(model);
                        db.cellphones.Add(cellphoneModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "جزییات لپ‌تاپ با موفقیت ثبت شد" });
                }
                else
                {
                    //Update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Cellphone cellphoneModel = AutoMapper.Mapper.Map<CellphoneAddEdit, Cellphone>(model);
                        db.cellphones.Update(cellphoneModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "جزییات لپ‌تاپ  با موفقیت ویراش شد" });
                }
            }

            var category = (from c in _context.categories select c).Where(c => c.Name.Contains("موبایل")).SingleOrDefault();
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
                var model = db.cellphones.Where(c => c.CellphoneId == id).SingleOrDefault();
                db.cellphones.Remove(model);
                db.SaveChanges();
                return Ok();
            }
        }
    }
}