using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BLL;

namespace DiningManagementSystem.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        private StudentManager studentManager;

        public LoginController()
        {
            studentManager=new StudentManager();
        }
        //
        // GET: /Security/Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(BOL.Student student)
        {
            int count =
                studentManager.GetAllStudents().Where(x => x.Email == student.Email && x.IsApproved =="A").Count();
            if (count==0)
            {
                TempData["Msg"] = "Login failed";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    if (Membership.ValidateUser(student.Email, student.Password))
                    {

                        FormsAuthentication.SetAuthCookie(student.Email, false);
                        return RedirectToAction("Dashboard", "Home", new { Area = "Common" });
                    }
                    else
                    {
                        TempData["Msg"] = "Login failed";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {

                    TempData["Msg"] = "Login failed " + e.Message;
                    return RedirectToAction("Index");
                }
            }
            
            
        }
        public ActionResult SignOut()
        {
            
            FormsAuthentication.SignOut();
//            Session.RemoveAll();
//            SessionManager.ClearSession();
            return RedirectToAction("Index", "Home", new {Area = "Common"});
        }

       
    }
}