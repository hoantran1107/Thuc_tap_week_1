using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopBanDo.Extension;
using ShopBanDo.Helpper;
using ShopBanDo.Models;

namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminChangePasswordController : Controller
    {
        private readonly dbshopContext _context;
        public INotyfService _notifyService { get; }

        public AdminChangePasswordController(dbshopContext context,INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        // GET: Admin/AdminChangePassword
        public async Task<IActionResult> Index()
        {
            var dbshopContext = _context.Accounts.Include(a => a.Role);
            return View(await dbshopContext.ToListAsync());
        }

        // GET: Admin/AdminChangePassword/Details/5
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

        // GET: Admin/AdminChangePassword/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return View();
        }

        // POST: Admin/AdminChangePassword/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountId,Phone,Email,Password,Salt,Active,Fullname,RoleId,LastLogin,CreateDate")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", account.RoleId);
            return View(account);
        }

        // GET: Admin/AdminChangePassword/Edit/5
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // POST: Admin/AdminChangePassword/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Account account, IFormFile UploadImage,string ConfirmPassword)
        {
            var ImagePath ="DefaultAdminAVT";
            if (id != account.AccountId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (account.Phone==ConfirmPassword)
                    {
                        account.ImageName = "face15.jpg";
                        if (UploadImage != null) { ImagePath = await Utilities.UploadFileForEmployee(UploadImage); account.ImageName = UploadImage.FileName; }         
                        account.ImagePath= ImagePath; 
                        string salt = Utilities.GetRandomKey();
                        account.Salt = salt;
                        account.Password = (account.Phone + salt.Trim()).ToMD5();
                        account.CreateDate = DateTime.Now;
                        //account.Active = true;
                        _notifyService.Success("Success");
                        _context.Update(account);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _notifyService.Warning("Password is not match");
                        ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
                        return View(account);
                    }
                    
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
                return RedirectToAction("Index", "Home", new {Areas="Admin"});
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", account.RoleId);
            return View(account);
        }

        // GET: Admin/AdminChangePassword/Delete/5
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

        // POST: Admin/AdminChangePassword/Delete/5
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
    }
}
