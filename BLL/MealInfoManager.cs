using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class MealInfoManager
    {
        private MealInfoGateway mealInfoGateway;

        public MealInfoManager()
        {
            mealInfoGateway=new MealInfoGateway();
        }

        public IEnumerable<MealInfo> GetAllMealInfos()
        {
            return mealInfoGateway.GetAllMealInfos();
        }

        public MealInfo GetById(int id)
        {
            return mealInfoGateway.GetById(id);
        }

        public void Insert(MealInfo mealInfo)
        {
            mealInfoGateway.Insert(mealInfo);
        }

        public void Delete(int id)
        {
            mealInfoGateway.Delete(id);
        }

        public void Update(MealInfo mealInfo)
        {
            mealInfoGateway.Update(mealInfo);
        }
    }
}
