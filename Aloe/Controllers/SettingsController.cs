using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aloe.Helper;

namespace Aloe.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        //
        // GET: /Settings/
        AloeUserInfo Userinfo = new AloeUserInfo();
        public ActionResult Index()
        {

            ViewBag.Title = Userinfo.UserName;
            ViewBag.ID = Userinfo.UserID;
            return View();
        }
	}
}