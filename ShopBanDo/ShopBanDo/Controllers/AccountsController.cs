using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShopBanDo.Extension;
using ShopBanDo.Helpper;
using ShopBanDo.Models;
using ShopBanDo.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopBanDo.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly dbshopContext _context;
        public INotyfService _notyfService { get; }
        public AccountsController (dbshopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        //kiem tra phone co bi chung tra ve json == true thi cho phep nhap == data thi khong cho phep
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
                if (khachhang != null)
                    return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");

                return Json(data: true);

            }
            catch
            {
                return Json(data: true);
            }
        }
        //kiem tra email co bi chung
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                    return Json(data: "Email : " + Email + " đã được sử dụng");
                return Json(data: true);
            }
            catch
            {
                return Json(data: true);
            }
        }
        //trang chu khach hang
        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            //lay ra session sau khi dang nhap CustomerID
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                //Tra ve view Dashbroad 
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var lsDonHang = _context.Orders
                        .Include(x => x.TransactStatus)
                        .AsNoTracking()
                        .Where(x => x.CustomerId == khachhang.CustomerId)
                        .OrderByDescending(x => x.OrderDate)
                        .ToList();
                    ViewBag.DonHang = lsDonHang;
                    return View(khachhang);
                }
            }
            //else tra lai ve trang login
            return RedirectToAction("Login");
        }
        // cancel order
        public IActionResult UpdateAddress(string newAddress)
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                khachhang.Address = newAddress;
                _context.Customers.Update(khachhang);
                _context.SaveChanges();
                _notyfService.Success("Change Address successful");
            }
            return RedirectToAction("Dashboard");
        }
        public IActionResult DeleteOrder(int id)
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var donhang = _context.Orders.AsNoTracking().SingleOrDefault(x => x.OrderId == id);
                    if (donhang != null)
                    {
                        // update delete to true
                        donhang.Deleted = true;
                        donhang.TransactStatusId = 5; // status cancel
                        _context.Orders.Update(donhang);
                        _context.SaveChanges();
                        _notyfService.Success("Cancel order successful");
                        return RedirectToAction("Dashboard");
                    }
                }
            }
            return RedirectToAction("Login");
        }


        //dang ki
        [HttpGet]
        [AllowAnonymous]
        [Route("dangki/taikhoan.html", Name = "DangKy")]
        public IActionResult DangkyTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dangki/taikhoan.html", Name = "DangKy")]
        public async Task<IActionResult> DangkyTaiKhoan(RegisterViewModel taikhoan)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //create salt + md5 for password
                    string salt = Utilities.GetRandomKey();
                    Customer khachhang = new Customer
                    {
                        //MD 5 PASS o trong extension
                        FullName = taikhoan.FullName,
                        Phone = taikhoan.Phone.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt,
                        CreateDate = DateTime.Now
                    };
                    try
                    {
                        
                        if(_context.Customers.AsNoTracking().SingleOrDefault(x=>x.Email.ToLower() == khachhang.Email.ToLower()) != null)
                        {
                            _notyfService.Success("Tài khoản đã tồn tại");
                            return RedirectToAction("DangkyTaiKhoan", "Accounts");
                        }
                        //dang ki thanh cong luu vao co so du lieu
                        _context.Add(khachhang);
                        await _context.SaveChangesAsync();
                        //Lưu Session MaKh khoi login lai CustomerId 
                        HttpContext.Session.SetString("CustomerId", khachhang.CustomerId.ToString());
                        var taikhoanID = HttpContext.Session.GetString("CustomerId");

                        //Identity ten dinh danh , DUA Vao ten dinh danh
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,khachhang.FullName),
                            new Claim("CustomerId", khachhang.CustomerId.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        //
                        await HttpContext.SignInAsync(claimsPrincipal);
                        _notyfService.Success("Đăng ký thành công");
                        //dang ki thanh cong tra ve trang dashbroad khong can dang nhap lai
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    catch 
                    {
                        //tao tai khoan that bai tra ve lai trang dangky
                        return RedirectToAction("DangkyTaiKhoan", "Accounts");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {
                return View(taikhoan);
            }
        }
        // login co session roi dua ve trang dashbroad neu khong tra ve trang login
        
        [AllowAnonymous]
        [Route("taikhoan/dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl)
        {
            //trang dang nhap
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                return RedirectToAction("Dashboard", "Accounts");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [Route("taikhoan/dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customer, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //kiem tra co phai email hop le hay ko
                    bool isEmail = Utilities.IsValidEmail(customer.UserName);
                    //khong phai email tra ve trang login lai
                    if (!isEmail) return View(customer);
                    //vao data base kiem tra co ton tai email khoan khach hang hay ko
                    var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);
                    //neu khong ton tai khoan ve trang dang ky
                    if (khachhang == null)
                    {
                        _notyfService.Error("Thông tin đăng nhập chưa chính xác");
                        /*return RedirectToAction("DangkyTaiKhoan");*/
                        return View(customer);
                    }

                    //ton tai thi hash lai pass = thong tin pass nhap + salt cua tai khoan do
                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
                    if (khachhang.Password != pass)
                    {
                        _notyfService.Error("Thông tin đăng nhập chưa chính xác");
                        return View(customer);
                    }
                    //kiem tra xem account co bi disable hay khong
                    //To do: disable nhung tai khoan dat hang ma khong nhan
                    if (khachhang.Active == false) return RedirectToAction("ThongBao", "Accounts");

                    //Luu Session MaKh
                    HttpContext.Session.SetString("CustomerId", khachhang.CustomerId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("CustomerId");

                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, khachhang.FullName),
                        new Claim("CustomerId", khachhang.CustomerId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notyfService.Success("Đăng nhập thành công");

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
            }
            catch 
            {
                return RedirectToAction("DangkyTaiKhoan", "Accounts");
            }
            return View(customer);
        }
        [HttpGet]
        [Route("taikhoan/dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }

        [Route("taikhoan/orders.html", Name = "Orders")]
        public IActionResult DanhSachOrder() //bo
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                //Tra ve view Dashbroad 
                var khachhang = _context.Customers
                    .AsNoTracking()
                    .SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
                if (khachhang != null)
                {
                    var lsDonHang = _context.Orders
                        .Include(x => x.TransactStatus)
                        .AsNoTracking()
                        .Where(x => x.CustomerId == khachhang.CustomerId)
                        .OrderByDescending(x => x.OrderDate)
                        .ToList();
                    ViewBag.DonHang = lsDonHang;
                    return View(khachhang);
                }

            }
            //else tra lai ve trang login
            return RedirectToAction("Login");
           
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _context.Customers.Find(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "Accounts");
                    var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    if(pass == taikhoan.Password)
                    {
                        string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
                        taikhoan.Password = passnew;
                        _context.Update(taikhoan);
                        _context.SaveChanges();
                        _notyfService.Success("Đổi mật khẩu thành công");
                        return RedirectToAction("Dashboard", "Accounts");
                    }
                    _notyfService.Success("Mật khẩu hiện tại không đúng");
                    return RedirectToAction("Dashboard", "Accounts");
                }
            }
            catch
            {
                _notyfService.Success("Thay đổi mật khẩu không thành công");
                return RedirectToAction("Dashboard", "Accounts");
            }
            _notyfService.Success("Thay đổi mật khẩu không thành công");
            return RedirectToAction("Dashboard", "Accounts");
        }
    }
}
