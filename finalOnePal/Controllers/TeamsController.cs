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

namespace OnepalV2.Controllers
{
    public class TeamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teams
        public ActionResult Index(string searchName, string kasi)
        {
            var kasiList = new List<string>();
            var kasiquery = from gmq in db.Teams
                              orderby gmq.kasi
                              select gmq.kasi;
            kasiList.AddRange(kasiquery.Distinct());
            
            
            ViewBag.kasi = new SelectList(kasiList);
            var teams = from cr in db.Teams select cr;
            if (!string.IsNullOrEmpty(searchName))
            {
                teams = teams.Where(x => x.name.Contains(searchName));
            }
            if (!string.IsNullOrEmpty(kasi))
            {
                teams = teams.Where(x => x.kasi.
                ToString().Equals(kasi));
            }
            foreach (var team in teams)
            {
                team.getPoints();
                team.goalDiff = Helper.getGoalDiff(team.goalsFor, team.goalsAgainst);
            }
            Helper.AssignGroups(db.Teams.ToList());
            db.SaveChanges();
            teams = teams.OrderByDescending(x => x.points);
            return View(teams);
        }
        public ActionResult ChartTeams()
        {
            var listOfPlayers = db.Players.ToList();
            var listOfTeams = db.Teams.ToList();
            Ratio obj = Helper.ReturnRatio(listOfPlayers);
            var ecxel = listOfTeams.Where(x => x.points >= 13);
            var good = listOfTeams.Where(x => x.points < 13 && x.points >= 9);
            var fair = listOfTeams.Where(x => x.points < 9 && x.points >= 5);
            var poor = listOfTeams.Where(x => x.points < 5);

            var fstats = Helper.getTeamsStats(ecxel);
            var estats = Helper.getTeamsStats(good);
            var gstats = Helper.getTeamsStats(fair);
            var sstats = Helper.getTeamsStats(poor);

            ViewBag.francewins = fstats.wins; ViewBag.francedraws = fstats.draws;
            ViewBag.francelosses = fstats.losses;
            ViewBag.englandwins = estats.wins; ViewBag.englanddraws = estats.draws;
            ViewBag.englandlosses = estats.losses;
            ViewBag.spainwins = sstats.wins; ViewBag.spaindraws = sstats.draws;
            ViewBag.spainlosses = sstats.losses;
            ViewBag.germanwins = gstats.wins; ViewBag.germandraws = gstats.draws;
            ViewBag.germanlosses = gstats.losses;


            return View(obj);
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Team team = db.Teams.Find(id);
            var players = db.Players.Where(x => x.teamId == id);
            team.players = players.ToList();
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        public ActionResult CurrentTeamPlayers(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var players = db.Players.Where(x => x.teamId == id);
            if (players == null)
            {
                return HttpNotFound();
            }
            ViewBag.teamName = db.Teams.Find(id).name;
            return View(players);
        }

        // GET: Teams/Create
        [Authorize]
        public ActionResult Create()
        {
            Team team = new Team();
            return View(team);
        }

        // POST: Teams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,kasi,imgPath,imageUpload,gamesPLayed,gamesWon,gamesDrawn,gamesLost,points,goalsFor,goalsAgainst,goalDiff")] Team team)
        {
            if (ModelState.IsValid)
            {
                team.getPoints(); team.getGoalDiff(); team.getGamesDrawn();

                if (team.imageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(team.imageUpload.FileName);
                    string extention = Path.GetExtension(team.imageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    team.imgPath = "~/Content/imgs/" + fileName;
                    team.imageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/imgs/"), fileName));
                }
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Teams/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,kasi,imgPath,gamesPLayed,gamesWon,gamesDrawn,gamesLost,points,imgPath,imageUpload")] Team team)
        {
            if (ModelState.IsValid)
            {
                if (team.imageUpload != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(team.imageUpload.FileName);
                    string extention = Path.GetExtension(team.imageUpload.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    team.imgPath = "~/Content/imgs/" + fileName;
                    team.imageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/imgs/"), fileName));
                }

                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Tournament()
        {
            var teams = db.Teams.ToList();
            ViewBag.Groups = Helper.getGroups();
            return View(teams);
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
