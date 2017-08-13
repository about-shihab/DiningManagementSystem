using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL;

namespace DiningManagementSystem.Areas.Security.Controllers
{
    public class ChangeStudentPasswordController : Controller
    {
        private StudentManager studentManager;

        public ChangeStudentPasswordController()
        {
            studentManager=new StudentManager();
        }
        //
        // GET: /Security/ChangeStudentPassword/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(BOL.Student student)
        {
            var students = studentManager.GetAllStudents().Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            if (student.Password == null )
            {
                TempData["Msgp"] = "This Value is Required";
                return RedirectToAction("Index");

            }
            else if (student.NewPassword == null )
            {
                TempData["Msgn"] = "This Value is Required";
                return RedirectToAction("Index");
            }
            else if (student.NewConfrimPassword == null)
            {
                TempData["Msgc"] = "This Value is Required";
                return RedirectToAction("Index");
            }
            else
            {


                if (student.NewPassword == student.NewConfrimPassword)
                {

                    if (Crypto.VerifyHashedPassword(students.Password, student.Password))
                    {
                        students.Password = Crypto.HashPassword(student.NewPassword);
                        students.NewPassword = Crypto.HashPassword(student.NewPassword);
                        students.ConfrimPassword = Crypto.HashPassword(student.NewPassword);
                        studentManager.Update(students);
                        TempData["Msg"] = "Password Successfully Changed";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Msg"] = "Password Changed unsuccessfull";
                        return RedirectToAction("Index");
                    }
                }

                else
                {
                    TempData["Msg1"] = "New Password & new Confirm password didn't mached";
                    return RedirectToAction("Index");

                }
            }

        }
	}
}