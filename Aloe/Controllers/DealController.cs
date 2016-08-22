using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aloe.DB_Context;
using Aloe.Helper;
using Aloe.Models;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Threading.Tasks;
using System.IO;
using iTextSharp.text.html.simpleparser;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;

namespace Aloe.Controllers
{
    [Authorize]
    //[AuthorizeUser("/Home/?Un-Auth=1", Helper.Roles.Admin, Helper.Roles.User, Helper.Roles.SuperUser, Helper.Roles.Guest)]
    public class DealController : Controller
    {
        private  DB_Main_Entities db = new DB_Main_Entities();
        private AloeUserInfo UserInfo = new AloeUserInfo();
        private DealTitle DTitle = new DealTitle();
        public ActionResult Index(string status = "inp")
        {
            AloeUserInfo Userinfo = new AloeUserInfo();
            var deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Initial || d.Status == (byte)Status.Review || d.Status == (byte)Status.Amendment || d.Status == (byte)Status.Completed).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).OrderByDescending(d => d.CreatedDate);
            //db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.i).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID);

            //var deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == 8 && (d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID));
            ViewBag.DealStatus = status;
            ViewBag.RoleID = Userinfo.UserRoleID;
            ViewBag.CurrentUserRoleID = Userinfo.UserID;
            return View(deals.ToList());
        }
        #region "DealListing"
        public ActionResult DealList(string status = "inp")
        {
            AloeUserInfo Userinfo = new AloeUserInfo();
            Deal dealsNew;
            var deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Initial).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID);

            //var deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == 0 && (d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID));

            if (status == "inp") deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Initial || d.Status == (byte)Status.Review || d.Status == (byte)Status.Amendment || d.Status == (byte)Status.Completed).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).OrderByDescending(d => d.CreatedDate);
            if (status == "complete") deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Finished).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).OrderByDescending(d => d.CreatedDate);
            if (status == "can")
            {
                //dealsNew = (Deal)db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Cancelled).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).OrderByDescending(d => d.CreatedDate);

                DateTime dt = DateTime.Today.Date;
                dt = dt.Date.AddDays(-30);
                deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Cancelled && DbFunctions.DiffDays(d.CancelDate.Value, dt.Date).Value < 30).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).OrderByDescending(d => d.CreatedDate);

                //dealsNew = (Deal)db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Cancelled && DbFunctions.DiffDays(d.CancelDate.Value, dt.Date).Value > 30).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).OrderByDescending(d => d.CreatedDate);


                //deals = db.Deals.Include(d => d.AspNetUser).Include(d => d.AspNetUser1).Include(d => d.AspNetUser2).Include(d => d.AspNetUser3).Include(d => d.LOITemplate).Where(d => d.Status == (byte)Status.Cancelled).ToList().Where(d => d.AgentID == Userinfo.UserID || d.TenantID == Userinfo.UserID || d.LandlordID == Userinfo.UserID || d.UserID == Userinfo.UserID).ToList().Where(d=> d.CancelDate.Value <= dt.Date).OrderByDescending(d => d.CreatedDate);

                //DateTime dt = DateTime.Today.Date;
                // dealsNew = deals.Where(a => a.CancelDate.Value.Date - dt).ToList();

            }

            ViewBag.DealStatus = status;
            ViewBag.RoleID = Userinfo.UserRoleID;
            ViewBag.CurrentUserRoleID = Userinfo.UserID;
            return PartialView("AllDeal", deals.OrderByDescending(b => b.ModifiedDate).ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            return View(deal);
        }
        #endregion
        #region "Deal Create"
        [AuthorizeUser("/Home/?Un-Auth=1", Helper.Roles.Admin, Helper.Roles.User)]
        public ActionResult Create()
        {
            
            DealPreviousSelection CurrentDeal = (DealPreviousSelection)TempData["DealPSelection"];

            EmptyTempSession();
            var Landlord = UserList(new String[] { "1" });
            var Tenant = UserList(new String[] { "4" });
            string AgentRole=((int)Aloe.Helper.UserRole.User).ToString();
            if (UserInfo.UserRoleName == Aloe.Helper.Roles.User) AgentRole = ((int)Aloe.Helper.UserRole.Admin).ToString();
            var AgentList = UserList(new String[] {AgentRole});
            ViewBag.TemplateID = new SelectList(db.MLOITemplates.OrderBy(d => d.Name), "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Template : null));
            ViewBag.AgentID = new SelectList(AgentList.OrderBy(d => d.Name), "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Agent : null));
            ViewBag.LandlordID = new SelectList(Landlord.OrderBy(d => d.Name), "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Landlord : null));
            ViewBag.TenantID = new SelectList(Tenant.OrderBy(d => d.Name), "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Tenant : null));
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            AloeUserInfo Userinfo = new AloeUserInfo();
            ViewBag.CurrentUserRoleID = Userinfo.UserRoleID;
                
            
            DealM Deal = new DealM();

            return View(Deal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreatedDate,ModifiedDate,PropertyID,TemplateID,UserID,AgentID,Status,TenantID,LandlordID,Property")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                deal.UserID = UserInfo.UserID;
                //deal.CreatedDate = ((DateTime)deal.CreatedDate) + DateTime.Now.TimeOfDay;
                deal.CreatedDate = DateTime.Now;
                deal.ModifiedDate = DateTime.Now;
                deal.Status = (int)Status.Initial;
                db.Deals.Add(deal);
                var DealTempID = deal.TemplateID;
                #region "Cloning Template"
                /// Cloning LoiTemplate from Master
                LOITemplate Ltp = db.MLOITemplates.Where(m => m.ID == DealTempID).ToList().Select(w => new LOITemplate
                {
                    ID = w.ID,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = (int)Status.Initial,
                    UserID = UserInfo.UserID,
                    Name = w.Name,
                    Description = w.Description
                }).SingleOrDefault();
                db.LOITemplates.Add(Ltp);
                db.SaveChanges();
                Int32 CTempID = Ltp.ID;
                if (CTempID > 0) deal.TemplateID = CTempID;
                List<LOITemplatePoint> LtpP = db.MLOITemplatePoints.Where(w => w.TemplateID == DealTempID).ToList().Select(w => new LOITemplatePoint()
                {
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = (int)Status.Initial,
                    TemplateID = CTempID,
                    InitialValue = w.InitialValue,
                    DealPoint = w.DealPoint
                }).ToList();
                db.LOITemplatePoints.AddRange(LtpP);
                db.SaveChanges();

                //******************************
                #endregion
                db.SaveChanges();
                var Dealid = deal.ID.ToString();
                var Tempid = deal.TemplateID.ToString();
                Tempid = RouteSecurity.encrypt(Tempid);
                Dealid = RouteSecurity.encrypt(Dealid);
                #region " Deal History / Notifications : Initializing Deal Points"
                try
                {
                    DealHistory(Helper.EntityNames.Deals, Dealid, Helper.DealHistory.DealPointsinitialized);
                    //** Notifications
                    DealHistory(Helper.EntityNames.Deals, Dealid, Helper.DealHistory.DealCreated,false);
                    DealHistory(Helper.EntityNames.loitemplatepoints, Dealid, Helper.DealHistory.DealNewPoints, false);
                }
                catch { }
                #endregion
                return RedirectToAction("DealInitialTerm", new { DealID = Dealid, TempID = Tempid });
            }
            DealPreviousSelection CurrentDeal = (DealPreviousSelection)TempData["DealPSelection"];
            var Landlord = UserList(new String[] { "1" });
            var Tenant = UserList(new String[] { "4" });
            var AgentList = UserList(new String[] { "2", "3" });
            ViewBag.TemplateID = new SelectList(db.MLOITemplates, "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Template : null));
            ViewBag.AgentID = new SelectList(AgentList, "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Agent : null));
            ViewBag.LandlordID = new SelectList(Landlord, "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Landlord : null));
            ViewBag.TenantID = new SelectList(Tenant, "ID", "Name", ((CurrentDeal != null) ? CurrentDeal.Tenant : null));
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            DealM Deal = new DealM() { AgentID = deal.AgentID, CreatedDate = ((deal.CreatedDate != null) ? deal.CreatedDate : DateTime.Now), ID = deal.ID, LandlordID = deal.LandlordID, ModifiedDate = deal.ModifiedDate, PropertyID = deal.PropertyID, Status = deal.Status, TemplateID = deal.TemplateID, TenantID = deal.TenantID, UserID = deal.UserID };
            return View(Deal);
        }
        #endregion
        #region "Deal Intital Terms"
        public ActionResult DealInitialTerm(string DealID = "0", string TempID = "0", bool status = false)
        {
            //DealID = RouteSecurity.decrypt(DealID); TempID = RouteSecurity.decrypt(TempID);
            int dealid = (DealID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(DealID)) : 0;
            int tempid = (TempID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            var TemplateID = (dealid > 0) ? db.Deals.Where(d => d.ID == dealid).Select(d => d.TemplateID).First() : tempid;
            IList<DB_Context.LOITemplatePoint> loiTEmp = db.LOITemplatePoints.Where(TID => TID.TemplateID == TemplateID).ToList();
            AloeUserInfo Userinfo = new AloeUserInfo();
            ViewBag.CurrentUserID = Userinfo.UserID;
            ViewBag.Msg = (status) ? "Initial Terms Saved Successfully" : string.Empty;
            #region "Deal Title"
            try
            {
                if (DTitle.TempName == null)
                { GetDealTile(dealid, tempid); }
                ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                ViewBag.DealSubTopTitle = '"' + DTitle.LandLordName + '"' + " AND " + '"' + DTitle.TenantName + '"';
            }
            catch { }
            #endregion

            return View(loiTEmp);
        }
        [HttpPost]
        public ActionResult SaveDealInitTerm(IList<LOITemplatePoint> LoiTempPoints, FormCollection frm, string DealID = "0")
        {
            var rp = frm["routestatus"];
            var TemplateID = "0";
            var Did = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (LOITemplatePoint LtemP in LoiTempPoints)
                    {
                        LtemP.ModifiedDate = DateTime.Now;
                        LtemP.Status = 1;
                        TemplateID = LtemP.TemplateID.ToString();
                        db.LOITemplatePoints.Attach(LtemP);
                        db.Entry(LtemP).Property(m => m.InitialValue).IsModified = true;
                        db.Entry(LtemP).Property(m => m.ModifiedDate).IsModified = true;
                        db.Entry(LtemP).Property(m => m.Status).IsModified = true;
                    }
                    #region " Deal History & Notifications: Initializing Deal Points"
                    try {
                        if (Convert.ToInt32(rp) > 0)
                        {
                            DealHistory(Helper.EntityNames.loitemplatepoints, DealID, Helper.DealHistory.DealPointsUpdated);
                            //** Notification
                            DealHistory(Helper.EntityNames.loitemplatepoints, DealID, Helper.DealHistory.DealPointsUpdated, false);
                        }
                        else
                        {
                            DealHistory(Helper.EntityNames.loitemplatepoints, DealID, Helper.DealHistory.DealPointsReview);
                            //** Notification
                            DealHistory(Helper.EntityNames.loitemplatepoints, DealID, Helper.DealHistory.DealPointsReview, false);
                        }
                    }
                    catch { }
                    #endregion
                    int Tid = Convert.ToInt32(TemplateID);
                    int dealID = (DealID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(DealID)) : 0;
                    if (Convert.ToInt32(rp) < 1)   // Saving/Submitting for Review 
                    {
                        Did = (dealID != 0) ? dealID : db.Deals.Where(m => m.TemplateID == Tid).Select(m => m.ID).FirstOrDefault();
                        Deal deal = db.Deals.Where(m => m.ID == Did).FirstOrDefault();
                        deal.Status = (int)Status.Review;
                        db.Deals.Attach(deal); db.Entry(deal).Property(m => m.Status).IsModified = true;
                        #region "Deal Title"
                        try
                        {
                            if (DTitle == null)
                            { GetDealTile(dealID, Tid); }
                            ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                            ViewBag.DealSubTopTitle = '"' + DTitle.ListingAgent + '"' + " AND " + '"' + DTitle.Agent + '"';
                        }
                        catch { }
                        #endregion
                    }
                        #region "Deal Modified Date"
                    try {
                        var dl = db.Deals.Where(m => m.ID == dealID).FirstOrDefault();
                        dl.ModifiedDate = DateTime.Now;
                    }
                    catch { }
                    #endregion 
                        db.SaveChanges();
                    if (Convert.ToInt32(rp) < 1) return RedirectToAction("DealClientReview", new { DealID = DealID, TempID = RouteSecurity.encrypt(TemplateID) });
                }
                catch { ModelState.AddModelError("", "Invalid Values"); }
            }
            TemplateID = RouteSecurity.encrypt(TemplateID.ToString());

            return RedirectToAction("DealInitialTerm", new { DealID = DealID, TempID = TemplateID, status = true });
        }
        public ActionResult EditTemplate(string DID,string TID)
        {
            if (TID == string.Empty)
            {
                int DealID=Convert.ToInt32(RouteSecurity.decrypt(DID));
                var TempID=db.Deals.Where(m=>m.ID==DealID).Select(m=>m.TemplateID).FirstOrDefault();
                TID = RouteSecurity.encrypt(TempID.ToString());
            }
            return RedirectToAction("EditTemplatePoints", "LOITemplates", new { TempID = TID, EditTemplate = true, DlID = DID });
        }
        #endregion
        #region "Deal Client Review"
        // (MA== : 0)
        public ActionResult DealClientReview(string DealID = "0", string TempID = "0", TStatus TracStatus = TStatus.Initial)
        {
            int dealid = (DealID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(DealID)) : 0;
            int tempid = (TempID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            LOITemplatePointM DealPoints = new LOITemplatePointM();
            var ActingDeal = db.Deals.Where(d => d.ID == dealid).FirstOrDefault();
            DealPoints.Status = Convert.ToByte(ActingDeal.Status);
            DealPoints.ClientT = (UserInfo.UserRoleName == Aloe.Helper.Roles.Guest) ? false : true;
            int TemplateID = (dealid > 0) ? ActingDeal.TemplateID : tempid;   // (TempID != "0" || TempID != string.Empty) ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            try
            {
                DealPoints.LoiTemPointsM = (from TemPoints in db.LOITemplatePoints
                                            join DlPoints in db.DealPoints on TemPoints.ID equals DlPoints.TemplatePointsID into Tbl
                                            from DlPoints in Tbl.DefaultIfEmpty()
                                            where (TemPoints.TemplateID == TemplateID)
                                            select (new LOITemplatePointM
                                            {
                                                CreatedDate = TemPoints.CreatedDate,
                                                DealPoint = TemPoints.DealPoint,
                                                //ID = (DlPoints.ID!=null)?DlPoints.ID:0,
                                                ID = TemPoints.ID,
                                                TempId = TemPoints.ID,
                                                InitialValue = TemPoints.InitialValue,
                                                ModifiedDate = TemPoints.ModifiedDate,
                                                Status = TemPoints.Status,
                                                TemplateID = TemPoints.TemplateID,
                                                DReview = DlPoints.DealReview,
                                                ModifiedBy = UserInfo.UserID,
                                                TemplatePointsID = 0,
                                                DpStatus = (DlPoints.Status != null) ? (Status)DlPoints.Status : Status.Created,
                                                CDealID = dealid
                                            })).OrderBy(m => m.ID).ToList();
                DealPoints.CheckAll = false; DealPoints.EncrptedTempID = (TempID != "0") ? TempID : RouteSecurity.encrypt(TemplateID.ToString());
                DealPoints.UserAllow = ValidateUser(UserInfo.UserID, dealid);
                #region "Deal Title"
                try
                {
                    if (DTitle.TempName == null)
                    { GetDealTile(dealid, tempid); }
                    ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                    ViewBag.DealSubTopTitle = '"' + DTitle.LandLordName + '"' + " AND " + '"' + DTitle.TenantName + '"';
                }
                catch { }
                #endregion
            }
            catch
            {
            }
            ViewBag.Msg = EnumDesc.GetValue(typeof(TStatusMsg), TracStatus);
            TracStatus = TStatus.Initial;
            return View(DealPoints);
        }
        [HttpPost]
        public ActionResult SaveDealClientReview(FormCollection Frm, LOITemplatePointM LoiTempPoints, string DlID = "0")
        {
            var Btn = Frm["BtnStatus"];
            var TemplateID = "0";
            TStatus TStat = TStatus.Initial;
            var RStatus = Status.Amendment;
            var DpStatus = Status.Accept;
            var Tpid = 0;
            var Did = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    TemplateID = LoiTempPoints.EncrptedTempID;
                    int dealid = (DlID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(DlID)) : 0;
                    if (Btn.Contains("_checkall") == false && (Btn.Contains("save") || Btn.Contains("draft")))
                    {
                        DpStatus = Status.Review;
                    }
                    IList<DealPoint> Dp = LoiTempPoints.LoiTemPointsM.Where(m => m.DpStatus == Status.Created).Select(m => new DealPoint
                    {
                        Deal_ID = dealid,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = UserInfo.UserID,
                        TemplatePointsID = m.ID,
                        DealReview = (m.DReview == "") ? "nil" : m.DReview,
                        Status = (byte)DpStatus
                       
                    }).ToList();
                    db.DealPoints.AddRange(Dp);
                    
                    foreach (LOITemplatePointM tdp in LoiTempPoints.LoiTemPointsM.Where(m => m.DpStatus == Status.Initial || m.DpStatus == Status.Review).ToList())
                    {
                        DealPoint tempDp = db.DealPoints.Where(m => m.TemplatePointsID == tdp.ID && m.Deal_ID == dealid).FirstOrDefault();
                        tempDp.Status = (byte)DpStatus; tempDp.DealReview = (tdp.DReview == "") ? "nil" : tdp.DReview;
                        db.SaveChanges();
                    }
                    #region " Deal History / Notifications : Initializing Deal Points"
                    try
                    {
                        if (DpStatus == Helper.Status.Accept)
                        {
                            foreach (LOITemplatePointM loitmpDP in LoiTempPoints.LoiTemPointsM.Where(m => m.DpStatus == Status.Created || m.DpStatus == Status.Initial || m.DpStatus == Status.Review))
                            {
                                string Notification = string.Empty;
                                Notification = (EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.DealPointAcptRejt) + '"' + loitmpDP.InitialValue + '"');
                                string Notify = string.Empty;
                                Notify = Notification + " is accepted " + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.joinsentence);
                                DealHistory(Helper.EntityNames.Deals, DlID, null, false, Notify);
                                //** for comment
                                if (loitmpDP.DReview != "" && loitmpDP.DReview!=null)
                                {
                                    Notification += EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.ClientComment) + '"' + loitmpDP.DReview + '"' + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.joinsentence);
                                    DealHistory(Helper.EntityNames.Deals, DlID, null, false, Notification);
                                }
                            }
                        }
                    }
                    catch { }
                    #endregion
                    Tpid = Convert.ToInt32(RouteSecurity.decrypt(TemplateID));
                    Did = (dealid != 0) ? dealid : db.Deals.Where(m => m.TemplateID == Tpid).Select(m => m.ID).FirstOrDefault();
                    if (Btn.Contains("save"))
                    {
                        var Dcomplete = db.DealPoints.Where(m => m.Deal_ID == dealid && m.Status != (int)Status.Accept && m.Status != (int)Status.Review).FirstOrDefault();
                        var DReview = (Dcomplete == null) ? Dp.Where(b => b.Status==(byte)Aloe.Helper.Status.Review || b.Status == (byte)Aloe.Helper.Status.Created || b.Status == (byte)Aloe.Helper.Status.Initial).FirstOrDefault() : null;
                        if (DReview == null && Dcomplete==null) DReview = db.DealPoints.Where(p =>p.Deal_ID==dealid && p.Status == (int)Aloe.Helper.Status.Review && p.Status!=(int)Aloe.Helper.Status.Reject).FirstOrDefault();
                        if (Dcomplete == null && DReview==null)
                        {
                            RStatus = Status.Completed;
                        }
                        else {if(DReview!=null) RStatus = Aloe.Helper.Status.Review; }
                        Deal deal = db.Deals.Where(m => m.ID == Did).FirstOrDefault();
                        deal.Status = (int)RStatus;
                        db.Deals.Attach(deal); db.Entry(deal).Property(m => m.Status).IsModified = true;
                    }
                    #region " Deal History: Amendment / Complete Deal Points"
                    try
                    {
                        if (Btn.Contains("draft")) DealHistory(Helper.EntityNames.loitemplatepoints, DlID, Helper.DealHistory.DealPointsReviewed);
                        else
                        {
                            if (RStatus == Status.Amendment) DealHistory(Helper.EntityNames.loitemplatepoints, DlID, Helper.DealHistory.DealAmendment);
                            else if (RStatus!=Helper.Status.Review) DealHistory(Helper.EntityNames.loitemplatepoints, DlID, Helper.DealHistory.DealComplete);
                        }
                    }
                    catch { }
                    #endregion
                    #region "Deal Modified Date"
                    try { 
                    var dl = db.Deals.Where(m => m.ID == Did).FirstOrDefault();
                    dl.ModifiedDate = DateTime.Now;
                    }
                    catch { }
                    #endregion 
                    db.SaveChanges();
                    TStat = TStatus.Saved;
                    
                    #region "Deal Title"
                    try
                    {
                        if (DTitle.TempName == null)
                        { GetDealTile(dealid, Tpid); }
                        ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                        ViewBag.DealSubTopTitle = '"' + DTitle.ListingAgent + '"' + " AND " + '"' + DTitle.Agent + '"';
                    }
                    catch { }
                    #endregion
                }
                catch { ModelState.AddModelError("", "Invalid Values"); TStat = TStatus.Error; }

            }
            if (Btn.Contains("draft")) return RedirectToAction("DealClientReview", new { DealID = DlID, TempID = TemplateID, TracStatus = TStat });
            else
            {
                if (RStatus != Status.Completed && RStatus != Status.Review) return RedirectToAction("DealAmendment", new { DealID = DlID, TempID = TemplateID, TracStatus = TStat });
                else if (RStatus == Status.Review) { TStat = TStatus.InCompleteReview; return RedirectToAction("DealClientReview", new { DealID = DlID, TempID = TemplateID, TracStatus = TStat }); }
                else return RedirectToAction("DealFinish", new { DealID = DlID, TempID = TemplateID });
            }
        }
        public ActionResult DealPAccept(string DReview, int TempPid = 0, string TempID = "MA==", bool DStatus = true, int dealid = 0, string DpITerm="")
        {
            try
            {
                string dealIDenc = string.Empty;
                DealPoint Dp = new DealPoint();
                Dp.DealReview = DReview; Dp.TemplatePointsID = TempPid; Dp.CreatedDate = DateTime.Now; Dp.ModifiedBy = UserInfo.UserID;
                Dp.Deal_ID = dealid;
                Dp.Status = (byte)((DStatus) ? Status.Accept : Status.Reject);
                DealPoint tempDp = db.DealPoints.Where(m => m.TemplatePointsID == TempPid && m.Deal_ID == dealid).FirstOrDefault();
                if (tempDp == null)
                {
                    db.DealPoints.Add(Dp);
                }
                else
                {
                    tempDp.Status = Dp.Status; tempDp.DealReview = DReview;
                }
                
                #region "Deal Modified Date"
                try
                {
                    var dl = db.Deals.Where(m => m.ID == dealid).FirstOrDefault();
                    dl.ModifiedDate = DateTime.Now;
                }
                catch { }
                #endregion 
                db.SaveChanges();
                dealIDenc = RouteSecurity.encrypt(dealid.ToString());
                #region " Deal Notifications : Reviewing Deal Points by Client"
                try
                {
                    string notificationtxt =string.Empty;
                    notificationtxt=(EnumDesc.GetValue(typeof(Helper.DealHistoryMsg),Helper.DealHistory.DealPointAcptRejt)+'"'+DpITerm+'"') ;
                    string Notify = string.Empty;
                    if (DStatus)
                    {
                        Notify = notificationtxt + " is accepted " + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.joinsentence);
                        DealHistory(Helper.EntityNames.DealPoints, dealIDenc, null, false, Notify);
                    }
                    else
                    {
                        Notify = notificationtxt + " is rejected" + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.joinsentence);
                        DealHistory(Helper.EntityNames.DealPoints, dealIDenc, null, false, Notify);
                    }
                    //** for comments
                    if (DReview != string.Empty && DReview != "")
                    {
                        notificationtxt = notificationtxt + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.ClientComment)+ '"'+DReview+'"' + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.joinsentence);
                        DealHistory(Helper.EntityNames.DealPoints, dealIDenc, null, false, notificationtxt);
                    }

                }
                catch { }
                #endregion
                return RedirectToAction("DealClientReview", new { DealID = dealIDenc, TempID = TempID });
            }
            catch { return RedirectToAction("DealClientReview", new { TempID = TempID }); }
        }
        public ActionResult DealPReject(int id = 0, int Tempid = 0, string TempID = "MA==")
        {
            DealPoint Dp = new DealPoint();
            Dp = db.DealPoints.Where(m => m.ID == id).FirstOrDefault();
            db.DealPoints.Remove(Dp);
            db.SaveChanges();
            return RedirectToAction("DealClientReview", new { TempID = TempID });
        }
        #endregion
        #region "Deal Amendment"
        public ActionResult DealAmendment(string TempID = "0", string DealID = "0", TStatus TracStatus = TStatus.Initial)
        { 
            int dealid = (DealID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(DealID)) : 0;
            int tempid = (TempID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            int TemplateID = (dealid > 0) ? db.Deals.Where(d => d.ID == dealid).Select(d => d.TemplateID).First() : tempid;
            //** Only Deal Creator have right to update Terms
            if (db.Deals.Where(dl => dl.UserID == UserInfo.UserID && dl.ID == dealid).FirstOrDefault() == null) return RedirectToAction("DealClientReview", "deal", new { DealID = DealID, TracStatus = TracStatus });
            //*********************************************
            LOITemplatePointM DealPoints = new LOITemplatePointM();
            //int TemplateID = (TempID != "0" || TempID != string.Empty) ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            try
            {
                DealPoints.LoiTemPointsM = (from TemPoints in db.LOITemplatePoints
                                            join DlPoints in db.DealPoints on TemPoints.ID equals DlPoints.TemplatePointsID into Tbl
                                            from DlPoints in Tbl.DefaultIfEmpty()
                                            //where (TemPoints.TemplateID == TemplateID)
                                            where (DlPoints.Deal_ID == dealid)
                                            select (new LOITemplatePointM
                                            {
                                                CreatedDate = TemPoints.CreatedDate,
                                                DealPoint = TemPoints.DealPoint,
                                                //ID = (DlPoints.ID!=null)?DlPoints.ID:0,
                                                ID = DlPoints.ID,
                                                TempId = TemPoints.ID,
                                                InitialValue = TemPoints.InitialValue,
                                                ModifiedDate = TemPoints.ModifiedDate,
                                                Status = TemPoints.Status,
                                                TemplateID = TemPoints.TemplateID,
                                                DReview = DlPoints.DealReview,
                                                ModifiedBy = UserInfo.UserID,
                                                TemplatePointsID = 0,
                                                DpStatus = (DlPoints.Status != null) ? (Status)DlPoints.Status : (Status)0,
                                            })).OrderBy(m => m.ID).ToList();
                DealPoints.CheckAll = false; DealPoints.EncrptedTempID = (TempID != "0") ? TempID : RouteSecurity.encrypt(TemplateID.ToString());
               
                #region "Deal Title"
                try
                {
                    if (DTitle.TempName == null)
                    { GetDealTile(dealid, TemplateID); }
                    ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                    ViewBag.DealSubTopTitle = '"' + DTitle.LandLordName + '"' + " AND " + '"' + DTitle.TenantName + '"';
                }
                catch { }
                #endregion
            }
            catch
            {
            }
            ViewBag.Msg = EnumDesc.GetValue(typeof(TStatusMsg), TracStatus);
            TracStatus = TStatus.Initial;
            return View(DealPoints);
        }
        [HttpPost]
        public ActionResult SaveDealAmendment(LOITemplatePointM LoiTempPoints, FormCollection Frm, string DID = "0")
        {
            var Btn = Frm["BtnStatus"];
            var TemplateID = "MA=="; TemplateID = LoiTempPoints.EncrptedTempID;
            TStatus TStat = TStatus.Initial;
            var Did = 0;
            var Tpid = 0;
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (LOITemplatePointM LTempPoints in LoiTempPoints.LoiTemPointsM.Where(m => m.DpStatus != Status.Accept && m.DpStatus != Status.Review))
                    {
                        var tp = db.LOITemplatePoints.Where(m => m.ID == LTempPoints.TempId).ToList();
                        tp.ForEach(m =>
                        {
                            m.InitialValue = LTempPoints.InitialValue;
                        });
                        var dp = db.DealPoints.Where(m => m.ID == LTempPoints.ID).ToList();
                        dp.ForEach(m =>
                        {
                            // m.DealReview= LTempPoints.DReview;
                            m.Status = (int)Status.Initial;
                        });
                        db.SaveChanges();
                    }
                    DID = (DID != "0") ? RouteSecurity.decrypt(DID) : "0";
                    Tpid = Convert.ToInt32(RouteSecurity.decrypt(TemplateID));
                    Did = (DID != "0") ? Convert.ToInt32(DID) : db.Deals.Where(m => m.TemplateID == Tpid).Select(m => m.ID).FirstOrDefault();
                    if (Btn.Contains("save"))
                    {
                        Deal deal = db.Deals.Where(m => m.ID == Did).FirstOrDefault();
                        deal.Status = (int)Status.Review;
                        db.Deals.Attach(deal); db.Entry(deal).Property(m => m.Status).IsModified = true;
                        #region " Deal Notifications : Amendment Deal Points by Creator"
                        try
                        {
                            foreach (LOITemplatePointM LTempdp in LoiTempPoints.LoiTemPointsM.Where(m => m.DpStatus != Status.Accept && m.DpStatus != Status.Review))
                            {
                                string notificationtxt = string.Empty;
                                notificationtxt = (EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.DealPointAcptRejt) + '"' + LTempdp.InitialValue + '"');
                                string Notify = string.Empty;
                                notificationtxt = notificationtxt + " is updated in amendment" + EnumDesc.GetValue(typeof(Helper.DealHistoryMsg), Helper.DealHistory.joinsentence);
                                DealHistory(Helper.EntityNames.DealPoints,RouteSecurity.encrypt(DID), null, false, notificationtxt);
                            }
                        }
                        catch { }
                        #endregion
                    }
                    #region "Deal Modified Date"
                    try { 
                    var dl = db.Deals.Where(m => m.ID == Did).FirstOrDefault();
                    dl.ModifiedDate = DateTime.Now;
                    }
                    catch { }
                    #endregion 
                    db.SaveChanges();
                    TStat = TStatus.Saved;
                    #region "Deal Title"
                    try
                    {
                        if (DTitle.TempName == null)
                        { GetDealTile(Did, Tpid); }
                        ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                        ViewBag.DealSubTopTitle = '"' + DTitle.ListingAgent + '"' + " AND " + '"' + DTitle.Agent + '"';
                    }
                    catch { }
                    #endregion
                   
                }
                catch { ModelState.AddModelError("", "Invalid Values"); TStat = TStatus.Error; }
            }
            DID = (DID != "0") ? RouteSecurity.encrypt(DID) : "0";
            #region " Deal History: Amendment / Review Deal Points"
            try
            {
                if (Btn.Contains("save")) DealHistory(Helper.EntityNames.loitemplatepoints, DID, Helper.DealHistory.DealAmendmented);
                else if (Btn.Contains("draft")) DealHistory(Helper.EntityNames.loitemplatepoints, DID, Helper.DealHistory.DealReviewAmendment);
            }
            catch { }
            #endregion
            if (Btn.Contains("draft")) return RedirectToAction("DealAmendment", new { DealID = DID, TempID = TemplateID, TracStatus = TStat });
            else return RedirectToAction("DealClientReview", new { DealID = DID, TempID = TemplateID });
        }
        private  string GetDealUserid()
        {
            AuthorizationContext Acc=new AuthorizationContext();
            int dealid=Convert.ToInt32(RouteSecurity.decrypt(Acc.Controller.ValueProvider.GetValue("DealID").AttemptedValue));
            return db.Deals.Where(dl=>dl.ID==dealid).Select(dl=>dl.UserID).SingleOrDefault();
        }
        #endregion
        #region "Deal Complete/Finish"
        public ActionResult DealFinish(string TempID = "0", string DealID = "0", TStatus TracStatus = TStatus.Initial)
        {
            int dealid = (DealID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(DealID)) : 0;
            int tempid = (TempID != "0") ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            int TemplateID = (dealid > 0) ? db.Deals.Where(d => d.ID == dealid).Select(d => d.TemplateID).First() : tempid;
            LOITemplatePointM DealPoints = new LOITemplatePointM();
            //int TemplateID = (TempID != "0" || TempID != string.Empty) ? Convert.ToInt32(RouteSecurity.decrypt(TempID)) : 0;
            try
            {
                DealPoints.LoiTemPointsM = (from TemPoints in db.LOITemplatePoints
                                            join DlPoints in db.DealPoints on TemPoints.ID equals DlPoints.TemplatePointsID into Tbl
                                            from DlPoints in Tbl.DefaultIfEmpty()
                                            where (TemPoints.TemplateID == TemplateID)
                                            select (new LOITemplatePointM
                                            {
                                                CreatedDate = TemPoints.CreatedDate,
                                                DealPoint = TemPoints.DealPoint,
                                                //ID = (DlPoints.ID!=null)?DlPoints.ID:0,
                                                ID = DlPoints.ID,
                                                TempId = TemPoints.ID,
                                                InitialValue = TemPoints.InitialValue,
                                                ModifiedDate = TemPoints.ModifiedDate,
                                                Status = TemPoints.Status,
                                                TemplateID = TemPoints.TemplateID,
                                                DReview = DlPoints.DealReview,
                                                ModifiedBy = UserInfo.UserID,
                                                TemplatePointsID = 0,
                                                DpStatus = (DlPoints.Status != null) ? (Status)DlPoints.Status : (Status)0,
                                                CDealID = DlPoints.Deal_ID
                                            })).OrderBy(m => m.DealPoint).ToList();
                DealPoints.CDealID = DealPoints.LoiTemPointsM[0].CDealID; DealPoints.CheckAll = false; DealPoints.EncrptedTempID = (TempID != "0") ? TempID : RouteSecurity.encrypt(TemplateID.ToString());
                if (dealid > 0) DealPoints.DpStatus = (Status)db.Deals.Where(m => m.ID == dealid).Select(f => f.Status).FirstOrDefault();
                DealPoints.UserAllow = ValidateUser(UserInfo.UserID, dealid);
                if (DealPoints.DpStatus == Status.Finished) TracStatus = TStatus.Finished;
                
                #region "Deal Title"
                try
                {
                    if (DTitle.TempName == null)
                    { GetDealTile(dealid, TemplateID); }
                    ViewBag.DealTopTitle = DTitle.TempName + " FOR " + DTitle.PropertyName;
                    ViewBag.DealSubTopTitle = '"' + DTitle.LandLordName + '"' + " AND " + '"' + DTitle.TenantName + '"';
                }
                catch { }
                #endregion

            }
            catch
            {
            }
            ViewBag.Msg = EnumDesc.GetValue(typeof(TStatusMsg), TracStatus);
            TracStatus = TStatus.Initial;
            return View(DealPoints);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDealFinish(LOITemplatePointM LoiTempPoints, FormCollection Frm, string DLiD = "0")
        {
            var Btn = Frm["BtnStatus"];
            var TemplateID = "MA=="; TemplateID = LoiTempPoints.EncrptedTempID;
            TStatus TStat = TStatus.Initial;
            var Did = 0;
            var Tpid = 0;
            string DealIdD = DLiD;
            if (ModelState.IsValid)
            {
                try
                {
                    if (Btn.Contains("finish"))
                    {
                        Tpid = Convert.ToInt32(RouteSecurity.decrypt(TemplateID));
                        if (LoiTempPoints.CDealID > 0)
                        {
                            Did = LoiTempPoints.CDealID;
                        }
                        else
                        {
                            DLiD = (DLiD != "0") ? RouteSecurity.decrypt(DLiD) : "0";
                            Did = (DLiD != "0") ? Convert.ToInt32(DLiD) : db.Deals.Where(m => m.TemplateID == Tpid).Select(m => m.ID).FirstOrDefault();
                        }
                        Deal deal = db.Deals.Where(m => m.ID == Did).FirstOrDefault();
                        deal.Status = (int)Status.Finished;
                        deal.ModifiedDate = DateTime.Now;
                        db.SaveChanges();
                        #region " Deal Notifications : Finishing Deal Points by Client"
                        try
                        {
                            DealHistory(Helper.EntityNames.DealPoints, DealIdD, Helper.DealHistory.DealFinished,false);
                        }
                        catch { }
                        #endregion
                    }
                    TStat = TStatus.Finished;
                    #region " Deal History: Amendment / Review Deal Points"
                    try
                    {
                        DealHistory(Helper.EntityNames.loitemplatepoints, DLiD, Helper.DealHistory.DealFinished);
                    }
                    catch { }
                    #endregion
                }
                catch { ModelState.AddModelError("", "Invalid Values"); TStat = TStatus.Error; }
            }
            DLiD = (Did > 0) ? RouteSecurity.encrypt(Did.ToString()) : "0";
            #region "Exporting"
            try
            {
                //if (Session["DealExport"] != null) Session["DealExport"] = null;
                //if (DTitle.TempName == null)
                //{ GetDealTile(Did, Tpid); }
                //DealDetail Dd = new DealDetail();
                //Dd.MDetail.TempName = DTitle.TempName; Dd.MDetail.PropertyName = DTitle.PropertyName;
                //Dd.MDetail.LandLordName = DTitle.LandLordName; Dd.MDetail.TenantName = DTitle.TenantName;
                //Dd.LoiTemPointsM = LoiTempPoints.LoiTemPointsM;
                //Session["DealExport"] = Dd;
            }
            catch { }
            #endregion
            return RedirectToAction("DealFinish","Deal", new { DealID = DLiD, TempID = TemplateID, TracStatus = TStat });
            // return RedirectToAction("Home");
        }
        #endregion
        #region "Deal Cancel"
        //[AuthorizeUser("/Home/?Un-Auth=2", Helper.Roles.Admin)]
        public ActionResult DealCancel(int DlID=0, string Msg="")
        {
            IList<DealM> Cd ;
            if (DlID > 0)
            {
                var deal = db.Deals.Where(dl => dl.ID == DlID).SingleOrDefault();
                deal.LastDealSttaus = deal.Status;
                deal.Status = (int)Status.Cancelled;
                deal.CancelDate = DateTime.Now;
                deal.Reason = Msg;
                db.SaveChanges();
                DlID = 0;
            }
            Cd= db.Deals.Where(dl => dl.Status != (byte)Status.Cancelled && dl.Status!=(byte)Status.Finished && dl.UserID==UserInfo.UserID).Select(dl => new DealM
            {
                ID=dl.ID, AgentID=dl.AgentID, CreatedDate=dl.CreatedDate, LandlordID=dl.LandlordID, ModifiedDate=dl.ModifiedDate, Property=dl.Property,
                 Status=dl.Status, TemplateID=dl.TemplateID, TenantID=dl.TenantID, UserID=dl.UserID
            }).OrderByDescending(dl => dl.ID).ToList();

            return RedirectToAction("index","Deal",null);
           // return View(Cd);
        }
        public ActionResult RestoreCancel(int DlID = 0)
        {
            IList<DealM> Cd;
            if (DlID > 0)
            {
                var deal = db.Deals.Where(dl => dl.ID == DlID).SingleOrDefault();
                deal.Status = (int)deal.LastDealSttaus;
                //deal.CancelDate = DateTime.Now;            
                db.SaveChanges();
                DlID = 0;
            }
            Cd = db.Deals.Where(dl => dl.Status != (byte)Status.Cancelled && dl.Status != (byte)Status.Finished && dl.UserID == UserInfo.UserID).Select(dl => new DealM
            {
                ID = dl.ID,
                AgentID = dl.AgentID,
                CreatedDate = dl.CreatedDate,
                LandlordID = dl.LandlordID,
                ModifiedDate = dl.ModifiedDate,
                Property = dl.Property,
                Status = dl.Status,
                TemplateID = dl.TemplateID,
                TenantID = dl.TenantID,
                UserID = dl.UserID
            }).OrderByDescending(dl => dl.ID).ToList();

            return RedirectToAction("index", "Deal",null);
            // return View(Cd);
        }
        #endregion
        #region "Deal Title"
        private void GetDealTile(int DealID, int TemplateID)
        {
            Deal TblDeal = db.Deals.Where(dl => dl.ID == DealID).SingleOrDefault();
            if (TblDeal != null)
            {
                if (TemplateID < 1) TemplateID = TblDeal.TemplateID;
                DTitle.TempName = (TemplateID > 0) ? db.LOITemplates.Where(t => t.ID == TemplateID).Select(tp => tp.Name).FirstOrDefault() : "nil";
                DTitle.PropertyName = TblDeal.Property;
                DTitle.LandLordName = db.AspNetUsers.Where(user => user.Id == TblDeal.LandlordID).Select(user => user.UserName).FirstOrDefault();
                DTitle.TenantName = db.AspNetUsers.Where(user => user.Id == TblDeal.TenantID).Select(user => user.UserName).FirstOrDefault();
                //DTitle.ListingAgent = db.AspNetUsers.Where(user => user.Id == TblDeal.UserID).Select(user => user.UserName).FirstOrDefault();
                //DTitle.Agent = db.AspNetUsers.Where(user => user.Id == TblDeal.AgentID).Select(user => user.UserName).FirstOrDefault();
            }
        }
        #endregion
        #region "Deal Messages"
        [HttpPost]
        public JsonResult ShowMessage(string Did)
        {
            
            int dealID = (Did != "0" && Did != "MA==") ? Convert.ToInt32(RouteSecurity.decrypt(Did)) : 0;
            List<DealMessageM> Dm = new List<DealMessageM>();
            Dm = db.DealMessages.Where(dm => dm.Receiver == UserInfo.UserID && dm.DealID == dealID).Select(d => new DealMessageM
            {  
             ID=d.ID, DealID=d.DealID, Sender=d.Sender,SenderName=db.AspNetUsers.Where(dd=>dd.Id==d.Sender).Select(ss=>ss.UserName).FirstOrDefault(), Message=d.Message, Status=(DMsgStatus)d.Status, SendDate=d.SendDate, ModifyDate=d.ModifyDate
            }).ToList();
            if (TempData["DMessageList"] != null && Dm.Count > 0) { TempData.Remove("DMessageList"); TempData["DMessageList"] = null; }
            TempData["DMessageList"] = Dm; TempData.Keep("DMessageList");
            return Json(Dm);
        }
        [HttpPost]
        public JsonResult SendMessage(string message, string Did, string receiver)
        {
            try
            {
                int dealID = (Did != "0" && Did != "MA==") ? Convert.ToInt32(RouteSecurity.decrypt(Did)) : 0;
                if (db.DealMessages.Where(dm => dm.Sender == UserInfo.UserID && dm.Receiver == receiver && dm.Message == message).SingleOrDefault() == null)
                {
                    DB_Context.DealMessage Dm = new DB_Context.DealMessage { DealID = dealID, Message = message, Sender = UserInfo.UserID, Receiver = receiver, Status = (int)DMsgStatus.Unread, SendDate = DateTime.Now, ModifyDate = DateTime.Now };
                    db.DealMessages.Add(Dm);
                    db.SaveChanges();
                }
                return Json(true);
            }
            catch { return Json(false); }
        }
        [HttpPost]
        public bool UpdateMessage(int Dmid,DMsgStatus Dmstatus)
        {
            try
            {
                var Dm = db.DealMessages.Where(dl => dl.ID == Dmid).SingleOrDefault();
                Dm.Status = (int)Dmstatus;
                Dm.ModifyDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        public JsonResult LoadReceiver(string dlID)
        {
            List<DealMRecieverM> DmM = new List<DealMRecieverM>();
            int deId = (dlID != "0" && dlID != "MA==") ? Convert.ToInt32(RouteSecurity.decrypt(dlID)) : 0;
            try
            {
                if (deId > 0)
                {
                    var deal = db.Deals.Where(d => d.ID == deId).SingleOrDefault();
                    #region "Finding Deal Creator Role.. Finding Lagent & Tagent"
                    var DealCreator = db.AspNetUserRoles.Where(r => r.UserId == deal.UserID).Select(r => r.RoleId).SingleOrDefault();
                    var LagentID = string.Empty; var Lagent = string.Empty;
                    var TagentID = string.Empty; var Tagent = string.Empty;
                    if (Convert.ToInt32(DealCreator) == (int)Aloe.Helper.UserRole.Admin)
                    {
                        LagentID = deal.UserID;
                        Lagent = db.AspNetUsers.Where(u => u.Id == deal.UserID).Select(ur => ur.UserName).SingleOrDefault();
                        TagentID = deal.AgentID;
                        Tagent = db.AspNetUsers.Where(u => u.Id == deal.AgentID).Select(ur => ur.UserName).SingleOrDefault();
                    }
                    else {
                        LagentID = deal.AgentID;
                        Lagent = db.AspNetUsers.Where(u => u.Id == deal.AgentID).Select(ur => ur.UserName).SingleOrDefault();
                        TagentID = deal.UserID;
                        Tagent = db.AspNetUsers.Where(u => u.Id == deal.UserID).Select(ur => ur.UserName).SingleOrDefault();
                    }
                    #endregion
                    if (UserInfo.UserRoleName == Aloe.Helper.Roles.Admin)       // Fill Landlord & TenantAgent
                    {
                        var landlord = db.AspNetUsers.Where(u => u.Id == deal.LandlordID).Select(ur => ur.UserName).SingleOrDefault();
                        DmM.Add(new DealMRecieverM { ID = deal.LandlordID, Receiver = landlord });
                        DmM.Add(new DealMRecieverM { ID = TagentID, Receiver = Tagent });
                    }
                    else if (UserInfo.UserRoleName == Aloe.Helper.Roles.User)   // Fill Tenant & LandlordAgent
                    {
                        var Tenant = db.AspNetUsers.Where(u => u.Id == deal.TenantID).Select(ur => ur.UserName).SingleOrDefault();
                        DmM.Add(new DealMRecieverM { ID = deal.TenantID, Receiver = Tenant });
                        DmM.Add(new DealMRecieverM { ID = LagentID, Receiver = Lagent });
                    }
                    else if (UserInfo.UserRoleName == Aloe.Helper.Roles.SuperUser) // filling Listing Agent
                    {
                        DmM.Add(new DealMRecieverM { ID = LagentID, Receiver = Lagent });
                    }
                    else if (UserInfo.UserRoleName == Aloe.Helper.Roles.Guest) // filling Tenant Agent
                    {
                        DmM.Add(new DealMRecieverM { ID = TagentID, Receiver = Tagent });
                    }
                }
            }
            catch { }
            return Json(DmM);
        }
        public JsonResult GetDMessage(int DmID)
        {
            List<DealMessageM> Dm = null;
            if (TempData["DMessageList"] != null)
            {
                Dm = (List<DealMessageM> )TempData["DMessageList"];
                TempData.Keep("DMessageList");
                 return Json(Dm.Where(m => m.ID == DmID).SingleOrDefault());
            }
             return Json(null);
           
        }
        [HttpPost]
        #endregion
        #region "Deal History / Notifications"
        private bool DealHistory(Helper.EntityNames EntityName,string Dealid,Helper.DealHistory? DHistory, bool history=true,string NotifMsg="")
        {
            try {
                if (Dealid != "" && Dealid != null && Dealid != "MA==")
                {
                    var DealHistoryMessage =(DHistory!=null)?EnumDesc.GetValue(typeof(DealHistoryMsg), DHistory).ToString() + UserInfo.UserName : string.Empty;
                    if (DHistory == null && NotifMsg != "") { DealHistoryMessage = NotifMsg + UserInfo.UserName; }
                    var SpParam=new object[5];
                    SpParam[0] = new SqlParameter() { ParameterName = "EntityName", Value = EntityName.ToString() };
                    SpParam[1] = new SqlParameter() { ParameterName = "DealID", Value = Dealid };
                    SpParam[2] = new SqlParameter() { ParameterName = "User", Value = UserInfo.UserID };
                    SpParam[3] = new SqlParameter() { ParameterName = "HistoryMessage", Value = DealHistoryMessage };
                    SpParam[4] = new SqlParameter() { ParameterName = "HistoryNotification", Value = history };   // History
                    var Dhistory = db.Database.ExecuteSqlCommand("Tbl_DealHistory @EntityName,@DealID,@User,@HistoryMessage,@HistoryNotification", SpParam);
                }
                return true; 
            }
            catch { return false; }
        }
        [HttpPost]
        public JsonResult GetDealHistory(string DlID, bool history = true, int? recordCount=10)
        {
            var DealHistory = new List<DealHistoryM>();
            var SqlCommand = "Get_DealHistory @DealID , @HistoryNotification , @RecordCount";
            var Param = new object[3];
            Param[0] = new SqlParameter() { ParameterName = "DealID", Value = DlID };
            Param[1] = new SqlParameter() { ParameterName = "HistoryNotification", Value = history };
            Param[2] = new SqlParameter() { ParameterName = "@RecordCount", Value = recordCount };
            DealHistory = db.Database.SqlQuery<DealHistoryM>(SqlCommand, Param).ToList();
            return Json(DealHistory);
        }
        #endregion
        #region "Graphs"
        [HttpPost]
        public JsonResult GetCircleReadings()
        {
            try
            {
                var circle = new List<Aloe.Models.CircleChart>();
                var User = new SqlParameter { ParameterName = "User", Value = UserInfo.UserID };
                var Readings = db.Database.SqlQuery<CircleChart>("GetCircleReadings @User", User).ToList();
                return Json(Readings);
            }
            catch { return Json(null); }
        }
        [HttpPost]
        public JsonResult GetProductivityGraph()
        {
            try
            {
                var ProdGraph = new List<Aloe.Models.ProductivityGraph>();
                var User = new SqlParameter { ParameterName = "User", Value = UserInfo.UserID };
                var Readings = db.Database.SqlQuery<ProductivityGraph>("GetProdGraph @User", User).ToList();
                return Json(Readings);
            }
            catch { return Json(null); }
        }
        #endregion
        #region "Others"
        private bool ValidateUser(string UserID,int DealID, bool VStatus = true)
        {
            //var Landlord = db.Deals.Where(landlord => landlord.LandlordID == UserID).FirstOrDefault();
            //if (Landlord != null) VStatus = false;
            //else if (db.Deals.Where(tenant => tenant.TenantID == UserID).FirstOrDefault() != null) VStatus = false;
            //else if (db.Deals.Where(creator => creator.UserID == UserID).FirstOrDefault() != null) VStatus = false;
            if (db.Deals.Where(eleget =>eleget.ID==DealID && eleget.UserID == UserID || eleget.LandlordID == UserID || eleget.TenantID == UserID).FirstOrDefault() != null) VStatus = false;
            return VStatus;
        }
        public void DownloadPDF(DealDetail DealDetail = null, string DlID = "0")
        {
            try
            {
                //if (Session["DealExport"] != null) DealDetail = (DealDetail)Session["DealExport"];
                if (DealDetail == null || DealDetail.LoiTemPointsM.Count < 1)
                {
                    #region "Geting Deal Details"
                    Int32 DealId = Convert.ToInt32(DlID);
                    var Deals = db.Deals.Where(d => d.ID == DealId).SingleOrDefault();
                    Int32 TempId = Convert.ToInt32(Deals.TemplateID);
                    #region "Deal Title"
                    try
                    {
                        if (DTitle.TempName == null)
                        { GetDealTile(DealId, TempId); }
                    }
                    catch { }
                    #endregion
                    DealDetail.LoiTemPointsM = (from TemPoints in db.LOITemplatePoints
                                                join DlPoints in db.DealPoints on TemPoints.ID equals DlPoints.TemplatePointsID into Tbl
                                                from DlPoints in Tbl.DefaultIfEmpty()
                                                where (TemPoints.TemplateID == TempId)
                                                select (new LOITemplatePointM
                                                {
                                                    DealPoint = TemPoints.DealPoint,
                                                    InitialValue = TemPoints.InitialValue,
                                                    DReview = DlPoints.DealReview,
                                                })).OrderBy(m => m.DealPoint).ToList();

                    DealDetail.MDetail.PropertyName = DTitle.PropertyName; DealDetail.MDetail.TempName = DTitle.TempName;
                    DealDetail.MDetail.LandLordName = DTitle.LandLordName; DealDetail.MDetail.TenantName = DTitle.TenantName;
                    #endregion
                }
                if (DealDetail != null && DealDetail.LoiTemPointsM.Count > 0)
                {
                    string ModelContent = string.Empty;
                    ModelContent = PrepareHtml(DealDetail);
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + DealDetail.MDetail.PropertyName + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(PDFDocument.GetPDF(ModelContent));
                    Response.End();
                }

            }
            catch { }
        }
        private string PrepareHtml<T>(T DealDetail) where T : DealDetail
        {
            string ModelContent = string.Empty;
            ModelContent = "<table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            ModelContent += @"<tr><td colspan='3' align='center'> <h3>DEAL DETAIL</h3> </td></tr> 
              <tr><td colspan='3' align='center'> <div style='width:5px; height:20px;'></div></td></tr> 
              <tr><td colspan='3'> <table width='100%' border='0' cellspacing='0' cellpadding='0'>";
            ModelContent += "<tr> <td >Property: " + DealDetail.MDetail.PropertyName + "</td> <td></td>";
            ModelContent += " <td >Template: " + DealDetail.MDetail.TempName + "</td>  </tr>";
            ModelContent += "<tr><td>Landlord: " + DealDetail.MDetail.LandLordName + "</td>  <td></td>";
            ModelContent += "  <td >Tenant: " + DealDetail.MDetail.TenantName + "</td> </tr>";
            ModelContent += @"</table> </td> </tr> 
              <tr><td colspan='3'>&nbsp;</td></tr> 
              <tr> <td colspan='3' align='center' valign='middle'>..................................................................................................................................................................</td> </tr> 
              <tr> 
              <td width='35%' valign='middle'>DEAL POINTS</td>  
              <td width='35%' valign='middle'>INITIAL TERMS</td> 
              <td width='30%' valign='middle'>CLIENT REVIEW</td>
              </tr> 
              <tr> <td colspan='3' align='center' valign='middle'>..................................................................................................................................................................</td> </tr>";
            foreach (LOITemplatePointM item in DealDetail.LoiTemPointsM)
            {
                ModelContent += "<tr><td>" + item.DealPoint + "</td>";
                ModelContent += "<td>" + item.InitialValue + "</td>";
                ModelContent += "<td>" + ((Convert.IsDBNull(item.DReview)) ? " " : item.DReview) + "</td>";
                ModelContent += "</tr>";
            }
            ModelContent = ModelContent + "</table>";
            return ModelContent;
        }
        public ActionResult ViewTemp([Bind(Include = "Template,CreatedDate,Agent,Landlord,Tenant")] DealPreviousSelection CurrentDeal)
        {
            TempData["DealPSelection"] = CurrentDeal;
            return RedirectToAction("TemplatePoints", "LOITemplates", new { TempID = RouteSecurity.encrypt(CurrentDeal.Template) });
        }
        // GET: Deal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal deal = db.Deals.Find(id);
            if (deal == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentID = new SelectList(db.AspNetUsers, "Id", "Email", deal.AgentID);
            ViewBag.LandlordID = new SelectList(db.AspNetUsers, "Id", "Email", deal.LandlordID);
            ViewBag.TenantID = new SelectList(db.AspNetUsers, "Id", "Email", deal.TenantID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", deal.UserID);
            ViewBag.TemplateID = new SelectList(db.LOITemplates, "ID", "Name", deal.TemplateID);
            return View(deal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreatedDate,ModifiedDate,PropertyID,TemplateID,UserID,AgentID,Status,TenantID,LandlordID")] Deal deal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentID = new SelectList(db.AspNetUsers, "Id", "Email", deal.AgentID);
            ViewBag.LandlordID = new SelectList(db.AspNetUsers, "Id", "Email", deal.LandlordID);
            ViewBag.TenantID = new SelectList(db.AspNetUsers, "Id", "Email", deal.TenantID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", deal.UserID);
            ViewBag.TemplateID = new SelectList(db.LOITemplates, "ID", "Name", deal.TemplateID);
            return View(deal);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private IEnumerable<dynamic> UserList(string[] Users)
        {
            return from Agents in db.AspNetUsers
                   join role in db.AspNetUserRoles.Where(roleid => Users.Contains(roleid.RoleId)) on Agents.Id equals role.UserId
                   select new { ID = Agents.Id, Name = Agents.UserName };
        }
        private void EmptyTempSession()
        {
            if (Session["QList"] != null) Session["QList"] = null;
            if (Session["QList2"] != null) Session["QList2"] = null;
        }
        #endregion
        
    }
    
   
}
