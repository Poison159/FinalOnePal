using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalOnePal.Models
{
    public class Result
    {
        public int id { get; set; }
        [Required]
        public string fixture { get; set; }
        [Required]
        public string homeTeam { get; set; }
        [Required]
        public string awayTeam { get; set; }
        [Required]
        public int homeGoals { get; set; }
        [Required]
        public int awayaGoals { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        public List<Player> scorers { get; set; }

    }
}