using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace DiningManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ExpenseController : Controller
    {
        private ExpenseManager expenseManager;
        private StudentManager studentManager;

        public ExpenseController()
        {
            expenseManager=new ExpenseManager();
            studentManager=new StudentManager();
        }
        //
        // GET: /Admin/Expense/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Expense expense)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    expense.StudentId = studentManager.GetAllStudents().Where(x=>x.Email==User.Identity.Name).FirstOrDefault().StudentId;
                    expense.ExpenseDate = DateTime.Now;
                    expenseManager.Insert(expense);
                    TempData["Msg1"] = "Expense amount saved successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Failed to save expense "+e.Message;
                return RedirectToAction("Index");
            }

            
          
            
        }
    }
}
