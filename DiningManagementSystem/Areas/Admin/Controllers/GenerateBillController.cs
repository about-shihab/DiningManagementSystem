using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace DiningManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class GenerateBillController : Controller
    {
        private MealManager mealManager;
        private ExpenseManager expenseManager;
        private StudentManager studentManager;
        private PaymentManager paymentManager;
        

        public GenerateBillController()
        {
            mealManager = new MealManager();
            expenseManager = new ExpenseManager();
            studentManager = new StudentManager();
            paymentManager = new PaymentManager();
           
        }
        //
        // GET: /Admin/GenerateBill/
        public ActionResult Index()
        {
            ViewBag.Month = TempData["Month"];
            return View();
        }

        [HttpPost]
        public ActionResult Create(int? mealId, int month)
        {
            TempData["MealId"] = mealId;
            TempData["Month"] = month;
            decimal mealRate = 0;
            if (mealManager.TotalMeal(month) != 0)
             {
               mealRate = expenseManager.TotalExpense(month) / mealManager.TotalMeal(month);
              }
            TempData["MealRate"] = System.Math.Round(mealRate, 3);
            if (mealId == null)
            {
                TempData["Msg"] = "Please enter Meal Id frist ";
                return RedirectToAction("Index");
            }

            int count = studentManager.GetAllStudents().Where(x => x.StudentId == mealId && x.IsApproved == "A").Count();
                        if (count==0)
                        {
                            TempData["Count"] = null;
                            TempData["Msg"] = "Entered Meal Id is not found ";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["Count"] = count;
                        }

                       
            
                        var student = studentManager.GetAllStudents().Where(x => x.StudentId == mealId&&x.IsApproved=="A").FirstOrDefault();
                        int studentId =student.StudentId;


                        TempData["studentId"] = studentId;
                        TempData["Name"] = student.Name;
            
                        int studentTotalMeal = studentManager.StudentTotalMeal(studentId,month);

                        TempData["studentTotalMeal"] = studentTotalMeal;

                        decimal studentPayment = paymentManager.TotalPayment(studentId, month);
                        TempData["studentPayment"] = System.Math.Round(studentPayment, 3);
            
                        decimal studentExpense = mealRate * studentTotalMeal;
                        TempData["studentExpense"] = System.Math.Round(studentExpense, 3);
            
            
                        if (studentPayment < studentExpense)
                        {
                            TempData["haveToPay"] = System.Math.Round(studentExpense - studentPayment, 3);
                        }
                        if (studentPayment > studentExpense)
                        {
                            TempData["Receive"] = System.Math.Round(studentPayment - studentExpense, 3);
                        }

            return RedirectToAction("Index");
        }
    }
}