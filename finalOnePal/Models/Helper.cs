using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using finalOnePal.Models;

namespace finalOnePal.Models
{
    public class Helper
    {
        public int MAXGAMES { get; set; }
        public int MyProperty { get; set; }

        public static TeamStats getTeamsStats(IEnumerable<Team> teams)
        {
            var teamStats = new TeamStats();

            foreach (var team in teams)
            {
                teamStats.wins += team.gamesWon;
                teamStats.losses += team.gamesLost;
                teamStats.draws += team.gamesDrawn;
            }

            return teamStats;
        }
        public static int getGoalDiff(int gf, int ga)
        {
            return (gf - ga);
        }

        public static Ratio ReturnRatio(IEnumerable<Player> list)
        {
            var ecxel = list.Where(x => x.position == "ST");
            var good = list.Where(x => x.position == "LW" || x.position == "RW");
            var fair = list.Where(x => x.position == "MD" || x.position == "CAM");
            var poor = list.Where(x => x.position == "CAD" || x.position == "LB" || x.position == "RB");

            Ratio obj = new Ratio();
            obj.skikers = ecxel.Count();
            obj.wingers = good.Count();
            obj.midfilders = fair.Count();
            obj.defenders = poor.Count();

            return obj;
        }

        internal static List<string> getFixtures(List<Fixture> list)
        {
            List<string> fixtureList = new List<string>();
            if (list != null) {
                foreach (var item in list)
                {
                    string temp = "";
                    temp = item.homeTeam + "," + item.awayTeam + "," + item.date + "," + item.pitch;
                    fixtureList.Add(temp);
                }
            }
            return fixtureList;
        }

        public static List<string> ReturnPositions()
        {
            List<string> positions = new List<string>()
            {
                "ST","DF","LW","RW","CAD","MD","RB","LB","GK"
            };
            return (positions);
        }

        public static List<string> getTeamNames(List<Team> teams)
        {
            List<string> teamNames = new List<string>();
            foreach (var team in teams)
            {
                teamNames.Add(team.name);
            }
            return teamNames;
        }
    }
}