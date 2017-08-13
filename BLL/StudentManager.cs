using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class StudentManager
    {
        private MealManager mealManager;
        private StudentGateway studentGateway;
        public StudentManager()
        {
            studentGateway = new StudentGateway();
            mealManager=new MealManager();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return studentGateway.GetAllStudents();
        }
        public Student GetById(int id)
        {
            return studentGateway.GetById(id);
        }

        public void Insert(Student student)
        {
            studentGateway.Insert(student);

        }

        public void Delete(int id)
        {
            studentGateway.Delete(id);
        }

        public void Update(Student student)
        {
            studentGateway.Update(student);
        }

        public int StudentTotalMeal(int studentId,int month )
        {
            int lunch =
                mealManager.GetAllMeals().Where(x => x.StudentId == studentId && x.MealDate.Month ==month).Select(x => x.Lunch).Sum();

            int dinner=
                mealManager.GetAllMeals().Where(x => x.StudentId == studentId && x.MealDate.Month == month).Select(x => x.Dinner).Sum();

            int lunchGuestMeal=
                mealManager.GetAllMeals().Where(x => x.StudentId == studentId && x.MealDate.Month == month).Select(x => x.LunchGuestMeal).Sum();

            int dinnerGuestMeal=
                mealManager.GetAllMeals().Where(x => x.StudentId == studentId && x.MealDate.Month == month).Select(x => x.DinnerGuestMeal).Sum();

            return lunch + dinner + lunchGuestMeal + dinnerGuestMeal;


        }
    }
}
