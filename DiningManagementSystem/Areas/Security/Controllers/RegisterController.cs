using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BLL;
using BOL;
namespace DiningManagementSystem.Areas.Security.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private StudentManager studentManager;
        private DepartmentManager departmentManager;

        public RegisterController()
        {
            studentManager = new StudentManager();
            departmentManager = new DepartmentManager();
        }
        //
        // GET: /Security/Register/
        public ActionResult Index()
        {
            ViewBag.DeptId = new SelectList(departmentManager.GetAllDepartments(), "DeptId", "Name");
            return View();
        }


        [HttpPost]
        public ActionResult Register(BOL.Student student)
        {
            if (student.Password==student.ConfrimPassword)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        student.Password = Crypto.HashPassword(student.Password);
                        student.Role = "U";
                        student.IsApproved = "P";
                        student.RegistrationDate = DateTime.Now;
                        studentManager.Insert(student);
                        TempData["Msg2"] = "Your have been successfully registered";
                        ViewBag.DeptId = new SelectList(departmentManager.GetAllDepartments(), "DeptId", "Name");

                        TempData["StudentId"] = studentManager.GetAllStudents().LastOrDefault().StudentId;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.DeptId = new SelectList(departmentManager.GetAllDepartments(), "DeptId", "Name");
                        TempData["Msg"] = "Registration Not Successfull";
                        return View("Index");
                    }
                }
                catch (Exception e)
                {
                    TempData["Msg"] = "Registraion unsuccessfull " + e.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Msg1"] = "Password & Confirm Password don't match ";
                return RedirectToAction("Index");
            }
            
        }
        public JsonResult IsEmailAvailble(string email)
        {
            var studentEmail = studentManager.GetAllStudents().FirstOrDefault(x => x.Email == email);

            if (studentEmail != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);

            }
        }
    }
}