using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace finalOnePal.Models.Interfaces
{
    interface Iteam
    {

        int id { get; set; }
        string name { get; set; }
        string kasi { get; set; }
        int gamesPlayed { get; set; }
        int gamesWon { get; set; }
        int gamesDrawn { get; set; }
        int gamesLost { get; set; }
        int points { get; set; }
        int goalDiff { get; set; }
        int goalsFor { get; set; }
        int goalsAgainst { get; set; }
        List<Player> players { get; set; }
        string imgPath { get; set; }
        HttpPostedFileBase imageUpload { get; set; }
        void getPoints();
        void getGamesDrawn();
        void getGoalDiff();
    }
}