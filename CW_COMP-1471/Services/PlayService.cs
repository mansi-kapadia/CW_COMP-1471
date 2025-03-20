using CW_COMP_1471.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public class PlayService : IPlayService
    {
        private readonly ApplicationDbContext _context;

        public PlayService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Play> GetAllPlays()
        {
            var plays = _context.Plays.ToList();

            foreach (var play in plays)
            {
                play.PricingBands = _context.Pricings.Where(x => x.PlayId == play.PlayId);
            }

            return plays;
        }

        public Play? GetById(int id)
        {
            return _context.Plays.Find(id);
        }

        public void Add(Play play)
        {
            if (string.IsNullOrEmpty(play.ImageUrl))
                play.ImageUrl = "https://cdn3.iconfinder.com/data/icons/online-states/150/Photos-1024.png";
            play.Dateandtime = play.Dateandtime.ToUniversalTime();
            _context.Plays.Add(play);
            _context.SaveChanges();
        }

        public void Update(Play play)
        {
            var existingPlay = _context.Plays.Find(play.PlayId);
            if (existingPlay != null)
            {
                existingPlay.Title = play.Title;
                existingPlay.Genre = play.Genre;
                existingPlay.Description = play.Description;
                existingPlay.Dateandtime = play.Dateandtime.ToUniversalTime(); ;
                existingPlay.PlayType = play.PlayType;
                existingPlay.ImageUrl = play.ImageUrl;
                _context.SaveChanges();
            }
        }

        public void DeletePlay(int id)
        {
            var play = _context.Plays.Find(id);
            if (play != null)
            {

                List<Ticket> tickets = _context.Tickets.Where(x => x.PlayId == id).ToList();
                foreach (Ticket ticket in tickets)
                {
                    _context.Tickets.Remove(ticket);
                }

                List<PricingBand> bands = _context.Pricings.Where(x => x.PlayId == id).ToList();
                foreach (PricingBand band in bands)
                {
                    _context.Pricings.Remove(band);
                }

                _context.Plays.Remove(play);
                _context.SaveChanges();
            }
        }
    }
}
