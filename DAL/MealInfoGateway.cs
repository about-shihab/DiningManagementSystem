using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class MealInfoGateway
    {
        private DMSDbEntities db;

        public MealInfoGateway()
        {
            db=new DMSDbEntities();
        }

        public IEnumerable<MealInfo> GetAllMealInfos()
        {
            return db.MealInfoes.ToList();
        }

        public MealInfo GetById(int id)
        {
            return db.MealInfoes.Find(id);
        }

        public void Insert(MealInfo mealInfo)
        {
            db.MealInfoes.Add(mealInfo);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            MealInfo mealInfo = db.MealInfoes.Find(id);
            db.MealInfoes.Remove(mealInfo);
            db.SaveChanges();
        }

        public void Update(MealInfo mealInfo)
        {
            db.Entry(mealInfo).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
