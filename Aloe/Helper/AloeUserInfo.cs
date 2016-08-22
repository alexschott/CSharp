using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aloe.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Text;
namespace Aloe.Helper

{
    /// <summary>
    ///  Get LoggedIn User Information (i.e. ID,Name,Role,Email)
    /// </summary>
    public class AloeUserInfo:AloeUser
    {
        private void _GetUserInformation()
        {
            ApplicationUser  AppUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            int RoleID = 0;
            if (AppUser != null)
            {
                RoleID = int.Parse(AppUser.Roles.Select(m => m.RoleId).FirstOrDefault().ToString());
                UserID = AppUser.Id;
                UserName = AppUser.UserName;
                UserEmail = AppUser.Email;
                UserRoleID = RoleID;
                UserRoleName = (Roles)Enum.Parse(typeof(Roles), Enum.GetName(typeof(UserRole), RoleID));   //Enum.GetName(typeof(UserRole), RoleID)
            }
           
        }
        public AloeUserInfo()
        {
            _GetUserInformation();
        }
      
    }
    public static class CurrentUser 
    {
        public static string UserID { get; set; }
        static AloeUserInfo UInfo = new AloeUserInfo();
        static CurrentUser()
        {
            UserID = UInfo.UserID;
         
        }
    }
    public class AloeUser
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int UserRoleID { get; set; }
        public Roles UserRoleName { get; set; }

    }
    public static class RouteSecurity 
    {
        public static string encrypt(string ToEncrypt)
        {
            try
            {
                return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
            }
            catch { return string.Empty; }
        }
        public static string decrypt(string cypherString)
        {
            try
            {
                return Convert.ToString(Encoding.ASCII.GetString(Convert.FromBase64String(cypherString)));
            }
            catch { return string.Empty; }
        }

    }
}