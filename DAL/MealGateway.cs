using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class MealGateway
    {
        private DMSDbEntities db;
        public MealGateway()
        {
            db=new DMSDbEntities();
        }


        public IEnumerable<Meal> GetAllMeals()
        {
            return db.Meals.ToList();
        }

        public Meal GetById(int id)
        {
            return db.Meals.Find(id);
        }

        public void Insert(Meal meal)
        {
            db.Meals.Add(meal);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            Meal meal = db.Meals.Find(id);
            db.Meals.Remove(meal);
            db.SaveChanges();
        }

        public void Update(Meal meal)
        {
            db.Entry(meal).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
