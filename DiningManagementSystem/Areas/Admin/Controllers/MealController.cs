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
    public class MealController : Controller
    {
        private MealManager mealManager;
        private MealInfoManager mealInfoManager;
        private StudentManager studentManager;

        public MealController()
        {
            mealInfoManager=new MealInfoManager();
            mealManager=new MealManager();
            studentManager=new StudentManager();
        }
        //
        // GET: /Admin/Meal/
        public ActionResult Index(string Page)
        {
            var todaysTotal = mealManager.GetAllMeals().Where(x => x.MealDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            var todaysTotalPaging = mealManager.GetAllMeals().Where(x => x.MealDate.ToShortDateString() == DateTime.Now.ToShortDateString()).ToList();
            ViewBag.Exist = todaysTotal.Count();
            int TodaysTotalLunchMeal = todaysTotal.Select(x=>x.Lunch).Sum();
            ViewBag.TodaysTotalLunchMeal = TodaysTotalLunchMeal;
            int TodaysTotalDinnerMeal = todaysTotal.Select(x => x.Dinner).Sum();
            ViewBag.TodaysTotalDinnerMeal = TodaysTotalDinnerMeal;
            
            int TodaysTotalDinnerGuestMeal = todaysTotal.Select(x => x.DinnerGuestMeal).Sum();
            ViewBag.TodaysTotalDinnerGuestMeal = TodaysTotalDinnerGuestMeal;
            int TodaysTotalLunchGuestMeal = todaysTotal.Select(x => x.LunchGuestMeal).Sum();
            ViewBag.TodaysTotalLunchGuestMeal = TodaysTotalLunchGuestMeal;

            int totalMeal = TodaysTotalLunchMeal + TodaysTotalDinnerMeal + TodaysTotalDinnerGuestMeal +
                            TodaysTotalLunchGuestMeal;

            ViewBag.totalMeal = totalMeal;

            var studentLunchsGuestMeal =
                mealManager.GetAllMeals().Select(x => new {x.StudentId, x.LunchGuestMeal}).GroupBy(x => x.StudentId).ToList();
            ViewBag.studentLunchsGuestMeal = studentLunchsGuestMeal;

            ViewBag.TotalPages = Math.Ceiling(todaysTotalPaging.Count() / 5.0);
            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;
            todaysTotal = todaysTotalPaging.Skip((page - 1) * 5).Take(5).ToList();


            ViewBag.EligibleLunchId =
                mealManager.GetAllMeals().Where(x => x.MealDate.ToShortDateString() == DateTime.Now.ToShortDateString() && x.Lunch != 0).Select(x => x.StudentId);

            ViewBag.EligibleDinnerId =
                mealManager.GetAllMeals().Where(x => x.MealDate.ToShortDateString() == DateTime.Now.ToShortDateString() && x.Dinner != 0).Select(x => x.StudentId);

            return View(todaysTotal);
        }


        [HttpPost]
        public ActionResult Create(Meal meal)
        {
            var mealInfo = mealInfoManager.GetAllMealInfos().ToList();
            var mealinfolist = mealInfo.Select(x => x.StudentId).Distinct().ToList();

            List<MealInfo> selectedMealInfos = new List<MealInfo>();
            foreach (var ml in mealinfolist)
            {
                selectedMealInfos.Add(mealInfoManager.GetAllMealInfos().Where(x => x.StudentId == ml).LastOrDefault());
            }


            var students = studentManager.GetAllStudents().Where(x => x.IsApproved == "A"&&x.Role=="U");
            var exist = mealManager.GetAllMeals().Where(x => x.MealDate.ToShortDateString() == DateTime.Now.ToShortDateString()).FirstOrDefault();


            //add all student to meal table
            if (exist == null)
            {
                foreach (var student in students)
                {
                    meal.MealDate = DateTime.Now;
                    meal.StudentId = student.StudentId;
                    meal.Lunch = 1;
                    meal.Dinner = 1;
                    meal.LunchGuestMeal = 0;
                    meal.DinnerGuestMeal = 0;
                    mealManager.Insert(meal);

                }

                //update meal table those on/off or add guest meal
                foreach (var selectedMealInfo in selectedMealInfos)
                {
                    var meals =
                        mealManager.GetAllMeals().Where(x => x.StudentId == selectedMealInfo.StudentId).LastOrDefault();
                    if (selectedMealInfo.StartDate.Date <= meals.MealDate && meals.MealDate.Date <= selectedMealInfo.EndDate.Date)
                    {
                        meals.LunchGuestMeal = Convert.ToInt32(selectedMealInfo.LunchGuestMeal);
                        meals.DinnerGuestMeal = Convert.ToInt32(selectedMealInfo.DinnerGuestMeal);
                        if (selectedMealInfo.LunchOff == true)
                        {
                            meals.Lunch = 0;
                        }
                        else
                        {
                            meals.Lunch = 1;
                        }
                        if (selectedMealInfo.DinnerOff == true)
                        {
                            meals.Dinner = 0;
                        }
                        else
                        {
                            meals.Dinner = 1;
                        }
                        mealManager.Update(meals);
                    }
                }
            }






            return RedirectToAction("Index");
        }
    }
}