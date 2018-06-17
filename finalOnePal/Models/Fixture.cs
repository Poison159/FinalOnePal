using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace finalOnePal.Models
{
    public class Fixture
    {
        public int id { get; set; }
        [DisplayName("Home Team")]
        public string homeTeam { get; set; }
        [DisplayName("Away Team")]
        public string awayTeam { get; set; }
        public DateTime date { get; set; }
        public string  pitch { get; set; }
    }
}