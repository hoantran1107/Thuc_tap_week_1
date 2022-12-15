namespace ShopBanDo.Controllers
{
    using AspNetCoreHero.ToastNotification.Abstractions;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using ShopBanDo.Extension;
    using ShopBanDo.Helpper;
    using ShopBanDo.Models;
    using ShopBanDo.ModelView;
    using ShopBanDo.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class AccountsController : Controller
    {

        private readonly dbshopContext _context;

        private CustomerRespository _customer;

        private OrderRespository _order;

        public INotyfService _notyfService { get; }

        public AccountsController(dbshopContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
            _customer = new CustomerRespository(context);
            _order = new OrderRespository(context);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone.ToLower());
            if (khachhang != null)
                return Json(data: "Số điện thoại : " + Phone + "đã được sử dụng");
            return Json(data: true);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
            if (khachhang != null)
                return Json(data: "Email : " + Email + " đã được sử dụng");
            return Json(data: true);
        }
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

        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard() //done
        {
            //lay ra session sau khi dang nhap CustomerID
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID == null) return RedirectToAction("Login");

            var khachhang = _customer.FindCustomerUsingSession(taikhoanID);
            var lsDonHang = _order.GetListOrderOfCustomer(khachhang.CustomerId);
            ViewBag.DonHang = lsDonHang;
            return View(khachhang);
        }
        public IActionResult DeleteOrder(int id) //done
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID == null) return RedirectToAction("Login");

            var khachhang = _customer.FindCustomerUsingSession(taikhoanID);
            var donhang = _order.GetById(id);
            var newdonhang = donhang;

            newdonhang.Deleted = true;
            newdonhang.TransactStatusId = 5;
            _order.Update(donhang, newdonhang, true); //true = SaveChanges() to database
            _notyfService.Success("Cancel order successful");
            return RedirectToAction("Dashboard");
        }

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
        public async Task<IActionResult> DangkyTaiKhoan(RegisterViewModel taikhoan) //done
        {
            if (!ModelState.IsValid) return View(taikhoan);

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
            if (_customer.GetAll().AsEnumerable().SingleOrDefault(x => x.Email.ToLower() == khachhang.Email.ToLower()) != null)
            {
                _notyfService.Error("This email has been used");
                return RedirectToAction("DangkyTaiKhoan", "Accounts");
            }

            _customer.Add(khachhang, true);

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
            _notyfService.Success("Resgister success");
            //dang ki thanh cong tra ve trang dashbroad khong can dang nhap lai
            return RedirectToAction("Dashboard", "Accounts");
        }

        [AllowAnonymous]
        [Route("taikhoan/dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl) //done
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
        public async Task<IActionResult> Login(LoginViewModel customer, string returnUrl) //done
        {

            if (!ModelState.IsValid) return View(customer);

            //kiem tra co phai email hop le hay ko
            bool isEmail = Utilities.IsValidEmail(customer.UserName);
            //khong phai email tra ve trang login lai
            if (!isEmail) return View(customer);
            //vao data base kiem tra co ton tai email khoan khach hang hay ko
            var khachhang = _customer.FindCustomerUsingUserEmail(customer.UserName);
            //neu khong ton tai khoan ve trang dang ky
            if (khachhang == null)
            {
                _notyfService.Error("Wrong email or password");
                /*return RedirectToAction("DangkyTaiKhoan");*/
                return View(customer);
            }

            //ton tai thi hash lai pass = thong tin pass nhap + salt cua tai khoan do
            string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
            if (khachhang.Password != pass)
            {
                _notyfService.Error("Wrong email or password");
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
            _notyfService.Success("Login Success");

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Dashboard", "Accounts");
            }
            else
            {
                return Redirect(returnUrl);
            }
        }

        [HttpGet]
        [Route("taikhoan/dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout() //done
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
        public IActionResult ChangePassword(ChangePasswordViewModel model) //done
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (!ModelState.IsValid) return RedirectToAction("Dashboard", "Accounts");

            var taikhoan = _customer.FindCustomerUsingSession(taikhoanID);
            if (taikhoan == null) return RedirectToAction("Login", "Accounts");

            var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();

            if (taikhoan.Password != pass)
            {
                _notyfService.Error("Wrong password");
                return RedirectToAction("Dashboard", "Accounts");
            }

            string passnew = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
            taikhoan.Password = passnew;

            _customer.UpdatePass(taikhoan, true);
            _notyfService.Success("Change password success");
            return RedirectToAction("Dashboard", "Accounts");
        }
    }
}
