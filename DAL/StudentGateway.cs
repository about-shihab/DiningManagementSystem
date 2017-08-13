using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class StudentGateway
    {
        private DMSDbEntities db;

        public StudentGateway()
        {
            db=new DMSDbEntities();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return db.Students.ToList();
        }

        public Student GetById(int id)
        {
            return db.Students.Find(id);
        }

        public void Insert(Student student)
        {
            db.Students.Add(student);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
        }

        public void Update(Student student)
        {
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
        }


    }
}
