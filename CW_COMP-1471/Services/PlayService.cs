using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class PlayService
    {
        private static List<Play> plays = new List<Play>
        {
            new Play { Playid = 1, Title = "Hamlet", Genre = "Tragedy", Description = "A Shakespearean classic", Dateandtime = DateTime.Now.AddDays(1), Playtype = "Drama", Createdby = 101, Createddate = DateTime.Now, PackageId = 1 },
            new Play { Playid = 2, Title = "Macbeth", Genre = "Tragedy", Description = "A story of ambition and fate", Dateandtime = DateTime.Now.AddDays(2), Playtype = "Drama", Createdby = 102, Createddate = DateTime.Now, PackageId = 2 },
            new Play { Playid = 3, Title = "Othello", Genre = "Tragedy", Description = "A tale of jealousy and betrayal", Dateandtime = DateTime.Now.AddDays(3), Playtype = "Drama", Createdby = 103, Createddate = DateTime.Now, PackageId = 3 },
            new Play { Playid = 4, Title = "Romeo and Juliet", Genre = "Romance", Description = "A tragic love story", Dateandtime = DateTime.Now.AddDays(4), Playtype = "Drama", Createdby = 104, Createddate = DateTime.Now, PackageId = 4 },
            new Play { Playid = 5, Title = "Julius Caesar", Genre = "Historical", Description = "A tale of betrayal and power", Dateandtime = DateTime.Now.AddDays(5), Playtype = "Drama", Createdby = 105, Createddate = DateTime.Now, PackageId = 5 },
            new Play { Playid = 6, Title = "A Midsummer Night's Dream", Genre = "Comedy", Description = "A whimsical romantic comedy", Dateandtime = DateTime.Now.AddDays(6), Playtype = "Drama", Createdby = 106, Createddate = DateTime.Now, PackageId = 6 },
            new Play { Playid = 7, Title = "The Tempest", Genre = "Fantasy", Description = "A magical and adventurous story", Dateandtime = DateTime.Now.AddDays(7), Playtype = "Drama", Createdby = 107, Createddate = DateTime.Now, PackageId = 7 },
            new Play { Playid = 8, Title = "Death of a Salesman", Genre = "Drama", Description = "A critique of the American Dream", Dateandtime = DateTime.Now.AddDays(8), Playtype = "Theater", Createdby = 108, Createddate = DateTime.Now, PackageId = 8 },
            new Play { Playid = 9, Title = "The Crucible", Genre = "Historical Drama", Description = "A play based on the Salem witch trials", Dateandtime = DateTime.Now.AddDays(9), Playtype = "Theater", Createdby = 109, Createddate = DateTime.Now, PackageId = 9 },
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
