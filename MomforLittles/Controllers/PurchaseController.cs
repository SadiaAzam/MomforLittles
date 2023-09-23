using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.IO;
using MomforLittles.Models;


namespace MomforLittles.Controllers
{
    public class PurchaseController : Controller
    {
        Model1 db = new Model1();

        public ActionResult Cart()
        {

            return View();
        }
        public ActionResult ClosePurchase()
        {
            Session["Order"] = null;
            Session["myStock"] = null;
            return RedirectToAction("Purchase");
        }
        public ActionResult Purchase(int? ID)
        {
            ProductModel s = new ProductModel();
            //It contains 2 lists. One for Category and Second for Products
            s.Cat = db.Categories.ToList();
            if (ID == null)
            {
                s.Pro = db.Products.ToList();
            }
            else
            {
                s.Pro = db.Products.Where(p => p.CATEGORY_FID == ID).ToList();
            }

            return View(s);
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AddToCart(int id)
        {
            List<Product> list;
            if (Session["myStock"] == null)
            {
                list = new List<Product>();
            }
            else
            {
                list = (List<Product>)Session["myStock"];
            }
            list.Add(db.Products.Where(p => p.PRODUCT_ID == id).FirstOrDefault());
            list[list.Count - 1].PRO_QUANTITY = 1;
            Session["myStock"] = list;
            //Session is a Grand Global State (can store whole table, list etc.)
            //that can be accessed anywhere in the application
            //Cart, Payment, Bill, Order Booking

            return RedirectToAction("Cart");
        }
        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myStock"];
            list[RowNo].PRO_QUANTITY--;
            Session["myStock"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myStock"];
            list[RowNo].PRO_QUANTITY++;
            Session["myStock"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myStock"];
            list.RemoveAt(RowNo);
            Session["myStock"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PurchaseNow(Order o)
        {
            o.ORDER_DATE = System.DateTime.Now;
            o.ORDER_STATUS = "Paid";
            o.ORDER_TYPE = "Purchase";
            Session["Order"] = o;
            return RedirectToAction("OrderBooked");
        }
        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];

            //1. save in DB
            db.Orders.Add(o);
            db.SaveChanges();

            List<Product> p = (List<Product>)Session["myStock"];
            for (int i = 0; i < p.Count; i++)
            {
                OrderDetail od = new OrderDetail();
                int orderID = db.Orders.Max(x => x.ORDER_ID);
                od.ORDER_FID = orderID;
                od.PRODUCT_FID = p[i].PRODUCT_ID;
                od.QUANTITY = p[i].PRO_QUANTITY;
                od.PURCHASE_PRICE = p[i].PURCHASEPRICE;
                od.SALE_PRICE = p[i].SALEPRICE;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Price(int? SearchValue)
        {
            ProductModel s = new ProductModel();
            if (SearchValue == null)
            {
                s.Pro = db.Products.ToList();
                // TempData["InfoMessage"] = "Please type any product name";
            }
            else
            {
                return View("Product", db.Products.Where(p => p.SALEPRICE == SearchValue).ToList());

            }
            return View(s);
        }

        [HttpPost]
        public ActionResult Login(Admin a)
        {
            //Please Count the records from Database Admin when Table email and password matches with our posted email and password
            //If email and password match then result is 1
            //If not match then result is 0
            int result = db.Admins.Where(x => x.ADMIN_EMAIL == a.ADMIN_EMAIL && x.ADMIN_PASSWORD == a.ADMIN_PASSWORD).Count();
            if (result == 1)
            {
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }
    }
}
