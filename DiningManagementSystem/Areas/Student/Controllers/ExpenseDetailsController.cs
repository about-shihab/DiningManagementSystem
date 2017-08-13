using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;

namespace DiningManagementSystem.Areas.Student.Controllers
{
    [Authorize(Roles = "U")]
    public class ExpenseDetailsController : Controller
    {
        private MealManager mealManager;
        private ExpenseManager expenseManager;
        private StudentManager studentManager;
        private PaymentManager paymentManager;

        public ExpenseDetailsController()
        {
            mealManager=new MealManager();
            expenseManager=new ExpenseManager();
            studentManager=new StudentManager();
            paymentManager=new PaymentManager();
        }
        //
        // GET: /Student/ExpenseDetails/
        public ActionResult Index()
        {

            ViewBag.Month = TempData["Month"];
            return View();
        }

        public ActionResult Create(int month)
        {



            TempData["Month"] = month.ToString();
            


            var student = studentManager.GetAllStudents().Where(x => x.Email == User.Identity.Name);
            int studentId = student.FirstOrDefault().StudentId;
            TempData["studentId"] = studentId;


            int studentTotalMeal = studentManager.StudentTotalMeal(studentId, month);

            TempData["studentTotalMeal"] = studentTotalMeal;


            decimal studentPayment = paymentManager.TotalPayment(studentId, month);
            TempData["studentPayment"] = System.Math.Round(studentPayment, 3);

            if (studentTotalMeal == 0 && studentPayment == 0)
            {
                TempData["Msg"] = "Opps!! Sorry you haven't take  meal in this month";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Msg1"] = "1";
            }


            decimal mealRate = 0;
            if (mealManager.TotalMeal(month) != 0)
            {
                mealRate = expenseManager.TotalExpense(month) / mealManager.TotalMeal(month);
            }
            TempData["MealRate"] = System.Math.Round(mealRate, 3);

           

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