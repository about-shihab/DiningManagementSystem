using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class ExpenseGateway
    {
        private DMSDbEntities db;

        public ExpenseGateway()
        {
            db=new DMSDbEntities();
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return db.Expenses.ToList();
        }

        public Expense GetById(int id)
        {
            return db.Expenses.Find(id);
        }

        public void Insert(Expense expense)
        {
            db.Expenses.Add(expense);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            Expense expense = db.Expenses.Find(id);
            db.Expenses.Remove(expense);
            db.SaveChanges();
        }

        public void Update(Expense expense)
        {
            db.Entry(expense).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
