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
    public class ProductListsController : Controller
    {
        private prodEntities1 db = new prodEntities1();

        // GET: ProductLists
        public ActionResult Index()
        {
            if ((string)(TempData.Peek("role")) == "admin")
                return View(db.ProductLists.ToList());
            else
                return RedirectToAction("Error");
        }

        // GET: ProductLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        // GET: ProductLists/Create
        public ActionResult Create()
        {
            if ((string)(TempData.Peek("role")) == "admin")

            { return View(); }
            else
            {
                return RedirectToAction("Error");
            }

        }
        public ActionResult Main()
        {
            return View(db.ProductLists.ToList());
        }
        // POST: ProductLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,SellPrice,Quantity,Type,Description,CostPrice")] ProductList productList)
        {
            if (ModelState.IsValid)
            {
                db.ProductLists.Add(productList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["msg"] = "You have been created new product successfully...";
            return View(productList);
        }

        // GET: ProductLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProductList productList = db.ProductLists.Find(id);
                if (productList == null)
                {
                    return HttpNotFound();
                }
                return View(productList);
            }
            else { return RedirectToAction("Error"); }
        }

        // POST: ProductLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,SellPrice,Quantity,Type,Description,CostPrice")] ProductList productList)
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(productList).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "You have been edited product successfully...";
    return RedirectToAction("Index");
                }
                 return View(productList);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: ProductLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProductList productList = db.ProductLists.Find(id);
                if (productList == null)
                {
                    return HttpNotFound();
                }
                return View(productList);
            }
            else { return RedirectToAction("Error"); }
        }

        // POST: ProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductList productList = db.ProductLists.Find(id);
            db.ProductLists.Remove(productList);
            db.SaveChanges();
            TempData["msg"] = "You have been deleted new product successfully...";

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

        public ActionResult Error()
        {
            return View();
        }
        public ActionResult Retailers()
        {
            return RedirectToAction("Index", "Retailers");
        }
        public ActionResult Transactions()
        {
            return RedirectToAction("Index", "Transactions");
        }
        public ActionResult History()
        {
            return RedirectToAction("History", "Transactions");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Main(FormCollection form)
        {
            int quan = Convert.ToInt32(form["YourQuantity"].ToString());
            int id = Convert.ToInt32(form["id"].ToString());
            int i = (int)TempData.Peek("id");
            ProductList product = db.ProductLists.Where(p => p.ProductId.Equals(id)).ToList().FirstOrDefault();
            if (product.Quantity >= quan)
            {
                int quant = 0;
                if (product.Quantity != null)
                {
                    quant = (int)product.Quantity;
                }
                else
                {
                    quant = 0;
                }

                int num = quant - quan;
                product.Quantity = num;
                db.SaveChanges();
                TempData["prod"] = product;
                DateTime dt = DateTime.Now;
                return RedirectToAction("Creat", "Transactions", new { RetailerID = i, ProductID = product.ProductId, Quantity = quan, Price = product.SellPrice, Date = dt });
            }
            else
            {
                return View();
            }
        }

        // GET: ProductLists/Edit/5
        public ActionResult Add(int? id)
        {
            if ((string)(TempData.Peek("role")) == "admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ProductList productList = db.ProductLists.Find(id);
                if (productList == null)
                {
                    return HttpNotFound();
                }
                return View(productList);
            }
            else { return RedirectToAction("Error"); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            int quan = Convert.ToInt32(form["YourQuantity"].ToString());
            int id = Convert.ToInt32(form["id"].ToString());
            int i = (int)TempData.Peek("id");
            ProductList product = db.ProductLists.Where(p => p.ProductId.Equals(id)).ToList().FirstOrDefault();
            if (quan > 0)
            {
                int quant = 0;
                if (product.Quantity != null)
                {
                    quant = (int)product.Quantity;
                }
                else
                {
                    quant = 0;
                }

                int num = quant + quan;
                product.Quantity = num;
                db.SaveChanges();
                TempData["prod"] = product;
                DateTime dt = DateTime.Now;
                return RedirectToAction("Creat", "Transactions", new { RetailerID = i, ProductID = product.ProductId, Quantity = quan, Price = product.SellPrice, Date = dt});
            }
            else
            {
                return View();
            }
        }

    }
}

