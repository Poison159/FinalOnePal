using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using finalOnePal.Models;
using System.ComponentModel.DataAnnotations;

namespace finalOnePal.Models.Interfaces
{
    interface Iplayer
    {
       int Id { get; set; }
       int teamId { get; set; }
       Team team { get; set; }
       string name { get; set; }
       int age { get; set; }
       int goals { get; set; }
       int assists { get; set; }
       int cleanSheets { get; set; }
       string position { get; set; }
       int gamesPlayed { get; set; }
       string imgPath { get; set; }
       HttpPostedFileBase imageUpload { get; set; }
    }
}
