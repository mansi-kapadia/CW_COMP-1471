using CW_COMP_1471.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public class PricingService : IPricingService
    {
        private readonly ApplicationDbContext _context;

        public PricingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Pricing
        public IEnumerable<PricingBand> GetPricings()
        {
            return _context.Pricings.Include(x => x.Play).ToList();
        }

        // Get Pricing by ID
        public PricingBand? GetById(int id)
        {
            return _context.Pricings.Find(id);
        }

        // Add new pricing 
        public void Add(PricingBand pricing)
        {
            _context.Pricings.Add(pricing);
            _context.SaveChanges();
        }

        // Update Pricing details
        public void Update(PricingBand updatedPricing)
        {
            var pricing = _context.Pricings.Find(updatedPricing.PricingId);
            if (pricing != null)
            {
                pricing.Band = updatedPricing.Band;
                pricing.Price = updatedPricing.Price;
                _context.SaveChanges();
            }
        }

        // Delete Pricing
        public void Delete(int id)
        {
            var pricing = _context.Pricings.Find(id);
            if (pricing != null)
            {
                _context.Pricings.Remove(pricing);
                _context.SaveChanges();
            }
        }
    }
}
