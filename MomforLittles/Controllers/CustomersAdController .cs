using MomforLittles.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace MomforLittles.Controllers
{
    public class CustomersAdController : Controller
    {
        private Model1 db = new Model1();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }
        public ActionResult CustAdmin()
        {
            return View(db.Customers.ToList());
        }
        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {


            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_ID,Customer_Name,Customer_Email,Customer_Address,Customer_Contact,Customer_Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                // code for sending mail
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("momforlittles306@gmail.com");
                mail.To.Add(customer.CUSTOMER_EMAIL);
                mail.Subject = "You Are Register Now";
                mail.Body = " Mom4Little's has Registered you successfully<br/>Grow and Glow with us.<br/>We hope you are excited as we are.Thanks for your registration!";
                mail.IsBodyHtml = true;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("momforlittles306@gmail.com", "qrvpjxqyxnjfgtih");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

                return RedirectToAction("Login");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_ID,Customer_Name,Customer_Email,Customer_Address,Customer_Contact,Customer_Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Login()
        {

           


            return View();
        }
        
        Customer b;
        [HttpPost]
        public ActionResult Login(Customer a)
        {
           

            //Please Count the records from Database Admin when Table email and password matches with our posted email and password
            //If email and password match then result is 1
            //If not match then result is 0
            b = db.Customers.Where(x => x.CUSTOMER_EMAIL == a.CUSTOMER_EMAIL && x.CUSTOMER_PASSWORD == a.CUSTOMER_PASSWORD).FirstOrDefault();
            if (b != null)
            {
                Session["Customer"] = b;
                return RedirectToAction("IndexCustomer", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }
           
            //It contains 2 lists. One for Category and Second for Products
           
          
        }
        public ActionResult Logout()
        {
            Session["Customer"] = null;
            return RedirectToAction("IndexCustomer", "Home");
        }

        public ActionResult CustomerHistory()
        {
            return View();
        }
        public ActionResult Invoice(int id)
        {
            var o = db.Orders.Where(x => x.ORDER_ID == id).ToList();
            return View(o);
        }
        public ActionResult OrderCancellation(int id)
        {
            Order o = db.Orders.Where(x => x.ORDER_ID == id).FirstOrDefault();
            o.STATUS = "Cancelled";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CustomerHistory");
        }
        public ActionResult CancelledOrder()
        {
            return View();
        }
        public ActionResult OrderActivate(int id)
        {
            Order o = db.Orders.Where(x => x.ORDER_ID == id).FirstOrDefault();
            o.STATUS = "Active";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("CustomerHistory");
            return RedirectToAction("CancelledOrder");
        }
        public ActionResult WishList()
        {
            return View();
        }
        public ActionResult AddtoWishList(int id)
        {
            List<Product> list;
            if (Session["myWishList"] == null)
            {
                list = new List<Product>();
            }
            else
            {
                list = (List<Product>)Session["myWishList"];
            }

            Boolean isProductExist = false;
            foreach (var item in list)
            {
                if (id == item.PRODUCT_ID)
                {
                    isProductExist = true;
                    item.PRO_QUANTITY++;
                }
            }

            if (isProductExist == false)
            {
                list.Add(db.Products.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
                list[list.Count - 1].PRO_QUANTITY = 1;
            }
            Session["myWishList"] = list;
            return RedirectToAction("WishList");
        }
        public ActionResult RemoveFromWishList(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myWishList"];
            list.RemoveAt(RowNo);
            Session["myWishList"] = list;
            return RedirectToAction("WishList");
        }

    }
}
