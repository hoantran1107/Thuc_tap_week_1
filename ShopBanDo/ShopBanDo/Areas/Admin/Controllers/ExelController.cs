using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using ShopBanDo.Helpper;
using ShopBanDo.Interface;
using ShopBanDo.Models;
using ShopBanDo.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ShopBanDo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExelController : Controller
    {
        private ProductRepository _productservices;
        public IConfiguration Configuration { get; }
        private readonly dbshopContext _context;
        private readonly IUnitOfWork _uow;
        public ExelController(dbshopContext context, IUnitOfWork uow)
        {
            _context = context;
            _productservices = new ProductRepository(context);
            _uow = uow;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ImportProducts(IFormFile batchUsers)
        {

            if (batchUsers?.Length == 0) return RedirectToAction("Index", "AdminProducts");
            else
            {
                var stream = batchUsers.OpenReadStream();
                List<Product> products = new List<Product>();

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();//package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (var row = 5; row <= rowCount; row++)
                    {
                        try
                        {
                            var id = worksheet.Cells[row, 1].Value?.ToString();
                            var name = worksheet.Cells[row, 2].Value?.ToString();
                            var price = worksheet.Cells[row, 3].Value?.ToString();
                            var Discount = worksheet.Cells[row, 4].Value?.ToString();
                            var CatId = worksheet.Cells[row, 5].Value?.ToString();
                            var UnitslnStock = worksheet.Cells[row, 6].Value?.ToString();
                            var Active = worksheet.Cells[row, 7].Value?.ToString();
                            var BestSeller = worksheet.Cells[row, 8].Value?.ToString();
                            var HomeFlag = worksheet.Cells[row, 9].Value?.ToString();
                            var Title = worksheet.Cells[row, 10].Value?.ToString();
                            var Tags = worksheet.Cells[row, 11].Value?.ToString();
                            var MetaDesc = worksheet.Cells[row, 12].Value?.ToString();
                            var MetaKey = worksheet.Cells[row, 13].Value?.ToString();
                            var Description = worksheet.Cells[row, 15].Value?.ToString();
                            var Anh = worksheet.Cells[row, 14].Value?.ToString();
                            if (Anh == "0") Anh = "default.jpg";
                            var product = new Product()
                            {
                                
                                ProductName = Utilities.ToTitleCase(name),
                                Price = Convert.ToInt32(price),
                                Discount = Convert.ToInt32(Discount),
                                CatId = Convert.ToInt32(CatId),
                                UnitslnStock = Convert.ToInt32(UnitslnStock),
                                Active = Convert.ToBoolean(Active),
                                BestSeller = Convert.ToBoolean(BestSeller),
                                HomeFlag = Convert.ToBoolean(HomeFlag),
                                Title = Title,
                                Tags = Tags,
                                MetaDesc = MetaDesc,
                                MetaKey = MetaKey,
                                Description = Description,
                                Thumb = Anh,
                                DateCreated = DateTime.Now,
                                Alias = Utilities.SEOUrl(name),
                            };
                            if (id == null)
                            {
                                _uow.GetRepository<Product>().Add(product, true);
                            }
                            else
                            {

                               
                                product.ProductId = Convert.ToInt32(id);
                                _context.Update(product);
                            }
                        }
                        catch
                        {
                            await _uow.Rollback();
                            Console.WriteLine("Something went wrong");
                        }
                    }
                    await _uow.Commit();
                }
                return RedirectToAction("Index", "AdminProducts");
            }
        }
        public IActionResult ExportToExcel()
        {
            // Get the user list 
            var users = _productservices.GetAll().ToList();

            var stream = new MemoryStream();
            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Users");
                var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
                namedStyle.Style.Font.UnderLine = true;
                namedStyle.Style.Font.Color.SetColor(Color.Blue);
                const int startRow = 5;
                var row = startRow;

                //Create Headers and format them
                worksheet.Cells["A1"].Value = "List Products ";
                using (var r = worksheet.Cells["A1:O1"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(23, 55, 93));
                }
                
                worksheet.Cells["A2"].Value = "Product without no id mean create and with id mean update ";
                using (var r = worksheet.Cells["A2:O2"])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(40, 50, 93));
                }
                
                worksheet.Cells["A4"].Value = "Id";
                worksheet.Cells["B4"].Value = "Product Name";
                worksheet.Cells["C4"].Value = "Price";
                worksheet.Cells["D4"].Value = "Discount";
                worksheet.Cells["E4"].Value = "CatId";
                worksheet.Cells["F4"].Value = "UnitslnStock";
                worksheet.Cells["G4"].Value = "Active";
                worksheet.Cells["H4"].Value = "BestSeller";
                worksheet.Cells["I4"].Value = "HomeFlag";
                worksheet.Cells["J4"].Value = "Title";
                worksheet.Cells["K4"].Value = "Tags";
                worksheet.Cells["L4"].Value = "MetaDesc";
                worksheet.Cells["M4"].Value = "MetaKey";
                worksheet.Cells["N4"].Value = "Thumb";
                worksheet.Cells["O4"].Value = "Description";
                worksheet.Cells["A4:O4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A4:O4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                worksheet.Cells["A4:O4"].Style.Font.Bold = true;

                row = 5;
                foreach (var user in users)
                {
                    worksheet.Cells[row, 1].Value = user.ProductId;
                    worksheet.Cells[row, 2].Value = user.ProductName;
                    worksheet.Cells[row, 3].Value = user.Price;
                    worksheet.Cells[row, 4].Value = user.Discount;
                    worksheet.Cells[row, 5].Value = user.CatId;
                    worksheet.Cells[row, 6].Value = user.UnitslnStock;
                    worksheet.Cells[row, 7].Value = user.Active;
                    worksheet.Cells[row, 8].Value = user.BestSeller;
                    worksheet.Cells[row, 9].Value = user.HomeFlag;
                    worksheet.Cells[row, 10].Value = user.Title;
                    worksheet.Cells[row, 11].Value = user.Tags;
                    worksheet.Cells[row, 12].Value = user.MetaDesc;
                    worksheet.Cells[row, 13].Value = user.MetaKey;
                    worksheet.Cells[row, 14].Value = user.Thumb;
                    worksheet.Cells[row, 15].Value = user.Description;

                    row++;
                }

                // set some core property values
                xlPackage.Workbook.Properties.Title = "Product List";
                xlPackage.Workbook.Properties.Author = "shopbando";
                xlPackage.Workbook.Properties.Subject = "Product List";
                // save the new spreadsheet
                xlPackage.Save();
                // Response.Clear();
            }
            stream.Position = 0;
            DateTime date = DateTime.Now;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"product_{date}.xlsx");
        }
    }
}
