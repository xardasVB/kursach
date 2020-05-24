using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Authorize(Users = "besarabvitaliy@gmail.com,asd@asd.asd")]
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.Dashboard = true;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}