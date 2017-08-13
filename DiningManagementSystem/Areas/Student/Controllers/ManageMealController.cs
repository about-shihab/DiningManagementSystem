using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace DiningManagementSystem.Areas.Student.Controllers
{
    [Authorize(Roles = "U")]
    public class ManageMealController : Controller
    {
        private MealInfoManager mealInfoManager;
        private StudentManager studentManager;

        public ManageMealController()
        {
            mealInfoManager = new MealInfoManager();
            studentManager = new StudentManager();
        }
        //
        // GET: /Student/ManageMeal/
        public ActionResult Index()
        {
            ViewBag.StudentId = new SelectList(studentManager.GetAllStudents().Where(x => x.IsApproved == "A"), "StudentId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(MealInfo mealInfo)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    if (mealInfo.LunchOff == false && mealInfo.DinnerOff == false && mealInfo.LunchGuestMeal == null && mealInfo.DinnerGuestMeal == null)
                    {
                        TempData["Msg"] = "Opps!! Your  request is unsuccessful. Please give atleast one value, Either off your meal or add guest meal or both";

                        return RedirectToAction("Index");
                    }
                    mealInfo.StudentId = studentManager.GetAllStudents().Where(x => x.Email == User.Identity.Name).FirstOrDefault().StudentId;

                    if (mealInfo.LunchGuestMeal==null)
                    {
                        mealInfo.LunchGuestMeal = 0;
                    }
                    if (mealInfo.DinnerGuestMeal == null)
                    {
                        mealInfo.DinnerGuestMeal = 0;
                    }


                    mealInfoManager.Insert(mealInfo);
                    TempData["Msg"] = "Your  request is successfully submitted";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Msg"] = "Opps!! Meal Off request was Unsuccessful";
                    return View("Index");
                }
            }
            catch (Exception e)
            {
                TempData["Msg"] = "Opps!! Meal Off request was Unsuccessful " + e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}