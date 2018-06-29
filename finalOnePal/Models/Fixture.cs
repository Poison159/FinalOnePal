using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace finalOnePal.Models
{
    public class Fixture
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Home Team")]
        public string homeTeam { get; set; }
        [Required]
        [DisplayName("Away Team")]
        public string awayTeam { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }
        [Required]
        public string  pitch { get; set; }
    }
}