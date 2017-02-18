using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Search.Entities;
using Search.Web.DataContexts;


namespace Search.Web.Controllers
{
    public class SalesLineItemsController : Controller
    {
        private SalesLineItemDb db = new SalesLineItemDb();

        // GET: SalesLineItems
        public async Task<ActionResult> Index()
        {
            return View(await db.SalesOrderItems.ToListAsync());
        }

        // GET: SalesLineItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = await db.SalesOrderItems.FindAsync(id);
            if (salesLineItem == null)
            {
                return HttpNotFound();
            }
            return View(salesLineItem);
        }

        // GET: SalesLineItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesLineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ItemName,Price,Qty,OrderId,CompanyId")] SalesLineItem salesLineItem)
        {
            if (ModelState.IsValid)
            {
                db.SalesOrderItems.Add(salesLineItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(salesLineItem);
        }

        // GET: SalesLineItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = await db.SalesOrderItems.FindAsync(id);
            if (salesLineItem == null)
            {
                return HttpNotFound();
            }
            return View(salesLineItem);
        }

        // POST: SalesLineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ItemName,Price,Qty,OrderId,CompanyId")] SalesLineItem salesLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesLineItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(salesLineItem);
        }

        // GET: SalesLineItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = await db.SalesOrderItems.FindAsync(id);
            if (salesLineItem == null)
            {
                return HttpNotFound();
            }
            return View(salesLineItem);
        }

        // POST: SalesLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SalesLineItem salesLineItem = await db.SalesOrderItems.FindAsync(id);
            db.SalesOrderItems.Remove(salesLineItem);
            await db.SaveChangesAsync();
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
    }
}
