using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class MealManager
    {
        private MealGateway mealGateway;

        public MealManager()
        {
            mealGateway=new MealGateway();
        }
        public IEnumerable<Meal> GetAllMeals()
        {
            return mealGateway.GetAllMeals();
        }

        public Meal GetById(int id)
        {
            return mealGateway.GetById(id);
        }

        public void Insert(Meal meal)
        {
            mealGateway.Insert(meal);

        }

        public void Delete(int id)
        {
            mealGateway.Delete(id);
        }

        public void Update(Meal meal)
        {
            mealGateway.Update(meal);
        }

        public int TotalMeal(int month)
        {
            var meals = mealGateway.GetAllMeals().Where(x=>x.MealDate.Month==month);
            int TotalMealNumber= meals.Select(x => x.Lunch).Sum() + meals.Select(x => x.Dinner).Sum() +
                   meals.Select(x => x.LunchGuestMeal).Sum() + meals.Select(x => x.DinnerGuestMeal).Sum();
            return TotalMealNumber;
        }
    }
}
