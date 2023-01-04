using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBanDo.Areas.Admin.Models;
using ShopBanDo.Extension;
using ShopBanDo.Helpper;
using ShopBanDo.Models;

namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAccountsController : Controller
    {
        private readonly dbshopContext _context;
        public INotyfService _notifyService { get; }

        public AdminAccountsController(dbshopContext context, INotyfService notyfService)
        {
            _notifyService = notyfService;
            _context = context;
        }
        [Authorize(Policy = "AdminOnly")]
        // GET: Admin/AdminAccounts
        public async Task<IActionResult> Index()
        {
            //loc cach 1
            ViewData["QuyenTruyCap"] = new SelectList(_context.Roles, "RoleId", "Description");
            //loc cach 2
            List<SelectListItem> isTrangThai = new List<SelectListItem>();
            isTrangThai.Add(new SelectListItem() { Text = "Active", Value = "1" });
            isTrangThai.Add(new SelectListItem() { Text = "Block", Value = "0" });
            ViewData["lsTrangThai"] = isTrangThai;

            var dbshopContext = _context.Accounts.Include(a => a.Role);
            return View(await dbshopContext.ToListAsync());
        }
        [Authorize(Policy = "AdminOnly")]
        // GET: Admin/AdminAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }
        [Authorize(Policy = "AdminOnly")]
        // GET: Admin/AdminAccounts/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Admin/AdminAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,Salt,Active,Fullname,RoleId,LastLogin,CreateDate")] Account account)
        {

            if (ModelState.IsValid)
            {
                if (_context.Accounts.AsNoTracking().FirstOrDefault(user => user.Email == account.Email) != null)
                {
                    _notifyService.Warning("Email is Exited");
                }
                else
                {
                    string salt = Utilities.GetRandomKey();
                    account.Salt = salt;
                    account.Password = (account.Phone + salt.Trim()).ToMD5();
                    account.CreateDate = DateTime.Now;
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    _notifyService.Success("Success");
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }
        [Authorize(Policy = "AdminOnly")]
        // GET: Admin/AdminAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }
        //GET :Admin/AdminAccounts/ChangePassword
        // POST: Admin/AdminAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountId,Phone,Email,Password,Salt,Active,Fullname,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string salt = Utilities.GetRandomKey();
                    account.Salt = salt;
                    account.Password = (account.Phone + salt.Trim()).ToMD5();
                    account.CreateDate = DateTime.Now;
                    _notifyService.Success("Success");
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }
        [Authorize(Policy = "AdminOnly")]
        // GET: Admin/AdminAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Admin/AdminAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

        [AllowAnonymous]
        [Route("Admin/AdminAccounts/Login", Name = "DangNhapAdmin")]
        public IActionResult Login(string returnUrl)
        {
            //trang dang nhap
            //var taikhoanID = HttpContext.Session.GetString("AccountId");
            //if (taikhoanID != null)
            //{
            //    return RedirectToAction("Login", "AdminAccounts");
            //}
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Admin/AdminAccounts/Login", Name = "DangNhapAdmin")]
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
                        ViewBag.Noty = "Incorrect Username or Password";
                        return View(account);
                    }

                    //ton tai thi hash lai pass = thong tin pass nhap + salt cua tai khoan do
                    string pass = (account.Password + admin.Salt.Trim()).ToMD5();
                    //string pass = account.Password;
                    if (admin.Password != pass)
                    {
                        ViewBag.Noty = "Incorrect Username or Password";
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
                        new Claim(ClaimTypes.Name, admin.Fullname),
                        new Claim("UserId",admin.AccountId.ToString()),
                        new Claim("AccountId", admin.AccountId.ToString()),
                        new Claim("Roles",admin.RoleId.ToString()),
                        new Claim("ImagePath",admin.ImagePath == null ? "face15.jpg" : admin.ImagePath ),
                        new Claim("ImageName",admin.ImageName == null ? "face15.jpg" : admin.ImageName)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal);
                    _notifyService.Success($"{admin.Email} is login");
                    if (string.IsNullOrEmpty(returnUrl))
                    {
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
                return RedirectToAction("Login");
            }
            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login","AdminAccounts");
        }
        [Route("Admin/AccessDenied", Name ="Denied")]
        [Authorize]
        public IActionResult ForbidPage()
        {
            return View();
        }
    }
}
