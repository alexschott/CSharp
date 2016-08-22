using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aloe.DB_Context;
using Aloe.Models;
using Aloe.Helper;


namespace Aloe.Controllers
{
     [Authorize]
    public class LOITemplatesController : Controller
    {
        private DB_Main_Entities db = new DB_Main_Entities();
        private LOITemplateEntity LPOints = new LOITemplateEntity();
        public LOITemplateEntity TEMPLPOints = new LOITemplateEntity();
        private Aloe.Helper.AloeUserInfo AppUser = new Helper.AloeUserInfo();
        AloeUserInfo Userinfo = new AloeUserInfo();
        public ActionResult Index()
        {
            var lOITemplates = db.MLOITemplates.Include(l => l.AspNetUser).OrderByDescending(I => I.ID);
            ViewBag.UserRole = Userinfo.UserRoleID;
            return View(lOITemplates.ToList());
        }
        #region "Editing Sub Template"
        [AuthorizeUser("/Home/", Roles.Admin, Roles.User)]
        public ActionResult EditTemplatePoints(string TempID, TStatus TracMsg = TStatus.Initial,String DlID="0")
        {
            EmptyTempSession();
            if (TempID != null && TempID != string.Empty) TempID = RouteSecurity.decrypt(TempID);
            if (TempID == null || TempID == string.Empty)
            {
                LPOints.UserID = AppUser.UserID;
                LPOints.CreatedDate = DateTime.Now;
                LPOints.ModifiedDate = DateTime.Now;
                return View(LPOints);
            }
            else
            {
                LOITemplate lOITemplate = db.LOITemplates.Find(Convert.ToInt32(TempID));
                LPOints.UserID = lOITemplate.UserID;
                LPOints.CreatedDate = lOITemplate.CreatedDate;
                LPOints.ModifiedDate = lOITemplate.ModifiedDate;
                LPOints.Name = lOITemplate.Name;
                LPOints.Description = lOITemplate.Description;
                LPOints.Status = lOITemplate.Status;
                LPOints.ID = Convert.ToInt32(lOITemplate.ID);
                Int32 TemplateID = Convert.ToInt32(TempID);
                List<LOITemplatePoint> QuestionList = db.LOITemplatePoints.Where(m => m.TemplateID == TemplateID).ToList();
                List<LOITemplatePointEntity> mModel = new List<LOITemplatePointEntity>();
                foreach (var c in QuestionList)
                {
                    LOITemplatePointEntity temp = new LOITemplatePointEntity();
                    temp.TemplateID = c.TemplateID;
                    temp.ID = c.ID;
                    temp.DealPoint = c.DealPoint;
                    temp.CreatedDate = c.CreatedDate;
                    temp.ModifiedDate = c.ModifiedDate;
                    temp.Status = c.Status;
                    mModel.Add(temp);
                }
                LPOints.LOITempPoints = mModel;
                Session["QList"] = null;
                Session["QList"] = mModel;
                if (TracMsg == TStatus.Saved) ViewBag.SMsg = "LOITemplate saved successfully.";

                return View("TemplatePoints", LPOints);
            }
        }
        public ActionResult SaveEditTemp(LOITemplateEntity mModelS)
        {
            List<LOITemplatePointEntity> QPoints = (Session["QList"] != null) ? ((List<LOITemplatePointEntity>)Session["QList"]) : new List<LOITemplatePointEntity>();
            List<LOITemplatePointEntity> QPoint2s = (Session["QList2"] != null) ? ((List<LOITemplatePointEntity>)Session["QList2"]) : new List<LOITemplatePointEntity>();

            mModelS.LOITempPoints = QPoints;
            try
            {
                /// Saving/Inserting of LOITemplate
                var Template = new LOITemplate();
                var TemplateNew = new LOITemplate();
                var TemplatePoints = new LOITemplatePoint();
                Template.CreatedDate = mModelS.CreatedDate; Template.ModifiedDate = DateTime.Now; Template.Status = mModelS.Status;
                Template.UserID = mModelS.UserID; Template.Name = mModelS.Name; Template.Description = mModelS.Description; Template.ID = mModelS.ID;
                foreach (var Tpoints in QPoints)
                {
                    if (mModelS.ID != 0)
                    {
                        if (Tpoints.TemplateID != 0)
                        {
                            Template.LOITemplatePoints.Add(new LOITemplatePoint()
                            {
                                CreatedDate = Tpoints.CreatedDate,
                                ModifiedDate = Tpoints.CreatedDate,
                                Status = 0,
                                InitialValue = string.Empty,
                                DealPoint = Tpoints.DealPoint,
                                ID = Tpoints.ID,
                                TemplateID = Tpoints.TemplateID
                            });
                        }
                        else
                        {
                            TemplateNew.LOITemplatePoints.Add(new LOITemplatePoint()
                            {
                                CreatedDate = Tpoints.CreatedDate,
                                ModifiedDate = Tpoints.CreatedDate,
                                Status = 0,
                                InitialValue = string.Empty,
                                DealPoint = Tpoints.DealPoint,
                                TemplateID = mModelS.ID
                            });
                        }
                    }
                    else
                    {
                        Template.LOITemplatePoints.Add(new LOITemplatePoint()
                        {
                            CreatedDate = Tpoints.CreatedDate,
                            ModifiedDate = Tpoints.CreatedDate,
                            Status = 0,
                            InitialValue = string.Empty,
                            DealPoint = Tpoints.DealPoint
                        });


                    }
                }
                if (mModelS.ID == 0)
                {
                    db.LOITemplates.Add(Template);
                }
                else
                {
                    db.LOITemplates.Attach(Template);
                    db.Entry(Template).Property(m => m.Name).IsModified = true;
                    db.Entry(Template).Property(m => m.Description).IsModified = true;
                    string paramValue = "";
                    foreach (LOITemplatePoint Lp in Template.LOITemplatePoints)
                    {
                        db.LOITemplatePoints.Attach(Lp);
                        db.Entry(Lp).Property(m => m.DealPoint).IsModified = true;
                        paramValue = paramValue + Lp.ID.ToString() + ",";

                    }
                    foreach (LOITemplatePoint Lp in TemplateNew.LOITemplatePoints)
                    {
                        db.LOITemplatePoints.Add(Lp);
                    }
                    DbContext d; d = db;
                    paramValue = paramValue.Remove(paramValue.Length - 1);
                    string sql = "delete from [LOITemplatePoints] where id not in (" + paramValue + ") and TemplateID=" + Template.ID.ToString();
                    d.Database.ExecuteSqlCommand(sql);
                }
                db.SaveChanges();
                var TepID = Template.ID.ToString();
                EmptyFields();
                TempData["Msg"] = "LOITemplate saved successfully.";
                Session["QList"] = null;
                //  return RedirectToAction("Create", "Deal");
                int DealID =(mModelS.ID > 0) ? mModelS.ID : Convert.ToInt32(TepID);
                 DealID =db.Deals.Where(d => d.TemplateID == DealID).Select(dl => dl.ID).FirstOrDefault();
                return RedirectToAction("EditTemplatePoints", new { TempID = RouteSecurity.encrypt(TepID), TracMsg = TStatus.Saved, EditTemplate = true, DlID = RouteSecurity.encrypt(DealID.ToString()) });
            }
            catch (Exception er)
            {
                string s = "";
            }
            return View("EditTemplatePoints", LPOints);
        }
        #endregion
        #region "Empty Form"
        private void EmptyTempSession()
        {
            if (Session["QList"] != null) Session["QList"] = null;
            if (Session["QList2"] != null) Session["QList2"] = null;
        }
        private void EmptyFields()
        {
            Session["QList"] = null;
            LPOints = null;
        }
        #endregion
        #region "Loi Template Manipulation"
        [AuthorizeUser("/Home/", Roles.Admin, Roles.User)]
        public ActionResult TemplatePoints(string TempID, TStatus TracMsg = TStatus.Initial, bool EditTemplate = false)
        {
            EmptyTempSession();
            if (TempID != null && TempID != string.Empty) TempID = RouteSecurity.decrypt(TempID);
            if (TempID == null || TempID == string.Empty)
            {
                LPOints.UserID = AppUser.UserID;
                LPOints.CreatedDate = DateTime.Now;
                LPOints.ModifiedDate = DateTime.Now;
                return View(LPOints);
            }
            else
            {
                var lOITemplate = db.MLOITemplates.Find(Convert.ToInt32(TempID));
                //MLOITemplate lOITemplate = db.MLOITemplates.Find(Convert.ToInt32(TempID)) ;
                LPOints.UserID = lOITemplate.UserID;
                LPOints.CreatedDate = lOITemplate.CreatedDate;
                LPOints.ModifiedDate = lOITemplate.ModifiedDate;
                LPOints.Name = lOITemplate.Name;
                LPOints.Description = lOITemplate.Description;
                LPOints.Status = lOITemplate.Status;
                LPOints.ID = Convert.ToInt32(lOITemplate.ID);
                Int32 TemplateID = Convert.ToInt32(TempID);
                List<MLOITemplatePoint> QuestionList = db.MLOITemplatePoints.Where(m => m.TemplateID == TemplateID).ToList();


                List<LOITemplatePointEntity> mModel = new List<LOITemplatePointEntity>();

                foreach (var c in QuestionList)
                {
                    LOITemplatePointEntity temp = new LOITemplatePointEntity();
                    temp.TemplateID = c.TemplateID;
                    temp.ID = c.ID;
                    temp.DealPoint = c.DealPoint;
                    temp.CreatedDate = c.CreatedDate;
                    temp.ModifiedDate = c.ModifiedDate;
                    temp.Status = c.Status;
                    mModel.Add(temp);
                }

                // List<LOITemplatePointEntity> mModel = PrepareLOIPoints2(LPOints);

                LPOints.LOITempPoints = mModel;

                //LPOints.LOITempPoints = (IEnumerable<LOITemplatePointEntity>) QuestionList;
                Session["QList"] = null;
                Session["QList"] = mModel;

                //LOITemplatePoint QPoints = (LOITemplatePoint)db.LOITemplatePoints.Select(e => e.TemplateID == TemplateID);
                //LOITemplatePoint QPoints = db.LOITemplatePoint.Find(Convert.ToInt32(id));

                //   LPOints.LOITempPoints = QPoints;
                //Session["QList2"] = null;
                //Session["QList2"] = mModel;
                if (TracMsg == TStatus.Saved) ViewBag.SMsg = "LOITemplate saved successfully.";

                return View(LPOints);
            }
        }

        [HttpGet]
        public bool ValidateName(string TemplateName)
        {
            var Status = false;
            var lOITemplates = db.LOITemplates.Include(l => l.AspNetUser).Where(d => d.Name.Replace(" ", "") == TemplateName.Replace(" ", "")).ToList();
            if (lOITemplates != null && lOITemplates.Count() > 0)
            {
                Status = true;

            }
            return Status;
        }

        [HttpGet]
        public ActionResult CreateTemp(LOITemplateEntity mModel)
        {
            LPOints.LOITempPoints = PrepareLOIPoints(mModel);
            return PartialView("PointList", LPOints);
        }
        [HttpGet]
        public ActionResult UpdateTemp(string id, string Dpoint)
        {
            List<LOITemplatePointEntity> QPoints = (Session["QList"] != null) ? ((List<LOITemplatePointEntity>)Session["QList"]) : new List<LOITemplatePointEntity>();
            if (Dpoint != "" || Dpoint != string.Empty)
            {
                foreach (LOITemplatePointEntity point in QPoints.Where(i => i.ID == int.Parse(id)).ToList())
                {
                    point.DealPoint = Dpoint;
                }
                if (Session["QList"] != null) Session["QList"] = null;
                Session["QList"] = QPoints;
            }
            LPOints.LOITempPoints = QPoints;

            return PartialView("PointList", LPOints);
        }
        [HttpGet]
        public ActionResult DeleteTemp(int id)
        {
            List<LOITemplatePointEntity> QPoints = (Session["QList"] != null) ? ((List<LOITemplatePointEntity>)Session["QList"]) : new List<LOITemplatePointEntity>();
            QPoints = QPoints.Where(l => l.ID != id).ToList();
            if (Session["QList"] != null) Session["QList"] = null;
            Session["QList"] = QPoints;
            LPOints.LOITempPoints = QPoints;
            return PartialView("PointList", LPOints);
        }
        [ValidateAntiForgeryToken]
        //public ActionResult SaveTemp(LOITemplateEntity mModel)
        public ActionResult SaveTemp(LOITemplateEntity mModel, bool EditTemplate = false)
        {
            if (EditTemplate) return RedirectToAction("SaveEditTemp", mModel );

            List<LOITemplatePointEntity> QPoints = (Session["QList"] != null) ? ((List<LOITemplatePointEntity>)Session["QList"]) : new List<LOITemplatePointEntity>();
            List<LOITemplatePointEntity> QPoint2s = (Session["QList2"] != null) ? ((List<LOITemplatePointEntity>)Session["QList2"]) : new List<LOITemplatePointEntity>();

            mModel.LOITempPoints = QPoints;
            try
            {
                /// Saving/Inserting of LOITemplate
                var Template = new MLOITemplate();
                var TemplateNew = new MLOITemplate();
                var TemplatePoints = new MLOITemplatePoint();
                Template.CreatedDate = mModel.CreatedDate; Template.ModifiedDate = DateTime.Now; Template.Status = mModel.Status;
                Template.UserID = mModel.UserID; Template.Name = mModel.Name; Template.Description = mModel.Description; Template.ID = mModel.ID;


                //TemplateNew.CreatedDate = mModel.CreatedDate; TemplateNew.ModifiedDate = mModel.CreatedDate; TemplateNew.Status = mModel.Status;
                //TemplateNew.UserID = mModel.UserID; TemplateNew.Name = mModel.Name; TemplateNew.Description = mModel.Description; TemplateNew.ID = mModel.ID;
                foreach (var Tpoints in QPoints)
                {
                    if (mModel.ID != 0)
                    {
                        if (Tpoints.TemplateID != 0)
                        {
                            Template.MLOITemplatePoints.Add(new MLOITemplatePoint()
                            {
                                CreatedDate = Tpoints.CreatedDate,
                                ModifiedDate = Tpoints.CreatedDate,
                                Status = 0,
                                InitialValue = string.Empty,
                                DealPoint = Tpoints.DealPoint,
                                ID = Tpoints.ID,
                                TemplateID = Tpoints.TemplateID
                            });
                        }
                        else
                        {
                            TemplateNew.MLOITemplatePoints.Add(new MLOITemplatePoint()
                            {
                                CreatedDate = Tpoints.CreatedDate,
                                ModifiedDate = Tpoints.CreatedDate,
                                Status = 0,
                                InitialValue = string.Empty,
                                DealPoint = Tpoints.DealPoint,
                                TemplateID = mModel.ID
                            });
                        }
                    }
                    else
                    {
                        Template.MLOITemplatePoints.Add(new MLOITemplatePoint()
                        {
                            CreatedDate = Tpoints.CreatedDate,
                            ModifiedDate = Tpoints.CreatedDate,
                            Status = 0,
                            InitialValue = string.Empty,
                            DealPoint = Tpoints.DealPoint
                        });


                    }
                }
                if (mModel.ID == 0)
                {
                    db.MLOITemplates.Add(Template);
                }
                else
                {
                    db.MLOITemplates.Attach(Template);
                    db.Entry(Template).Property(m => m.Name).IsModified = true;
                    db.Entry(Template).Property(m => m.Description).IsModified = true;
                    string paramValue = "";
                    foreach (MLOITemplatePoint Lp in Template.MLOITemplatePoints)
                    {
                        db.MLOITemplatePoints.Attach(Lp);
                        db.Entry(Lp).Property(m => m.DealPoint).IsModified = true;
                        paramValue = paramValue + Lp.ID.ToString() + ",";

                    }
                    foreach (MLOITemplatePoint Lp in TemplateNew.MLOITemplatePoints)
                    {
                        db.MLOITemplatePoints.Add(Lp);
                    }
                    DbContext d; d = db;
                    paramValue = paramValue.Remove(paramValue.Length - 1);
                    string sql = "delete from [MLOITemplatePoints] where id not in (" + paramValue + ") and TemplateID=" + Template.ID.ToString();
                    d.Database.ExecuteSqlCommand(sql);
                }
                db.SaveChanges();
                var TepID = Template.ID.ToString();
                EmptyFields();
                TempData["Msg"] = "LOITemplate saved successfully.";
                Session["QList"] = null;
                //  return RedirectToAction("Create", "Deal");
                return RedirectToAction("TemplatePoints", new { TempID = RouteSecurity.encrypt(TepID), TracMsg = TStatus.Saved });
            }
            catch (Exception er)
            {
                string s = "";
            }
            return View("TemplatePoints", LPOints);
        }

        private List<LOITemplatePointEntity> PrepareLOIPoints(LOITemplateEntity mModel)
        {
            int id = 0;
            List<LOITemplatePointEntity> QPoints = (Session["QList"] != null) ? ((List<LOITemplatePointEntity>)Session["QList"]) : new List<LOITemplatePointEntity>();
            try
            {
                id = (QPoints.Count > 0) ? (QPoints.Max(IdMax => IdMax.ID) + 1) : 1;
                if (QPoints.Where(check => check.DealPoint == mModel.Question.Trim()).ToList().Count < 1)
                {
                    QPoints.Add(new LOITemplatePointEntity()
                    {
                        ID = id,
                        CreatedDate = mModel.CreatedDate,
                        ModifiedDate = mModel.ModifiedDate,
                        TemplateID = 0,
                        InitialValue = string.Empty,
                        Status = 0,
                        DealPoint = mModel.Question.Trim()
                    });
                    if (Session["QList"] != null) Session["QList"] = null;
                    Session["QList"] = QPoints;
                }
                else
                {
                    //ViewBag.ErrorMessage = "Question is already added in list.";
                }
            }
            catch { }
            return QPoints;
        }
        #endregion
        #region "Back To Deal"
        public ActionResult BackToDeal(string EditTemplate = "0", string DlID="0")
        {
            DlID=RouteSecurity.decrypt(DlID);
            if (EditTemplate == "True") return RedirectToAction("DealInitialTerm", "Deal", new { DealID = RouteSecurity.encrypt(DlID) });
            else if (EditTemplate == "0" || EditTemplate=="") return RedirectToAction("Create", "Deal");
            else return View();
        }
        #endregion
        // POST: LOITemplates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        #region "Others"
        // GET: LOITemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOITemplate lOITemplate = db.LOITemplates.Find(id);
            if (lOITemplate == null)
            {
                return HttpNotFound();
            }
            return View(lOITemplate);
        }
        // GET: LOITemplates/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            var lOITemplates = new LOITemplate(); // db.LOITemplates.Include(l => l.AspNetUser);
            return View(lOITemplates);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,UserID,ModifiedDate,Status,CreatedDate")] LOITemplate lOITemplate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOITemplate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", lOITemplate.UserID);
            return View(lOITemplate);
        }

        // GET: LOITemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            MLOITemplate lOITemplate = db.MLOITemplates.Find(id);

            List<MLOITemplatePoint> lop = new List<MLOITemplatePoint>();
            foreach (MLOITemplatePoint Lp in lOITemplate.MLOITemplatePoints)
            {
                lop.Add(Lp);
            }
            foreach (MLOITemplatePoint Lp in lop)
            {
                db.MLOITemplatePoints.Remove(Lp);
            }

            // db.LOITemplatePoints.Remove(lop);


            db.MLOITemplates.Remove(lOITemplate);
            db.SaveChanges();
            TempData["Save"] = "LOITemplate deleted successfully.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,UserID,ModifiedDate,Status,CreatedDate")] LOITemplate lOITemplate)
        {
            if (ModelState.IsValid)
            {
                db.LOITemplates.Add(lOITemplate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", lOITemplate.UserID);
            return View(lOITemplate);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOITemplate lOITemplate = db.LOITemplates.Find(id);
            if (lOITemplate == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", lOITemplate.UserID);
            return View(lOITemplate);
        }
        // POST: LOITemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LOITemplate lOITemplate = db.LOITemplates.Find(id);

            List<LOITemplatePoint> lop = new List<LOITemplatePoint>();
            foreach (LOITemplatePoint Lp in lOITemplate.LOITemplatePoints)
            {
                lop.Add(Lp);
            }
            foreach (LOITemplatePoint Lp in lop)
            {
                db.LOITemplatePoints.Remove(Lp);
            }

            // db.LOITemplatePoints.Remove(lop);


            db.LOITemplates.Remove(lOITemplate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
