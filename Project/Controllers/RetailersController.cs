using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Project.Controllers
{
    public class RetailersController : Controller
    {
        private prodEntities1 db = new prodEntities1();
        
        // GET: Retailers
        public ActionResult Index()
        {
            if ((string)(TempData.Peek("role")) == "admin")
                return View(db.Retailers.ToList());
            else
                return RedirectToAction("Error");
        }

        // GET: Retailers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = db.Retailers.Find(id);
            if (retailer == null)
            {
                return HttpNotFound();
            }
            return View(retailer);
        }

        // GET: Retailers/Create
        public ActionResult Register()
        {
            return View();
        }

        // POST: Retailers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "RetailerId,Name,Email,Contact,Address,Password,confirmPassword")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                db.Retailers.Add(retailer);
                db.SaveChanges();
                TempData["role"] = "user";
                TempData["isLoggedIn"] = true;
                TempData["id"] = retailer.RetailerId;
                TempData["name"] = retailer.Name;
                return RedirectToAction("Main","ProductLists");
            }

            return View(retailer);
        }

        // GET: Retailers/Edit/5
        public ActionResult Edit()
        {
            if (TempData.ContainsKey("isLoggedIn"))
            {
                int id = (int)TempData.Peek("id");
       
                    Retailer retailer = db.Retailers.Find(id);
                    if (retailer == null)
                    {
                        return HttpNotFound();
                    }
                    return View(retailer);               
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: Retailers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RetailerId,Name,Email,Contact,Address,Password,confirmPassword")] Retailer retailer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(retailer).State = EntityState.Modified;
                db.SaveChanges();
                if ((string) TempData.Peek("role") == "admin")
                {
                    return RedirectToAction("Index", "ProductLists");
                }
                else
                    return RedirectToAction("Main", "ProductLists");
            }
            return View(retailer);
        }

        public ActionResult Prof()
        {
            int? id = null;
            if(TempData.ContainsKey("isLoggedIn"))
            {
                id = (int) TempData.Peek("id");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = db.Retailers.Find(id);
            if (retailer == null)
            {
                return HttpNotFound();
            }
            return View(retailer);
        }

        // GET: Retailers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retailer retailer = db.Retailers.Find(id);
            if (retailer == null)
            {
                return HttpNotFound();
            }
            return View(retailer);
        }

        // POST: Retailers/Delete/5
        public ActionResult DeleteConfirmed()
        {
            int id = (int) TempData["id"];
            Retailer retailer = db.Retailers.Find(id);
            db.Retailers.Remove(retailer);
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
            ViewBag.message = TempData["msg"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection form)
        {
            string phn = form["contact"].ToString();
            string pass = form["password"].ToString();
            if(phn=="9999999999" && pass=="admin123")
            {
                TempData["role"] = "admin";
                TempData["name"] = "Admin";
                TempData["id"] = 9;
                TempData["isLoggedIn"] = true;
                return RedirectToAction("Index","ProductLists");

            }
            Retailer person = db.Retailers.Where(p => p.Contact.Equals(phn)).ToList().FirstOrDefault();
            if (person != null && person.Password == pass)
            {
                TempData["role"] = "user";
                TempData["isLoggedIn"] = true;
                TempData["id"] = person.RetailerId;
                TempData["name"] = person.Name;
                return RedirectToAction("Main", "ProductLists");
            }

            else
            {
                ViewBag.message = "Log-In Credentials not correct.Enter again!";
                return View();
            }
        }

        public ActionResult LogOff()
        {
           TempData.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Error(int? id)
        {
            return View();
        }
        
    }
}
