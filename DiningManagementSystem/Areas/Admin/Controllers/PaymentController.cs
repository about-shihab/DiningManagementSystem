using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;

namespace DiningManagementSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class PaymentController : Controller
    {
        private StudentManager studentManager;
        private PaymentManager paymentManager;

        public PaymentController()
        {
            studentManager=new StudentManager();
            paymentManager=new PaymentManager();
        }
        //
        // GET: /Admin/Payment/
        public ActionResult Index()
        {
            ViewBag.StudentId = new SelectList(studentManager.GetAllStudents().Where(x=>x.IsApproved=="A"), "StudentId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Save(Payment payment)
        {
            int count =
                studentManager.GetAllStudents()
                    .Where(x => x.IsApproved == "A" && x.StudentId == payment.StudentId)
                    .Count();
            if (count==0)
            {
                TempData["Msg"] = "No Approved student found with "+payment.StudentId+" meal Id";
                return RedirectToAction("Index");
               
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {

                        payment.PaymentDate = DateTime.Now;
                        paymentManager.Insert(payment);
                        TempData["Msg"] = "Successfully Saved Payment";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Msg"] = "Failed to save payment";
                        return View("Index");
                    }
                }
                catch (Exception e)
                {
                    TempData["Msg"] = "Failed to save payment " + e.Message;
                    return RedirectToAction("Index");
                }
            }
            
        }
    }
}