using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL;

namespace DAL
{
    public class PaymentGateway
    {
        private DMSDbEntities db;

        public PaymentGateway()
        {
            db=new DMSDbEntities();
        }


        public IEnumerable<Payment> GetAllPayments()
        {
            return db.Payments.ToList();
        }

        public Payment GetById(int id)
        {
            return db.Payments.Find(id);
        }

        public void Insert(Payment payment)
        {
            db.Payments.Add(payment);
            db.SaveChanges();

        }

        public void Delete(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
        }

        public void Update(Payment payment)
        {
            db.Entry(payment).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
