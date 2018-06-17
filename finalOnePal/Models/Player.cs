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
    public class Player : Iplayer
    {
        public Player()
        {
            imgPath = "~/Content/user.png";
        }

        public int Id { get; set; }
        [DisplayName("Team")]
        public int teamId { get; set; }
        public virtual Team team { get; set; }
        [Required]
        [DisplayName("name")]
        public string name { get; set; }
        [Required]
        [DisplayName("age")]
        public int age { get; set; }
        [Required]
        [DisplayName("goals")]
        public int goals { get; set; }
        [Required]
        [DisplayName("assits")]
        public int assists { get; set; }
        [Required]
        [DisplayName("clean sheets")]
        public int cleanSheets { get; set; }
        [Required]
        [DisplayName("position")]
        public string position { get; set; }
        [Required]
        [DisplayName("games played")]
        public int gamesPlayed { get; set; }
        [Required]
        [DisplayName("path")]
        public string imgPath { get; set; }
        [NotMapped]
        public HttpPostedFileBase imageUpload { get; set; }
        
    }
}