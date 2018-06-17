using finalOnePal.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace finalOnePal.Models
{
    public class Team : Iteam
    {
        public Team()
        {
            imgPath = "~/Content/user.png";
        }
        public int id { get; set; }
        [Required]
        [Display(Name = "name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "kasi")]
        public string kasi { get; set; }
        [Required]
        [Display(Name = "games played")]
        public int gamesPlayed { get; set; }
        [Required]
        [Display(Name = "games won")]
        public int gamesWon { get; set; }
        [Display(Name = "games drawn")]
        public int gamesDrawn { get; set; }
        [Required]
        [Display(Name = "games lost")]
        public int gamesLost { get; set; }
        [Display(Name = "points")]
        public int points { get; set; }
        [Display(Name = "goal diff")]
        public int goalDiff { get; set; }
        [Required]
        [Display(Name = "goals For")]
        public int goalsFor { get; set; }
        [Required]
        [Display(Name = "goals Against")]
        public int goalsAgainst { get; set; }
        [Display(Name = "players")]
        public List<Player> players { get; set; }
        [Display(Name = "image")]
        public string imgPath { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageUpload { get; set; }
        public void getPoints()
        {
            points = (gamesWon * 3) + gamesDrawn;
        }
        public void getGamesDrawn()
        {
            gamesDrawn = gamesPlayed - ( gamesWon + gamesLost);
        }

        public void getGoalDiff()
        {
            goalDiff = goalsFor - goalsAgainst;
        }
    }
}