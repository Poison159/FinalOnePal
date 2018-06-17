using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using finalOnePal.Models;
using System.IO;

namespace finalOnePal.Controllers
{
    public class PlayersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Players
        //Make sure edit can change pic
        //Check error maximum length ecxeeded for uploading more pics 
        public ActionResult Index(string searchName, string position)
        {
            var positionsList = new List<string>();
            var pointsquery = from gmq in db.Players
                              orderby gmq.position
                              select gmq.position;
            positionsList.AddRange(pointsquery.Distinct());
            ViewBag.position = new SelectList(positionsList);
            var players = from cr in db.Players select cr;
            if (!string.IsNullOrEmpty(searchName))
            {
                players = players.Where(x => x.name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(position))
            {
                players = players.Where(x => x.position.
                ToString().Equals(position));
            }
            db.SaveChanges();
            players = players.Include(p => p.team
            ).OrderByDescending(x => x.goals);

            return View(players.ToList());
        }

        // GET: Players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            ViewBag.teamId = new SelectList(db.Teams, "id", "name");
            Player player = new Player();
            var positions = Helper.ReturnPositions();
            ViewBag.position = new SelectList(positions);
            return View(player);
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,teamId,name,age,goals,assists,cleanSheets,position,gamesPlayed,imgPath,imageUpload")] Player player)
        {
            if (ModelState.IsValid)
            {
                if (player.imageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(player.imageUpload.FileName);
                    string extention = Path.GetExtension(player.imageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    player.imgPath = "~/Content/imgs/" + fileName;
                    player.imageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/imgs/"), fileName));
                }
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.teamId = new SelectList(db.Teams, "id", "name", player.teamId);
            return View(player);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.teamId = new SelectList(db.Teams, "id", "name", player.teamId);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,teamId,name,age,goals,assists,cleanSheets,position,gamesPlayed,imgPath,imageUpload")] Player player)
        {
            if (ModelState.IsValid)
            {
                if (player.imageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(player.imageUpload.FileName);
                    string extention = Path.GetExtension(player.imageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    player.imgPath = "~/Content/imgs/" + fileName;
                    player.imageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/imgs/"), fileName));
                }
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.teamId = new SelectList(db.Teams, "id", "name", player.teamId);
            return View(player);
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
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
