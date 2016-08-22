using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Aloe.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Aloe.Helper;
 

namespace Aloe.Controllers
{
    //** Use the url below(i.e."/Account/Login") for UnAuthorized roles. This url only redirects for UnAuthorized Roles only 
    //** Below AuthorizeUser Attribute can be used for Class as wel as for Methods/ActionResults
   // [AuthorizeUser("/Account/Login", Roles.Admin, Roles.User)]
    [Authorize]
    public class HomeController : Controller
    {
        // Use this class object to get loggedin user info (i.e. ID,Name,Role,Email)
        DB_Context.DB_Main_Entities db = new DB_Context.DB_Main_Entities();
         AloeUserInfo Userinfo = new AloeUserInfo();
         #region "Home Default"
         public ActionResult Index()
         {
             //string UserName = Userinfo.UserName;
             //string str = Enum.GetName(typeof(AppRole), Userinfo.UserRoleName);
             try { TempData.Remove("DealPSelection"); }
             catch { }
             AllDeal();
             ViewBag.UName = Userinfo.UserName;
             return View();
         }
         #endregion
         #region "Deal Listing"
         public ActionResult AllDeal()
        {
            AloeUserInfo Userinfo = new AloeUserInfo();
            var deals = db.Deals.Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).ToList().Where(m => m.Status == (byte)Status.Initial || m.Status == (byte)Status.Review || m.Status == (byte)Status.Amendment || m.Status == (byte)Status.Completed);
            ViewBag.RoleID = Userinfo.UserRoleID;
            ViewBag.CurrentUserRoleID = Userinfo.UserID;
            return PartialView("AllDeal", deals.OrderByDescending(b=>b.ModifiedDate).ToList());
        }
        [HttpGet]
        public ActionResult AllDealSearch(string Search)
        {
            //var deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == id);              

            var deals = db.Deals.Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).ToList().Where(d => d.Status == (byte)Status.Initial || d.Status == (byte)Status.Review || d.Status == (byte)Status.Amendment || d.Status == (byte)Status.Completed).Where(d => d.Property.Replace(" ", "").ToUpper().Contains(Search.Replace(" ", "").ToUpper())).OrderByDescending(d => d.CreatedDate);
              
            
            //var deals = db.Deals.Where(d => d.Status == 0 || d.Status == 5 || d.Status == 6 && (d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID)).Where(d => d.Property.Replace(" ", "").Contains(Search.Replace(" ", ""))).OrderByDescending(d => d.CreatedDate);



            if (Search == "") { deals = db.Deals.Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).ToList().Where(d => d.Status == (byte)Status.Initial || d.Status == (byte)Status.Review || d.Status == (byte)Status.Amendment).OrderByDescending(d => d.CreatedDate); }
            //if (Search == "") { deals = db.Deals.Where(d => d.Status == 0 || d.Status == 5 || d.Status == 6 && (d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID)).OrderByDescending(d => d.CreatedDate); }
            if (deals.ToList().Count == 0)
            { ViewBag.NoRecord = "No Record found."; }
            ViewBag.RoleID = Userinfo.UserRoleID;
            return PartialView("AllDeal", deals.ToList());
        }
        #endregion
         #region "About Contact"
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
        #endregion
         #region "Deal Notifications"
        public ActionResult Notification()
        {
            IList<DNotificationM> Notifications = db.DealHistories.Where(DNotify => DNotify.HistoryNotification == false && DNotify.UserID!=Userinfo.UserID).Select(log => new DNotificationM
            {
                id=log.ID, LogDate=log.LogDate, message=log.HistoryMessage
            }).OrderByDescending(log => log.LogDate).Take(6).ToList();
            return View(Notifications);
        }
        public ActionResult LoadPreviousNotification()
        {
            IList<DNotificationM> Notifications = db.DealHistories.Where(DNotify => DNotify.HistoryNotification == false && DNotify.UserID != Userinfo.UserID).Select(log => new DNotificationM
            {
                id = log.ID,
                LogDate = log.LogDate,
                message = log.HistoryMessage
            }).OrderByDescending(log => log.LogDate).ToList();
            return View("Notification",Notifications);
        }
        #endregion
    }
  
}