using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBanDo.Models;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SendEmailController : Controller
    {
        private readonly dbshopContext _context;
        private IConfiguration configuration;
        public INotyfService _notyfService { get; }
        public SendEmailController(dbshopContext context, INotyfService notyfService, IConfiguration _configuration)
        {
            _context = context;
            _notyfService = notyfService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            ViewData["Email"] = new SelectList(_context.Customers, "Email", "FullName", "Email");
            return View();
        }

    }
}
