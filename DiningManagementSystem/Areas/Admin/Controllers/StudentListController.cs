using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace DiningManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "A,U")]
    public class StudentListController : Controller
    {
        private StudentManager studentManager;

        public StudentListController()
        {
            studentManager=new StudentManager();
        }
        //
        // GET: /Admin/StudentList/
        public ActionResult Index(string Status,string SortOrder,string SortBy, string Page)
        {
            ViewBag.Status = (Status == null ? "P" : Status);
            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            if (Status == null)
            {
                var students = studentManager.GetAllStudents().Where(x => x.IsApproved == "P"&&x.Role=="U").ToList();
                switch (SortOrder)
                {
                    case "Asc":
                        students = students.OrderBy(x => x.Name).ToList();
                        break;
                    case "Desc":
                        students = students.OrderByDescending(x => x.Name).ToList();
                        break;

                    default: break;
                }
                ViewBag.TotalPages = Math.Ceiling(students.Count() / 3.0);

                int page = int.Parse(Page == null ? "1" : Page);
                ViewBag.Page = page;
                students = students.Skip((page - 1) * 3).Take(3).ToList();

                return View(students);
            }
            else
            {
                var students = studentManager.GetAllStudents().Where(x => x.IsApproved == Status&&x.Role=="U").ToList();
                switch (SortOrder)
                {
                    case "Asc":
                        students = students.OrderBy(x => x.Name).ToList();
                        break;
                    case "Desc":
                        students = students.OrderByDescending(x => x.Name).ToList();
                        break;

                    default: break;
                }

                ViewBag.TotalPages = Math.Ceiling(students.Count() / 3.0);

                int page = int.Parse(Page == null ? "1" : Page);
                ViewBag.Page = page;
                students = students.Skip((page - 1) * 3).Take(3).ToList();
            
                return View(students);
            }

        }
        public ActionResult Approve(int id)
        {
            try
            {
                var students = studentManager.GetById(id);
                students.IsApproved = "A";
                students.ConfrimPassword = "123456";
                students.NewConfrimPassword = "";
                students.NewPassword = "";
                studentManager.Update(students);

                TempData["Msg"] = "Approved";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not Approved " + e.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Reject(int id)
        {
            try
            {
                var students = studentManager.GetById(id);
                students.IsApproved = "R";
                students.ConfrimPassword = "123456";
                students.NewConfrimPassword = "";
                students.NewPassword = "";
                studentManager.Update(students);

                TempData["Msg"] = "Rejected";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Not Rejected " + e.Message;
                return RedirectToAction("Index");
            }
        }
	}
}