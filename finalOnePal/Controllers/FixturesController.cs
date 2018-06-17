using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using finalOnePal.Models;

namespace finalOnePal.Controllers
{
    public class FixturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Fixtures
        public ActionResult Index()
        {
            return View(db.Fixtures.ToList());
        }

        // GET: Fixtures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // GET: Fixtures/Create
        public ActionResult Create()
        {
            Fixture fixture = new Fixture();
            var teams = Helper.getTeamNames(db.Teams.ToList());
            ViewBag.homeTeam = new SelectList(teams);
            ViewBag.awayTeam = new SelectList(teams);
            return View(fixture);
        }

        // POST: Fixtures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,homeTeam,awayTeam,date,pitch")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Fixtures.Add(fixture);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    var teams = Helper.getTeamNames(db.Teams.ToList());
                    ViewBag.homeTeam = new SelectList(teams);
                    ViewBag.awayTeam = new SelectList(teams);
                    return View(fixture);
                }

            }
            else
            {
                var teams = Helper.getTeamNames(db.Teams.ToList());
                ViewBag.homeTeam = new SelectList(teams);
                ViewBag.awayTeam = new SelectList(teams);
                return View(fixture);
            }
        }

        // GET: Fixtures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // POST: Fixtures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,homeTeam,awayTeam,date,pitch")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fixture);
        }

        // GET: Fixtures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fixture fixture = db.Fixtures.Find(id);
            if (fixture == null)
            {
                return HttpNotFound();
            }
            return View(fixture);
        }

        // POST: Fixtures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fixture fixture = db.Fixtures.Find(id);
            db.Fixtures.Remove(fixture);
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
