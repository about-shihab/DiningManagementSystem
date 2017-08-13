using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;
using DAL;

namespace BLL
{
    public class PaymentManager
    {
        private PaymentGateway paymentGateway;

        public PaymentManager()
        {
            paymentGateway=new PaymentGateway();
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return paymentGateway.GetAllPayments();

        }

        public Payment GetById(int id)
        {
            return paymentGateway.GetById(id);
        }

        public void Insert(Payment payment)
        {
            paymentGateway.Insert(payment);
        }

        public void Delete(int id)
        {
            paymentGateway.Delete(id);
        }

        public void Update(Payment payment)
        {
            paymentGateway.Update(payment);
        }

        public decimal TotalPayment(int studentId, int month)
        {
            decimal totalPayment = GetAllPayments().Where(x => x.StudentId == studentId &&x.PaymentDate.Month==month).Select(x => x.Amount).Sum();
            return totalPayment;
        }

    }
}
