using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aloe.Models;

namespace Aloe.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
       // private DB_Aloe_Entities db = new DB_Aloe_Entities();
        private DB_Context.DB_Main_Entities db = new DB_Context.DB_Main_Entities();
        private Aloe.Helper.AloeUserInfo AppUser = new Helper.AloeUserInfo();
        // GET: /User/
        public ActionResult Index()
        {      
          
            return View(db.AspNetUsers.ToList());
        }
       

        // GET: /User/Details/5
        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUser aspnetuser = db.AspNetUsers.Find(id);
        //    if (aspnetuser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspnetuser);
        //}

        // GET: /User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Phone,Company,Street,City,State,PostalCode,RememberMe")] Aloe.DB_Context.AspNetUser aspnetuser)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUsers.Add(aspnetuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aspnetuser);
        }

        // GET: /User/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aloe.DB_Context.AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }

            ViewBag.Name = new SelectList(db.AspNetRoles.ToList(), "ID", "Name");
            ViewBag.RoleID = db.AspNetUserRoles.Where(m => m.UserId == aspnetuser.Id).First().RoleId;
            return View(aspnetuser);
        }

        // POST: /User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,PhoneNumber,UserName,Phone,Street,City,State,PostalCode")] Aloe.DB_Context.AspNetUser aspnetuser, FormCollection VForm)
        //{
        public ActionResult Edit(Aloe.DB_Context.AspNetUser aspnetuser, FormCollection VForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var RoleID = VForm["UserRoles"].ToString();
                    //aspnetuser.Email=VForm["Email"].ToString()
                    db.Entry(aspnetuser).State = System.Data.Entity.EntityState.Modified; //System.Data.EntityState.Modified;
                    var UserRole = db.AspNetUserRoles.Where(m => m.UserId == aspnetuser.Id).First();
                    db.AspNetUserRoles.Remove(UserRole);
                    db.AspNetUserRoles.Add(new DB_Context.AspNetUserRole() {  RoleId=RoleID, UserId=aspnetuser.Id});
                    db.SaveChanges();

                    ViewBag.Name = new SelectList(db.AspNetRoles.ToList(), "ID", "Name");
                    ViewBag.RoleID = db.AspNetUserRoles.Where(m => m.UserId == aspnetuser.Id).First().RoleId;
                    ViewBag.msg = "Personal Detail Updated Successfully";
                    return View(aspnetuser);
                }
                catch 
                {
                     
                }

                //return RedirectToAction("index", "home");
            }
            return View(aspnetuser);
        }

        // GET: /User/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aloe.DB_Context.AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            if (aspnetuser == null)
            {
                return HttpNotFound();
            }
            return View(aspnetuser);
        }

        // POST: /User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Aloe.DB_Context.AspNetUser aspnetuser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspnetuser);
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
    }
}
