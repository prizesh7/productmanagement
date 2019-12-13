using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class TransactionsController : Controller
    {
        private prodEntities1 db = new prodEntities1();

        // GET: Transactions
        public ActionResult Index()
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
                int p = 0;
                foreach (var item in db.Transactions)
                {
                    if ((int)item.RetailerID != 9)
                    {
                        int id = (int)item.ProductID;
                        int q = (int)item.Quantity;
                        ProductList po = db.ProductLists.Find(id);
                        int cp = (int)po.CostPrice;
                        int sp = (int)item.Price;
                        p += (q * (sp - cp));
                    }
                }
                ViewBag.pro = p;
                var transactions = db.Transactions.Include(t => t.ProductList).Include(t => t.Retailer).Where(t => t.RetailerID != 9);
                return View(transactions.ToList());
            }
            else
            {
                return View("Error");
            }
        }
        
        public ActionResult Profit()
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
            
              int p = 0;
                foreach (var item in db.Transactions)
                {
                    if ((int)item.RetailerID != 9)
                    {
                    int id = (int)item.ProductID;
                    int q = (int)item.Quantity;
                    ProductList po = db.ProductLists.Find(id);
                    int cp = (int)po.CostPrice;
                    int sp = (int)item.Price;
                    p += (q * (sp - cp));
                }
                 }
                ViewBag.pro = p;
                var transactions = db.Transactions.Include(t => t.ProductList).Include(t => t.Retailer).Where(t => t.RetailerID != 9);
                return View(transactions.ToList());
            } 
            else
            {
                return RedirectToAction("Error");
            }
          
        }
        public ActionResult History()
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
                var transactions = db.Transactions.Include(t => t.ProductList).Include(t => t.Retailer).Where(t => t.RetailerID == 9);
                return View(transactions.ToList());
            }
            else if(TempData.ContainsKey("isLoggedIn"))
            {
                int id = (int)  TempData.Peek("id");
                var transactions = db.Transactions.Include(t => t.ProductList).Include(t => t.Retailer).Where(t => t.RetailerID == id);
                return View(transactions.ToList());
            }
            else
            {
                TempData["msg"] ="Please login first to see your transactions" ;
                return RedirectToAction("Login", "Retailers");
            }
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.ProductLists, "ProductId", "Name");
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerId", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RetailerID,ProductID,Quantity,Price,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                
            }

            ViewBag.ProductID = new SelectList(db.ProductLists, "ProductId", "Name", transaction.ProductID);
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerId", "Name", transaction.RetailerID);
            return View(transaction);
        }
        public ActionResult Creat([Bind(Include = "RetailerID,ProductID,Quantity,Price,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                if (transaction.RetailerID == 9)
                {
                    TempData["msg"] = "You have been added more product successfully...";
                    return RedirectToAction("Index", "ProductLists");
                }
                else
                {
                    TempData["msg"] = "You have successfully bought...purchase again!!!";
                    return RedirectToAction("Main", "ProductLists");
                }
            }

            ViewBag.ProductID = new SelectList(db.ProductLists, "ProductId", "Name", transaction.ProductID);
            ViewBag.RetailerID = new SelectList(db.Retailers, "RetailerId", "Name", transaction.RetailerID);
            return RedirectToAction("Error");
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection form)
        {
            int id = Convert.ToInt32(form["id"].ToString());
            var transactions = db.Transactions.Where(t => t.RetailerID == id).ToList();
            foreach(var trans in transactions)
            {
                db.Transactions.Remove(trans);
            }

            db.SaveChanges();
            TempData["id"] = id;
            return RedirectToAction("DeleteConfirmed","Retailers");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Error(int? id)
        {
            return View();
        }

    }
}
