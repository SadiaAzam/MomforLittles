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
using Antlr.Runtime;
using System.Web.ModelBinding;
using System.Threading.Tasks;
using System.Reflection;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
namespace MomforLittles.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        public ActionResult Price(int? pri)
        {

            return View(db.Products.Where(p => p.SALEPRICE == pri));

        }
        public ActionResult Index( string search, int? Page_No)
        {
            
                return View(db.Products.Where(x => x.PRODUCT_NAME.StartsWith(search) || search == null).ToList());


        }
  //Display Categories in navbar
        public ActionResult IndexCustomer(int? ID)
        {

            ProductModel s = new ProductModel();

            //It contains 2 lists. One for Category and Second for Products
            s.Cat = db.Categories.ToList();
            if (ID == null)
            {
                s.Pro = db.Products.ToList();
            }
            if (ID.HasValue)
            {
                s.Pro = db.Products.Where(p => p.CATEGORY_FID == ID).ToList();

            }
            return View(s);


        }
       

        public ActionResult IndexAdmin()
        {
            ProductModel s = new ProductModel();
            s.Cat = db.Categories.ToList();


           
            return View(s);
        }
      
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
        public ActionResult IndexBrand(int? ID)
        {
            ProductModel s = new ProductModel();
            s.Bnd = db.Brands.ToList();

            if (ID == null)
            {
                s.Pro = db.Products.ToList();
            }
            if (ID.HasValue)
            {
                s.Pro = db.Products.Where(p => p.BRAND_FID == ID).ToList();

            }
            return View(s);
        }
        public ActionResult ShopDetail(int ? id)
        {
            ProductModel s = new ProductModel();
            {
                //Product s = new Product();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                return View(product);
            }
        }
        public ActionResult CheckPro()
        {
            ProductModel s = new ProductModel();
            s.Cat = db.Categories.ToList();
           


            return View(s);
        }
        public ActionResult WishList()
        {
            ProductModel s = new ProductModel();
            s.Cat = db.Categories.ToList();


           
            return View(s);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        public ActionResult Cart()
        {
            ProductModel s = new ProductModel();
            s.Cat = db.Categories.ToList();


            return View(s);
        }
        public ActionResult Feedback()
        {
            ProductModel s = new ProductModel();
            s.Cat = db.Categories.ToList();


            ViewBag.Message = "Your contact page.";
            return View(s);

           

        }
        //Display Brands List
        public ActionResult Products(int? ID)
        {
            ProductModel s = new ProductModel();

            //It contains 2 lists. One for Category and Second for Products
            s.Bnd = db.Brands.ToList();
            s.Cat = db.Categories.ToList();

            if (ID == null)
            {
                s.Pro = db.Products.ToList();
            }
            if (ID.HasValue)
            {
                s.Pro = db.Products.Where(p => p.BRAND_FID == ID).ToList();

            }
          

            return View(s);
        }
        //Product View Page
        public ActionResult Product(int? ID)
        {
            ProductModel s = new ProductModel();
            s.Bnd = db.Brands.ToList();
            //It contains 2 lists. One for Category and Second for Products
            s.Cat = db.Categories.ToList();
           
            if (ID == null)
            {
                s.Pro = db.Products.ToList();
            }
            if (ID.HasValue)
            {
                s.Pro = db.Products.Where(p => p.CATEGORY_FID == ID).ToList();

            }

            return View(s);
        }
        public ActionResult Login()
        {
            ProductModel s = new ProductModel();
            s.Cat = db.Categories.ToList();


            return View(s);
           
        }
        public ActionResult AddToCart(int id)//receive product id
        {
            List<Product> list;
            if (Session["myCart"] == null)
            {
                list = new List<Product>();
            }
            else
            {
                list = (List<Product>)Session["myCart"];
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
            Session["myCart"] = list;
            //Session is a Grand Global State (can store whole table, list etc.)
            //that can be accessed anywhere in the application
            //Cart, Payment, Bill, Order Booking

            return RedirectToAction("Cart");
        }
        public ActionResult MinusFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list[RowNo].PRO_QUANTITY--;
            if (list[RowNo].PRO_QUANTITY == 0)
            {
                list.RemoveAt(RowNo);
            }
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PlusToCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            int P_ID = list[RowNo].PRODUCT_ID;
            int? available = db.OrderDetails.Where(x => x.PRODUCT_FID == P_ID).Sum(x => x.QUANTITY);
            if (available > list[RowNo].PRO_QUANTITY)
            {
                list[RowNo].PRO_QUANTITY++;
            }

            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult RemoveFromCart(int RowNo)
        {
            List<Product> list = (List<Product>)Session["myCart"];
            list.RemoveAt(RowNo);
            Session["myCart"] = list;
            return RedirectToAction("Cart");
        }
        public ActionResult PayNow(Order o)
        {
            o.ORDER_DATE = System.DateTime.Now;
            //o.Order_Status = "Paid";
            o.ORDER_TYPE = "Sale";
            o.STATUS = "Active";
            if (Session["Customer"] != null)
            {
                Customer c = (Customer)Session["Customer"];
                o.CUSTOMER_FID = c.CUSTOMER_ID;
            }

            Session["Order"] = o;
            if (o.ORDER_STATUS == "Cash on Delivery")
            {
                return RedirectToAction("OrderBooked");
            }
            else
            {
                return Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&business=momforlittles306@gmail.com&item_name=MomforLittle'sProducts&return=https://localhost:44350/Home/OrderBooked&amount=" + double.Parse(Session["totalAmount"].ToString()) /286);
            }

        }
        public ActionResult OrderBooked()
        {
            Order o = (Order)Session["Order"];
            //   code for sending mail
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("momforlittles306@gmail.com");
            mail.To.Add(o.ORDER_EMAIL);
            mail.Subject = "Order Confirmation";
            mail.Body = "Your Mom4Little's order has shipped <br/>Grow and Glow with us.<br/>We hope you are excited as we are.Thanks for your order!";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("momforlittles306@gmail.com", "qrvpjxqyxnjfgtih");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            ////code for sms
            //String api = "https://lifetimesms.com/json?api_token=11cbcf912cf60ab1bc9a4b20f7a840d6fd130b8340&api_secret=MomforLittle's&to=" + o.ORDER_CONTACT + "&from=MomforLittle's&message=Order Confirmation.";
            //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(api);
            //    var httpResponse = (HttpWebResponse) req.GetResponse();
            //3. save in DB
            db.Orders.Add(o);
            db.SaveChanges();

            List<Product> p = (List<Product>)Session["myCart"];
            for (int i = 0; i < p.Count; i++)
            {
                OrderDetail od = new OrderDetail();
                int orderID = db.Orders.Max(x => x.ORDER_ID);
                od.ORDER_FID = orderID;
                od.PRODUCT_FID = p[i].PRODUCT_ID;
                od.QUANTITY = p[i].PRO_QUANTITY * -1;
                od.PURCHASE_PRICE = p[i].PURCHASEPRICE;
                od.SALE_PRICE = p[i].SALEPRICE;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }
           
            return View();
        }
        Admin b;
        [HttpPost]
        public ActionResult Login(Admin a)
        {
            //Please Count the records from Database Admin when Table email and password matches with our posted email and password
            //If email and password match then result is 1
            //If not match then result is 0
            b = db.Admins.Where(x => x.ADMIN_EMAIL == a.ADMIN_EMAIL && x.ADMIN_PASSWORD == a.ADMIN_PASSWORD).FirstOrDefault();
            if (b != null)
            {
                Session["Admin"] = b;
                return RedirectToAction("IndexAdmin", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid Email or Password";
                return View();
            }

        }

        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult CloseOrder()
        {
            Session["Order"] = null;
            Session["myCart"] = null;
            return RedirectToAction("IndexCustomer");
        }
        public ActionResult FeedNow(Feedback f)
        {

            if (Session["Customer"] != null)
            {
                Customer c = (Customer)Session["Customer"];
                f.CUSTOMER_FID = c.CUSTOMER_ID;
                f.FEEDBACK_EMAIL = c.CUSTOMER_EMAIL;
                f.FEEDBACK_CONTACT = c.CUSTOMER_CONTACT;
                f.FEEDBACK_NAME = c.CUSTOMER_NAME;
            }
            db.Feedbacks.Add(f);
            db.SaveChanges();
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("momforlittles306@gmail.com");
            mail.To.Add(f.FEEDBACK_EMAIL);
            mail.Subject = "Thanks for Your valuable Feedback";
            mail.Body = " Thank you for a nice comment,</br> And we hope you'll continue to enjoy using our products. Thank you again...and we hope to see you back soon!";
            mail.IsBodyHtml = true;

            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("momforlittles306@gmail.com", "qrvpjxqyxnjfgtih");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            return RedirectToAction("Feedback", "Home");
        }
        public ActionResult FeedBackA()
        {
            var FeedBack = db.Feedbacks.ToList();
            return View(FeedBack.ToList());
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(Admin user)
        {
            Admin tbluser = db.Admins.Where(a => a.ADMIN_EMAIL == user.ADMIN_EMAIL).FirstOrDefault();
            if (tbluser != null)
            {

                // Send Password By Email
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("momforlittles306@gmail.com");
                mail.To.Add(tbluser.ADMIN_EMAIL);
                mail.Subject = "Forgot PAssword";
                mail.Body = "<b>Mom4Littles!</b><br /> Your Password is given below:<br />" + tbluser.ADMIN_PASSWORD;
                mail.IsBodyHtml = true;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("momforlittles306@gmail.com", "qrvpjxqyxnjfgtih");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                TempData["def"] = "Your Password sent you. Now you can Login.";
                return RedirectToAction("Login", "Home");

            }
            else
            {
                TempData["abc"] = "Not a Valid Account.";
                return RedirectToAction("ForgotPassword", "Home");

            }

        }

    }

}
