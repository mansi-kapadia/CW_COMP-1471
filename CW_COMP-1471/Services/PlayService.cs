using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class PlayService
    {
        private static List<Play> plays = new List<Play>
        {
            new Play { Playid = 1, Title = "Hamlet", Genre = "Tragedy", Description = "A Shakespearean classic", Dateandtime = DateTime.Now.AddDays(1), Playtype = "Drama", Createdby = 101, Createddate = DateTime.Now, PackageId = 1 },
            new Play { Playid = 2, Title = "Macbeth", Genre = "Tragedy", Description = "A story of ambition and fate", Dateandtime = DateTime.Now.AddDays(2), Playtype = "Drama", Createdby = 102, Createddate = DateTime.Now, PackageId = 2 },
            new Play { Playid = 3, Title = "Othello", Genre = "Tragedy", Description = "A tale of jealousy and betrayal", Dateandtime = DateTime.Now.AddDays(3), Playtype = "Drama", Createdby = 103, Createddate = DateTime.Now, PackageId = 3 }
        };

        public static IEnumerable<Play> GetAllPlays()
        {
            return plays;
        }

        public static void Add(Play play)
        {
            plays.Add(play);
        }

        public static Play GetById(int id)
        {
            return plays.FirstOrDefault(p => p.Playid == id);
        }

        public static void Update(Play play)
        {
            var existingPlay = GetById(play.Playid);
            if (existingPlay != null)
            {
                existingPlay.Title = play.Title;
                existingPlay.Genre = play.Genre;
                existingPlay.Description = play.Description;
                existingPlay.Dateandtime = play.Dateandtime;
                existingPlay.Playtype = play.Playtype;
                existingPlay.Createdby = play.Createdby;
                existingPlay.Createddate = play.Createddate;
                existingPlay.PackageId = play.PackageId;
            }
        }

        public static void DeletePlay(int id)
        {
            var play = GetById(id);
            if (play != null)
            {
                plays.Remove(play);
            }
        }
    }

}
