using finalOnePal.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "kasi")]
        public string kasi { get; set; }
        [Required]
        [Display(Name = "GP")]
        public int gamesPlayed { get; set; }
        [Required]
        [Display(Name = "GW")]
        public int gamesWon { get; set; }
        [Display(Name = "GD")]
        public int gamesDrawn { get; set; }
        [Required]
        [Display(Name = "GL")]
        public int gamesLost { get; set; }
        [Display(Name = "P")]
        public int points { get; set; }
        [Display(Name = "GD")]
        public int goalDiff { get; set; }
        [Required]
        [Display(Name = "GF")]
        public int goalsFor { get; set; }
        [Required]
        [Display(Name = "GA")]
        public int goalsAgainst { get; set; }
        [Display(Name = "players")]
        public List<Player> players { get; set; }
        [Display(Name = "image")]
        public string imgPath { get; set; }
        [DisplayName("Group")]
        public string group { get; set; }
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