using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ShopBanDo.Areas.Admin.Models;
using ShopBanDo.Extension;
using ShopBanDo.Helpper;
using ShopBanDo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly dbshopContext _context;

        public INotyfService _notifyService { get; }
        public LoginController(dbshopContext context,INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Admin/LoginAdmin/Login", Name = "DangNhapAdmin")]
        public IActionResult Login(string returnUrl)
        {
            //trang dang nhap
            var taikhoanID = HttpContext.Session.GetString("AccountId");
            if (taikhoanID != null)
            {
                return RedirectToAction("Login", "AdminAccounts");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Admin/LoginAdmin/Login", Name = "DangNhapAdmin")]
        public async Task<IActionResult> Login(LoginViewModel account, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //kiem tra co phai email hop le hay ko
                    bool isEmail = Utilities.IsValidEmail(account.Email);
                    //khong phai email tra ve trang login lai
                    if (!isEmail) return View(account);
                    //vao data base kiem tra co ton tai email khoan khach hang hay ko
                    var admin = _context.Accounts.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == account.Email);
                    //neu khong ton tai khoan ve trang dang ky
                    if (admin == null)
                    {
                        _notifyService.Error("Thông tin đăng nhập chưa chính xác");
                        /*return RedirectToAction("DangkyTaiKhoan");*/
                        return View(account);
                    }

                    //ton tai thi hash lai pass = thong tin pass nhap + salt cua tai khoan do
                    string pass = (account.Password + admin.Salt.Trim()).ToMD5();
                    if (admin.Password != pass)
                    {
                        _notifyService.Error("Thông tin đăng nhập chưa chính xác");
                        return View(account);
                    }
                    //kiem tra xem account co bi disable hay khong
                    //To do: disable nhung tai khoan dat hang ma khong nhan
                    if (admin.Active == false) return RedirectToAction("ThongBao", "Accounts");

                    //Luu Session MaKh
                    HttpContext.Session.SetString("AccountId", admin.AccountId.ToString());
                    var taikhoanID = HttpContext.Session.GetString("AccountId");

                    //Identity
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email,admin.Email),
                        new Claim("AccountId", admin.AccountId.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notifyService.Success("Đăng nhập thành công");

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        Log.Information(User.Identity.Name);
                        return RedirectToAction("Index", "Home", new { Areas = "Admin" });
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
            return View(account);
        }
    }
}
