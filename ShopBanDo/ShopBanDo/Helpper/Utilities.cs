
﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
﻿using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ShopBanDo.Helpper
{
    public static class Utilities
    {
        public static string StripHTML(string input)
        {
            try
            {
                if(!string.IsNullOrEmpty(input))
                {
                    return Regex.Replace(input, "<.*?>", String.Empty);
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        public static bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; 
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static int PAGE_SIZE = 20;
        //Create File if missing
        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
        public static string ToTitleCase(string str)
        {
            string result = str;
            if (!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int index = 0; index < words.Length; index++)
                {
                    var s = words[index];
                    if (s.Length > 0)
                    {
                        words[index] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }
            return result;
        }
        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"^[0-9]+$");

            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }

                return true;

            }
            catch
            {

            }
            return false;

        }
        public static string GetRandomKey(int length = 5)
        {
            //chuỗi mẫu (pattern)
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }
        public static string SEOUrl(string url)
        {
            url = url.ToLower();
            url = Regex.Replace(url, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            url = Regex.Replace(url, @"[éèẹẻẽêếềệểễ]", "e");
            url = Regex.Replace(url, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            url = Regex.Replace(url, @"[íìịỉĩ]", "i");
            url = Regex.Replace(url, @"[ýỳỵỉỹ]", "y");
            url = Regex.Replace(url, @"[úùụủũưứừựửữ]", "u");
            url = Regex.Replace(url, @"[đ]", "d");

            //2. Chỉ cho phép nhận:[0-9a-z-\s]
            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            //xử lý nhiều hơn 1 khoảng trắng --> 1 kt
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            //thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");
            while (true)
            {
                if (url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }
                else
                {
                    break;
                }
            }
            return url;
        }
        //Upload ảnh
        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname = null)
        {
            try
            {
                if (newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", sDirectory, newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt.ToLower())) /// Khác các file định nghĩa thì không cho upload
                {
                    return null;
                }
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }
            }
            catch
            {
                return null;
            }
        }
        public static async Task<string> UploadFileForEmployee(IFormFile File)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/Admin", Path.GetFileNameWithoutExtension(File.FileName));
            Directory.CreateDirectory(FolderPath);
            var GetParentFolderPath=Directory.GetParent(FolderPath);
            var GetParentFolder = new DirectoryInfo(GetParentFolderPath.ToString()).Name;
            var GetSubFoler=new DirectoryInfo(FolderPath).Name;
            string CombinePath = GetParentFolder + "/" + GetSubFoler;
            var FilePath=Path.Combine(FolderPath, File.FileName);
            using var filestream=new FileStream(FilePath, FileMode.Create);
            await File.CopyToAsync(filestream);
            return CombinePath;
        }
        public static void MessageEmail(string emailFrom,string title, string body, string nameCustomer)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false);
            IConfiguration config = builder.Build();

            string host = config.GetValue<string>("Smtp:Host");
            int port = config.GetValue<int>("Smtp:Port");
            string fromEmail = config.GetValue<string>("Smtp:FromEmail");
            string userName = config.GetValue<string>("Smtp:UserName");
            string passWord = config.GetValue<string>("Smtp:Password");

            try
            {
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(host, port, SecureSocketOptions.SslOnConnect);
                    client.Authenticate(fromEmail, passWord);

                    var bodyBuilder = new BodyBuilder
                    {
                        HtmlBody = $"Hello {nameCustomer}" +
                        $"<p>{body}</p>" +
                        "<p>Thank you and best regard<p>" +
                        "<p>Male Shop Tel:0927479189</p>",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodyBuilder.ToMessageBody(),
                    };

                    message.From.Add(new MailboxAddress("Male Shop: ", userName));
                    message.To.Add(new MailboxAddress("Customer: ", emailFrom));
                    message.Subject = title;
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                Log.Error(ex.ToString());
            }

        }
    }
}
