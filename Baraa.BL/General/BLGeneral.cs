using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Baraa.Model;
using Microsoft.AspNetCore.Http;

namespace Baraa.BLL.General
{
   public  class BLGeneral : BaseEntity
    {
        IHttpContextAccessor httpContextAccessor;
        public BLGeneral(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public string KeyGeneratorNumbersOnly(int length)
        {
            int maxSize = length;
            char[] chars = new char[62];
            string a;
            a = "1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }
        public DateTime GetDateTimeNow()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            CultureInfo cultureinfo = new CultureInfo("en-US");
            string RESdate = "";//.ToString("G", cultureinfo);
            if (httpContextAccessor.HttpContext != null)
            {
                var cookie = httpContextAccessor.HttpContext.Request.Cookies["Language"];

                if (string.IsNullOrEmpty(cookie))
                {
                    if (cookie == "ar-EG")
                    {
                        RESdate = DateTime.Now.ToLocalTime().AddHours(0).ToString("MM/dd/yyyy hh:mm:ss tt");
                    }
                    else
                    {
                        RESdate = DateTime.Now.ToLocalTime().AddHours(0).ToString("MM/dd/yyyy hh:mm:ss tt");
                    }
                }
                else
                {
                    RESdate = DateTime.Now.ToLocalTime().AddHours(0).ToString("MM/dd/yyyy hh:mm:ss tt");
                }

            }
            else
            {
                RESdate = DateTime.Now.ToLocalTime().AddHours(0).ToString("MM/dd/yyyy hh:mm:ss tt");

            }

            DateTime dt = DateTime.Parse(RESdate);
            return dt;
        }
        public string RandomNumber(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }


    }
}
