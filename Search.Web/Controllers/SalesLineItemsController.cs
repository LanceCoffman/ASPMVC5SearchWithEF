using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Search.Entities;
using Search.Web.DataContexts;
using System.Collections.Generic;
using Search.Web.Models;

namespace Search.Web.Controllers
{
    public class SalesLineItemsController : Controller
    {
        private SalesLineItemDb db = new SalesLineItemDb();
        private List<SalesLineItem> salesLineItems = new List<SalesLineItem>();

        // GET: SalesLineItems
        public ActionResult Index(string option, string search, int? page)
        {

            //if a user choose the radio button option as Subject  
            if (option == "ItemName")
            {
                salesLineItems = db.SalesOrderItems.Where(x => x.ItemName.StartsWith(search) || search == null).ToList();
            }
            else if (option == "Company")
            {
                salesLineItems = db.SalesOrderItems.Where(x => x.CompanyId.ToString() == search || search == null).ToList();
            }
            else if (option == "Price")
            {
                salesLineItems = db.SalesOrderItems.Where(x => x.Price.ToString() == search || search == null).ToList();
            }
            else
            {
                salesLineItems = db.SalesOrderItems.ToList();
            }

            var pager = new Pager(option, search, salesLineItems.Count(), page);
            var viewModel = new SalesLineItemsIndexViewModel
            {
                Items = salesLineItems.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
                Option=option,
                Search=search
            };

            return View(viewModel);
       }


        // GET: SalesLineItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = db.SalesOrderItems.Find(id);
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = db.SalesOrderItems.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,ItemName,Price,Qty,OrderId,CompanyId")] SalesLineItem salesLineItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesLineItem).State = EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(salesLineItem);
        }

        // GET: SalesLineItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesLineItem salesLineItem = db.SalesOrderItems.Find(id);
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
    }
}
