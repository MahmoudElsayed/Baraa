using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace Baraa.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ChangeLang(string LangId)
        {
            if (LangId != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LangId);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LangId);
            }
            HttpContext.Response.Cookies.Append("Language",LangId);
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
    }

}