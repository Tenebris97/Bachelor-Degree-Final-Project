using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, 
            IServiceProvider iServiceProvider, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            //LINQ to Query string
            //Join on 3 tables
            var query = (from p in _context.products
                         join c in _context.categories on p.CategoryId equals c.CategoryId

                         select new ProductListViewModel()
                         {
                             CategoryId = c.CategoryId,
                             ProductCategory = c.Name,
                             ProductName = p.ProductName,
                             ProductBrand = p.ProductBrand,
                             ProductDescription = p.ProductDescription,
                             ProductDiscount = p.ProductDiscount,
                             ProductId = p.ProductId,
                             ProductImage = p.ProductImage,
                             ProductLikeCount = p.ProductLikeCount,
                             ProductPrice = p.ProductPrice,
                             ProductStock = p.ProductStock,
                             ProductViews = p.ProductViews                            
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            ViewBag.RootPath = "/upload/normalimage/";

            var modelPaging = await PagingList.CreateAsync(query, 2, page);
            return View(modelPaging);
        }

        public async Task<IActionResult> Search(string productSearch, string categorySearch, int page = 1)
        {
            var query = (from p in _context.products
                         join c in _context.categories on p.CategoryId equals c.CategoryId

                         select new ProductListViewModel()
                         {
                             CategoryId = c.CategoryId,
                             ProductCategory = c.Name,
                             ProductName = p.ProductName,
                             ProductBrand = p.ProductBrand,
                             ProductDescription = p.ProductDescription,
                             ProductDiscount = p.ProductDiscount,
                             ProductId = p.ProductId,
                             ProductImage = p.ProductImage,
                             ProductLikeCount = p.ProductLikeCount,
                             ProductPrice = p.ProductPrice,
                             ProductStock = p.ProductStock,
                             ProductViews = p.ProductViews
                         }).AsNoTracking().OrderBy(p => p.ProductId);

            var modelPaging = await PagingList.CreateAsync(query, 2, page);

            if (productSearch != null)
            {
                modelPaging = await PagingList.CreateAsync(
                    query.Where(m => m.ProductName.Contains(productSearch)).OrderBy(p => p.ProductId), 2, page);
            }
            if (categorySearch != null)
            {
                modelPaging = await PagingList.CreateAsync(
                    query.Where(m => m.ProductName.Contains(categorySearch)).OrderBy(p => p.ProductName), 2, page);
            }

            ViewBag.RootPath = "/upload/normalimage/";
            return View("Index", modelPaging);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ProductAddEditModel model = new ProductAddEditModel();

            //Dropdown List - Categories
            model.Categories = _context.categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToList();

            return View("Add", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)//Id Az asp-route-id miad inja
        {
            ProductAddEditModel model = new ProductAddEditModel();

            //Dropdown List - Categories
            model.Categories = _context.categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToList();

            if (id != 0)
            {
                //Dependency Injection
                using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    Product product = _context.products.Where(p => p.ProductId == id).SingleOrDefault();
                    if (product != null)
                    {
                        model.ProductId = product.ProductId;
                        model.ProductBrand = product.ProductBrand;
                        model.ProductDescription = product.ProductDescription;
                        model.ProductDiscount = product.ProductDiscount;
                        model.ProductImage = product.ProductImage;
                        model.CategoryId = product.CategoryId;
                        model.ProductName = product.ProductName;
                        model.ProductPrice = product.ProductPrice;
                        model.ProductStock = product.ProductStock;
                        model.ProductColor = product.ProductColor;
                        model.ProductWarranty = product.ProductWarranty;
                        model.ProductViews = product.ProductViews;
                    }
                }
            }
            return View("Add", model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int ProductId, ProductAddEditModel model, IEnumerable<IFormFile> files, string ImgName)
        {
            if (ModelState.IsValid)
            {
                //Upload Image
                var uploads = Path.Combine(_appEnvironment.WebRootPath, "upload/normalimage/");
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        var filename = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                        using (var fs = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                        {
                            await file.CopyToAsync(fs);
                            model.ProductImage = filename;
                        }
                        //Resize Images
                        //InsertShowImage.ImageResizer img = new InsertShowImage.ImageResizer();
                        //img.Resize(uploads + filename, _appEnvironment.WebRootPath + "upload/thumbnailimage/" + filename);
                    }
                }
                /***********************/
                if (ProductId == 0)
                {
                    if (model.ProductImage == null)
                    {
                        model.ProductImage = "defaultpic1.png";
                    }
                    //Insert
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Product productModel = AutoMapper.Mapper.Map<ProductAddEditModel, Product>(model);
                        db.products.Add(productModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "کالا با موفقیت ثبت شد" });
                }
                else
                {
                    if (model.ProductImage == null)
                    {
                        model.ProductImage = ImgName;
                    }
                    //Update
                    using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        Product productModel = AutoMapper.Mapper.Map<ProductAddEditModel, Product>(model);
                        db.products.Update(productModel);
                        db.SaveChanges();
                    }
                    return Json(new { status = "success", message = "کالا با موفقیت ویراش شد" });
                }
            }

            //Refreshing Category Dropdown
            model.Categories = _context.categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
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
                var model = _context.products.Where(p => p.ProductId == id).SingleOrDefault();

                //dastoorate hazf tasvir az server
                if (model.ProductImage != "defaultpic.png")
                {

                    var uploadsNormal = Path.Combine(_appEnvironment.WebRootPath, "upload//normalimage//") + model.ProductImage;
                    if (System.IO.File.Exists(uploadsNormal))
                        System.IO.File.Delete(uploadsNormal);

                    var uploadsThumb = Path.Combine(_appEnvironment.WebRootPath, "upload//thumbnailimage//") + model.ProductImage;
                    if (System.IO.File.Exists(uploadsThumb))
                        System.IO.File.Delete(uploadsThumb);
                }
                db.products.Remove(model);
                db.SaveChanges();
            }
            return Ok();
        }
    }
}