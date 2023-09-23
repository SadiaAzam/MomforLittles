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
using MomforLittles.Models;

namespace MomforLittles.Controllers
{
    public class BrandsAdController : Controller
    {
        private Model1 db = new Model1();

        // GET: Brands
        public ActionResult Index()
        {
            return View(db.Brands.ToList());
        }
        public ActionResult IndexAd()
        {
            return View(db.Brands.ToList());
        }


        // GET: Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // GET: Brands/Create
        public ActionResult Create()
        {

           

            return View();
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                brand.PRO_PIC.SaveAs(Server.MapPath("~/ProductPicture/" + brand.PRO_PIC.FileName));
                brand.BRAND_LOGO = "~/ProductPicture/" + brand.PRO_PIC.FileName;
                db.Brands.Add(brand);
                db.SaveChanges();

                // code for sending mail
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("momforlittles306@gmail.com");
                mail.To.Add(brand.BRAND_EMAIL);
                mail.Subject = "You Are Register Now";
                mail.Body = " Mom4Little's has Registered you successfully<br/>Grow and Glow with us.<br/>We hope you are excited as we are.Thanks for your registration!";
                mail.IsBodyHtml = true;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("momforlittles306@gmail.com", "qrvpjxqyxnjfgtih");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return RedirectToAction("Index");
            }

            return View(brand);
        }

        // GET: Brands/Edit/5
        public ActionResult Edit(int? id)
        {

            Brand brand = db.Brands.Find(id);
            return View(brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand brand)
        {
            if (ModelState.IsValid)
            {
                if (brand.PRO_PIC != null)
                {
                    brand.PRO_PIC.SaveAs(Server.MapPath("~/ProductPicture/" + brand.PRO_PIC.FileName));
                    brand.BRAND_LOGO = "~/ProductPicture/" + brand.PRO_PIC.FileName;
                }
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand brand = db.Brands.Find(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }
       
        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
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

        [HttpPost]
        public ActionResult Login(Brand brand)
        {
            //Please Count the records from Database Admin when Table email and password matches with our posted email and password
            //If email and password match then result is 1
            //If not match then result is 0
            Brand b = db.Brands.Where(x => x.BRAND_EMAIL == brand.BRAND_EMAIL && x.BRAND_PASSWORD == brand.BRAND_PASSWORD).FirstOrDefault();
            if (b != null)
            {
                Session["Brand"] = b;
                return RedirectToAction("IndexBrand", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session["Brand"] = null;
            return RedirectToAction("IndexCustomer", "Home");
        }
    }
}
