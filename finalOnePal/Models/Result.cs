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
        public int fixId { get; set; }
        public Fixture fix { get; set; }
        [Required]
        public string fixture { get; set; }
        [Required]
        public int homeGoals { get; set; }
        [Required]
        public int awayaGoals { get; set; }
    }
}