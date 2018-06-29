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

        internal static List<string> getFixturesString(List<Fixture> list)
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
        public static void AssignGroups(List<Team> teams)
        {
            List<string> groups = getGroups();
            IEnumerable<int> teamIdList = getIdList(teams);
            Random rand = new Random();
            var result = teamIdList.OrderBy(x => rand.Next()).ToArray();
            int j = 0;
            foreach (var group in groups)
            {
                for (int i = 0; i < 2; i++)
                {
                    teams.First(x => x.id == result[j]).group = group;
                    j++;
                }
            }
        }
        public static List<string> getGroups()
        {
            return new List<string> { "A", "B", "C", "D", "E" };
        }

        private static IEnumerable<int> getIdList(List<Team> teams)
        {
            foreach (var team in teams)
            {
                yield return team.id;
            }
        }

        public static void assignStats(Result result, List<Team> teams)  
        {
            var teamInFixture = Helper.findTeams(result,teams);
            var homeTeam = teamInFixture.ElementAt(0);
            var awayTeam = teamInFixture.ElementAt(1);

            homeTeam.gamesPlayed++;
            homeTeam.goalsFor += result.homeGoals;
            homeTeam.goalsAgainst += result.awayaGoals;

            awayTeam.gamesPlayed++;
            awayTeam.goalsFor += result.awayaGoals;
            awayTeam.goalsAgainst += result.homeGoals;

            Helper.PopilateGamesWon(homeTeam, awayTeam,result);

        }

        private static void PopilateGamesWon(Team homeTeam, Team awayTeam, Result result)
        {
            if (result.homeGoals > result.awayaGoals)
            {
                homeTeam.gamesWon++;
                homeTeam.points += 3;
                awayTeam.gamesLost++;
            }
            else if (result.homeGoals < result.awayaGoals)
            {
                awayTeam.gamesWon++;
                awayTeam.points += 3;
                homeTeam.gamesLost++;
            }
            else {
                homeTeam.gamesDrawn++;
                awayTeam.gamesDrawn++;
                homeTeam.points++;
                awayTeam.points++;
            }
        }

        private static List<Team> findTeams(Result result, List<Team> teams)
        {
            List<Team> teamsInFixture = new List<Team>();
            var awayTeam = teams.First(x => x.name == result.awayTeam);
            var homeTeam = teams.First(x => x.name == result.homeTeam);

            teamsInFixture.Add(homeTeam);
            teamsInFixture.Add(awayTeam);
            return teamsInFixture;
        }
    }
}