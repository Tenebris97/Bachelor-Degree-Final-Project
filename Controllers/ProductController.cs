using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinalProject.Data;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace FinalProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IHostingEnvironment _appEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductController(ApplicationDbContext context, IServiceProvider iServiceProvider, IHostingEnvironment appEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _iServiceProvider = iServiceProvider;
            _appEnvironment = appEnvironment;
            _userManager = userManager;
        }

        //نمایش همه
        public async Task<IActionResult> Index(int page = 1)
        {

            var query = (from p in _context.products
                         join c in _context.categories on p.CategoryId equals c.CategoryId

                         select new ProductListViewModel()
                         {
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
            ViewBag.ViewTitle = "تمام محصولات";
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

            var model = new MultiModels();


            model.Product = (from p in _context.products where p.ProductId == id select p).SingleOrDefault();
            var categoryId = model.Product.CategoryId;

            var categoryLap = (from c in _context.categories select c).Where(c => c.Name.Contains("لپ")).SingleOrDefault();
            var categoryCellphone = (from c in _context.categories select c).Where(c => c.Name.Contains("موبایل")).SingleOrDefault();

            if (categoryId == categoryLap.CategoryId)
            {
                model.LaptopDetailViewModel = (from l in _context.laptops
                                               join p in _context.products on l.ProductId equals p.ProductId
                                               where p.ProductId == id
                                               select new LaptopDetailViewModel()
                                               {
                                                   ProductId = p.ProductId,
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
                                                   Wifi = l.Wifi,
                                                   ProductBrand = p.ProductBrand,
                                                   ProductDiscount = p.ProductDiscount,
                                                   ProductImage = p.ProductImage,
                                                   ProductLikeCount = p.ProductLikeCount,
                                                   ProductPrice = p.ProductPrice,
                                                   ProductName = p.ProductName,
                                                   ProductStock = p.ProductStock,
                                                   ProductViews = p.ProductViews,
                                                   ProductDescription = p.ProductDescription,
                                                   ProductCategory = "لپ‌تاپ",
                                                   ProductColor = p.ProductColor,
                                                   ProductWarranty = p.ProductWarranty
                                               }).SingleOrDefault();
            }

            if (categoryId == categoryCellphone.CategoryId)
            {
                model.CellphoneDetailViewModel = (from c in _context.cellphones
                                                  join p in _context.products on c.ProductId equals p.ProductId
                                                  where p.ProductId == id
                                                  select new CellphoneDetailViewModel()
                                                  {
                                                      ProductId = p.ProductId,
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
                                                      ProductBrand = p.ProductBrand,
                                                      ProductDiscount = p.ProductDiscount,
                                                      ProductImage = p.ProductImage,
                                                      ProductLikeCount = p.ProductLikeCount,
                                                      ProductPrice = p.ProductPrice,
                                                      ProductName = p.ProductName,
                                                      ProductStock = p.ProductStock,
                                                      ProductViews = p.ProductViews,
                                                      ProductDescription = p.ProductDescription,
                                                      ProductCategory = "گوشی موبایل",
                                                      ProductColor = p.ProductColor,
                                                      ProductWarranty = p.ProductWarranty
                                                  }).SingleOrDefault();
            }
            //دستورات تعداد بازدید
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                var result = (from b in db.products where b.ProductId == id select b);
                var currentProduct = result.FirstOrDefault();
                if (result.Count() != 0)
                {
                    currentProduct.ProductViews++;
                    //Update Field morede nazar - vaghT faghat bekhaym 1 field o estefade konim az in 2khat estefade mishe
                    db.products.Attach(currentProduct);
                    db.Entry(currentProduct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            ViewBag.imagepath = "/upload/normalimage/";
            return View(model);
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
                    query.Where(m => m.ProductName.Contains(productSearch)).OrderBy(p => p.ProductId), 10, page);
            }
            if (categorySearch != null)
            {
                modelPaging = await PagingList.CreateAsync(
                    query.Where(m => m.ProductName.Contains(categorySearch)).OrderBy(p => p.ProductName), 10, page);
            }

            ViewBag.RootPath = "/upload/normalimage/";
            ViewBag.ViewTitle = "نتایج جستجو";
            return View("Index", modelPaging);
        }

        public IActionResult NotFounds()
        {
            return View("NotFounds");
        }

        [Authorize(Policy = "RequireMemberRole")]
        public async Task<IActionResult> Like(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                //Check book for exist
                var query = db.products.Where(p => p.ProductId == id).SingleOrDefault();
                if (query == null)
                {
                    return Redirect(Request.Headers["Referer"].ToString());
                    //farghi baham nadaran in 2ta
                    //return RedirectToAction("Index");
                }
                //چک کردن اینکه آیا از قبل کوکی ایجاد شده یا نه
                if (Request.Cookies["LKP"] == null)
                {
                    Response.Cookies.Append("LKP", "," + id + ",", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddYears(5)
                    });
                    query.ProductLikeCount++;
                    db.Update(query);
                    await db.SaveChangesAsync();

                    //return RedirectToAction("Index");
                    //return Redirect(Request.Headers["Referer"].ToString());
                    return Json(new { status = "success", message = "نظر شما ثبت شد", result = query.ProductLikeCount });
                }
                else
                {
                    //اگر کوکی از قبل وجود داشت

                    string content = Request.Cookies["LKP"].ToString();

                    //کاربر قبلا امتیاز داده
                    if (content.Contains("," + id + ","))
                        return Redirect(Request.Headers["Referer"].ToString());
                    //اگر قبلا رای نداده
                    else
                    {
                        content += "," + id + ",";
                        Response.Cookies.Append("LKP", content, new CookieOptions()
                        {
                            Expires = DateTime.Now.AddYears(5)
                        }
                        );
                        query.ProductLikeCount++;
                        db.Update(query);
                        await db.SaveChangesAsync();

                        //return Redirect(Request.Headers["Referer"].ToString());
                        return Json(new { status = "success", message = "نظر شما ثبت شد", result = query.ProductLikeCount });
                    }
                }
            }
        }

        [Authorize(Policy = "RequireMemberRole")]
        public async Task<IActionResult> Dislike(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                //Check if book exists
                var query = db.products.Where(p => p.ProductId == id).SingleOrDefault();
                if (query == null)
                {
                    return Redirect(Request.Headers["Referer"].ToString());
                    //farghi baham nadaran in 2ta
                    //return RedirectToAction("Index");
                }
                //چک کردن اینکه آیا از قبل کوکی ایجاد شده یا نه
                if (Request.Cookies["DKP"] == null)
                {
                    Response.Cookies.Append("DKP", "," + id + ",", new CookieOptions()
                    {
                        Expires = DateTime.Now.AddYears(5)
                    });
                    query.ProductLikeCount--;
                    db.Update(query);
                    await db.SaveChangesAsync();

                    //return RedirectToAction("Index");
                    //return Redirect(Request.Headers["Referer"].ToString());
                    return Json(new { status = "success", message = "نظر شما ثبت شد", result = query.ProductLikeCount });
                }
                else
                {
                    //اگر کوکی از قبل وجود داشت

                    string content = Request.Cookies["DKP"].ToString();

                    //کاربر قبلا امتیاز داده
                    if (content.Contains("," + id + ","))
                        return Redirect(Request.Headers["Referer"].ToString());
                    //اگر قبلا رای نداده
                    else
                    {
                        content += "," + id + ",";
                        Response.Cookies.Append("DKP", content, new CookieOptions()
                        {
                            Expires = DateTime.Now.AddYears(5)
                        }
                        );
                        query.ProductLikeCount--;
                        db.Update(query);
                        await db.SaveChangesAsync();

                        //return Redirect(Request.Headers["Referer"].ToString());
                        return Json(new
                        {
                            status = "success",
                            message = "نظر شما ثبت شد",
                            result = query.ProductLikeCount
                        });
                    }
                }
            }
        }

        [Authorize(Policy = "RequireMemberRole")]
        public IActionResult AddToBasket(int id)
        {
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                //کنترل اینکه آیا آی دی ارسال شده وجود دارد یا خیر در دیتابیس
                var query = db.products.Where(b => b.ProductId == id).SingleOrDefault();//LINQ to SQL
                if (query == null)
                {
                    return Json(new
                    {
                        status = "fail",
                        message = "این محصول یافت نشد"
                    });
                }

                //اگر موجود نبود
                if (query.ProductStock == 0)
                    return Json(new
                    {
                        status = "success",
                        message = "کالای مورد نظر موجود نیست"
                    });

                //اگر کالا موجود بود
                else
                {
                    //agar cookie mojud nabud
                    if (Request.Cookies["ATB"] == null)
                    {
                        Response.Cookies.Append("ATB", "," + id + ",", new CookieOptions()
                        { Expires = DateTime.Now.AddMinutes(30) });
                        return Json(new
                        {
                            status = "success",
                            message = "کالای مورد نظر به سبد اضافه شد",
                            basketCount = 1
                        });
                    }
                    else
                    {
                        string cookie = Request.Cookies["ATB"].ToString();
                        //اگر کتاب قبلا به لیست درخواستی اضافه نشده باشد
                        if (cookie.Contains("," + id + ","))
                        {
                            return Json(new
                            {
                                status = "success",
                                message = "این کالا قبلا به سبد خرید شما اضافه شده است"
                            });
                        }
                        else
                        {
                            cookie += "," + id + ",";
                            Response.Cookies.Append("ATB", cookie, new CookieOptions()
                            { Expires = DateTime.Now.AddMinutes(30) });

                            string[] requestedBookCount = cookie.Split(',');
                            requestedBookCount = requestedBookCount.Where(r => r != "").ToArray();

                            return Json(new
                            {
                                status = "success",
                                message = "کالای مورد نظر به سبد اضافه شد",
                                basketCount = requestedBookCount.Count()
                            });
                        }
                    }
                }
            }
        }

        [Authorize(Policy = "RequireMemberRole")]
        public async Task<IActionResult> Cart()
        {
            var model = new MultiModels();

            //Query String
            //model.UserList = (from u in _userManager.Users orderby u.Id descending select u).Take(10).ToList();
            //model.LastNews = (from n in _context.news orderby n.NewsId descending select n).Take(5).ToList();

            var userId = _userManager.GetUserId(User);

            model.Address = _context.Address.Where(a => a.UserId == userId).SingleOrDefault();
            model.CurrentUser = _context.Users.Where(a => a.Id == userId).SingleOrDefault();

            if (Request.Cookies["ATB"] != null)
            {
                string cookieContent = Request.Cookies["ATB"].ToString();
                string[] requestedBook = cookieContent.Split(',');
                requestedBook = requestedBook.Where(r => r != null).ToArray();
                model.SearchedProduct = (from b in _context.products
                                         where requestedBook.Contains(b.ProductId.ToString())
                                         select new Product
                                         {
                                             ProductId = b.ProductId,
                                             ProductName = b.ProductName,
                                             ProductPrice = b.ProductPrice,
                                             ProductDiscount = b.ProductDiscount
                                         }).ToList();
            }

            ViewBag.imagepath = "/upload/normalimage/";
            var queryFullname = (from u in _context.Users where u.Id == _userManager.GetUserId(HttpContext.User) select u).SingleOrDefault();
            return View(model);
        }

        public IActionResult DeleteRequestedProduct(int id)
        {
            string cookieContent = Request.Cookies["ATB"].ToString();
            string[] bookIdRequested = cookieContent.Split(',');
            bookIdRequested = bookIdRequested.Where(b => b != "").ToArray();
            //ezafe kardan yek araye be list
            List<string> idList = new List<string>(bookIdRequested);
            idList.Remove(id.ToString());

            cookieContent = "";
            for (int i = 0; i < idList.Count(); i++)
                cookieContent += "," + idList[i] + ",";

            Response.Cookies.Append("ATB", cookieContent, new CookieOptions()
            { Expires = DateTime.Now.AddMinutes(30) });

            var model = new MultiModels();
            //Query String
            //model.UserList = (from u in _userManager.Users orderby u.Id descending select u).Take(10).ToList();
            //model.LastNews = (from n in _context.news orderby n.NewsId descending select n).Take(5).ToList();

            if (Request.Cookies["ATB"] != null)
            {
                string[] requestedBook = cookieContent.Split(',');
                requestedBook = requestedBook.Where(r => r != null).ToArray();
                model.SearchedProduct = (from b in _context.products
                                         where requestedBook.Contains(b.ProductId.ToString())
                                         select new Product
                                         {
                                             ProductId = b.ProductId,
                                             ProductName = b.ProductName,
                                             ProductPrice = b.ProductPrice,
                                             ProductDiscount = b.ProductDiscount
                                         }).ToList();
            }

            ViewBag.imagepath = "/upload/normalimage/";
            return View("Cart", model);
        }

        [Authorize]
        [Authorize(Policy = "RequireMemberRole")]
        public IActionResult Order(string userId, string tp)
        {
            string cookieContent = Request.Cookies["ATB"].ToString();
            string[] productId = cookieContent.Split(',');
            productId = productId.Where(b => b != "").ToArray();

            //get AddressId
            Address address = new Address();
            address = _context.Address.Where(a => a.UserId == _userManager.GetUserId(User)).SingleOrDefault();

            //Sabte etelaat dar DB
            using (var db = _iServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                using (var transaction = db.Database.BeginTransaction())//Ya hame in amaliat ha anjam beshan ya hichkodum
                {
                    try
                    {
                        //Tarikhe Shamsi
                        var currentDate = DateTime.Now;
                        PersianCalendar persianCalendar = new PersianCalendar();
                        int year = persianCalendar.GetYear(currentDate);
                        int month = persianCalendar.GetMonth(currentDate);
                        int day = persianCalendar.GetDayOfMonth(currentDate);
                        string persianDate = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));

                        Order order = new Order();
                        order.UserId = userId;
                        order.OrderDate = persianDate;
                        order.Flag = 2;
                        order.Price = Convert.ToInt32(tp);
                        order.AddressId = address.AddressId;

                        db.Order.Add(order);
                        db.SaveChanges();

                        int orderId = order.OrderId;

                        for (int i = 0; i < productId.Count(); i++)
                        {
                            var product = (from b in db.products where b.ProductId == Convert.ToInt32(productId[i]) select b).SingleOrDefault();
                            OrderDetails orderDetails = new OrderDetails();
                            orderDetails.OrderId = orderId;
                            orderDetails.ProductId = product.ProductId;
                            orderDetails.UserId = _userManager.GetUserId(User);

                            db.OrderDetails.Add(orderDetails);
                            db.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch { }
                }
            }
            Response.Cookies.Delete("ATB");
            return Json(new { status = "success", message = "سفارش شما با موفقیت ثبت شد. با تشکر از خرید شما" });
        }

        [HttpPost]
        [Authorize(Policy = "RequireMemberRole")]
        public async Task<IActionResult> Payment(MultiModels t)
        {

            //Order(userId, tp);

            if (t.Transaction.Amount == 0)
            {
                ModelState.AddModelError("AmountError", "مبلغ خالی است");
            }

            //From Zarinpal
            var payment = await new ZarinpalSandbox.Payment(t.Transaction.Amount).PaymentRequest("توضیحات",
                Url.Action(nameof(PaymentVerify), "Product", new
                {
                    amount = t.Transaction.Amount,
                    Email = t.Transaction.Email,
                    Desc = t.Transaction.Description
                }
                , Request.Scheme),
               t.Transaction.Email, t.CurrentUser.PhoneNumber);
            //درصورت موفقیت آمیز بودن درخواست کاربر را به صفحه درخواست هدایت کن
            //در غیراینصورت باید خطا نمایش داده شود
            return payment.Status == 100 ? (IActionResult)Redirect(payment.Link) :
                BadRequest($"خطا در پرداخت، کد خطا :  { payment.Status}");

        }

        [Authorize(Policy = "RequireMemberRole")]
        public async Task<IActionResult> PaymentVerify(int amount, string Email, string Desc, string Authority, string Status)
        {
            if (Status == "NOK") return View("FailedPayment");

            //گرفتن تاییدیه پرداخت
            var verification = await new ZarinpalSandbox.Payment(amount).Verification(Authority);

            //ارسال به صفحه خطا
            if (verification.Status != 100) return View("FailedPayment");

            //ارسال کد تراکنش جهت نمایش به کاربر
            var RefId = verification.RefId;

            //get AddressId
            Address address = new Address();
            address = _context.Address.Where(a => a.UserId == _userManager.GetUserId(User)).SingleOrDefault();

            //get date
            var currentDate = DateTime.Now;
            PersianCalendar pc = new PersianCalendar();
            int year = pc.GetYear(currentDate);
            int month = pc.GetMonth(currentDate);
            int day = pc.GetDayOfMonth(currentDate);
            string date = string.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(year + "/" + month + "/" + day));

            //get time
            string time = string.Format("{0:HH:mm:ss}", Convert.ToDateTime(currentDate.TimeOfDay.Hours + ":"
                + currentDate.TimeOfDay.Minutes + ":" + currentDate.TimeOfDay.Seconds));

            string cookieContent = Request.Cookies["ATB"].ToString();
            string[] productId = cookieContent.Split(',');
            productId = productId.Where(b => b != "").ToArray();
            //Insert Payment info
            using (var database = _context)//yani az in be bad az moteghayere database bejaye contex estefade mikonim
            {
                using (var transaction = database.Database.BeginTransaction())//Ya hame in amaliat ha anjam beshan ya hichkodum
                {
                    try
                    {
                        Order order = new Order();
                        order.UserId = _userManager.GetUserId(User);
                        order.OrderDate = date;
                        order.Flag = 1;
                        order.Price = amount;
                        order.AddressId = address.AddressId;

                        database.Order.Add(order);
                        database.SaveChanges();

                        int orderId = order.OrderId;

                        for (int i = 0; i < productId.Count(); i++)
                        {
                            var product = (from b in database.products where b.ProductId == Convert.ToInt32(productId[i]) select b).SingleOrDefault();

                            OrderDetails orderDetails = new OrderDetails();
                            orderDetails.ProductId = product.ProductId;
                            orderDetails.OrderId = orderId;
                            orderDetails.UserId = _userManager.GetUserId(User);

                            //Update product stock
                            product.ProductStock--;
                            database.products.Attach(product);
                            database.Entry(product).State = EntityState.Modified;

                            database.OrderDetails.Add(orderDetails);
                            database.SaveChanges();
                        }

                        //ثبت اطلاعات تراکنش
                        Transaction p = new Transaction();
                        p.TransactionDate = date;
                        p.TransactionTime = time;
                        p.Amount = amount;
                        p.Description = Desc;
                        p.Email = Email;
                        p.TransactionNo = verification.RefId.ToString();
                        p.UserId = _userManager.GetUserId(User);
                        p.OrderId = orderId;
                        database.Transaction.Add(p);
                        database.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {

                    }
                }
            }
            ViewBag.TransactionNo = verification.RefId.ToString();
            ViewBag.Amount = amount;
            ViewBag.TransactionDate = date;
            ViewBag.TransactionTime = time;
            Response.Cookies.Delete("ATB");
            return View("SuccessfullPayment");
        }

        [Authorize]
        [Authorize(Policy = "RequireMemberRole")]
        public IActionResult Wishlist(string id)
        {
            int pId = Convert.ToInt32(id);
            var query = (from w in _context.Wishlist where w.ProductId == pId && w.UserId == _userManager.GetUserId(User) select w).SingleOrDefault();
            
            if(query == null)
            {
                using (var db = _context)
                {
                    Wishlist wishlist = new Wishlist();
                    wishlist.ProductId = pId;
                    wishlist.UserId = _userManager.GetUserId(User);
                    db.Wishlist.Add(wishlist);
                    db.SaveChanges();
                }
                return Json(new { status = "success", message = "کالای مورد نظر به لیست علاقه‌مندی‌ها اضافه شد" });
            }
            else
                return Json(new { status = "warning", message = "این کالا قبلا در لیست علاقه‌مندی‌های شما اضافه شده است" });
        }
    }
}