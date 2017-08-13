using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class ExpenseManager
    {
        private ExpenseGateway expenseGateway;

        public ExpenseManager()
        {
            expenseGateway=new ExpenseGateway();
        }

        public IEnumerable<Expense> GetAllExpenses()
        {
            return expenseGateway.GetAllExpenses();
        }

        public Expense GetById(int id)
        {
            return expenseGateway.GetById(id);
        }

        public void Insert(Expense expense)
        {
            expenseGateway.Insert(expense);
        }

        public void Delete(int id)
        {
            expenseGateway.Delete(id);
        }

        public void Update(Expense expense)
        {
            expenseGateway.Update(expense);
        }

        public decimal TotalExpense(int month)
        {
            var expenses = GetAllExpenses().Where(x => x.ExpenseDate.Month == month);
            decimal totalExpense = expenses.Select(x => x.Amount).Sum();
            return totalExpense;
        }

    }
}
