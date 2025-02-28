using CW_COMP_1471.Models;
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
        public IEnumerable<Pricing> GetPricings()
        {
            return _context.Pricings.ToList();
        }

        // Get Pricing by ID
        public Pricing? GetById(int id)
        {
            return _context.Pricings.Find(id);
        }

        // Add new pricing 
        public void Add(Pricing pricing)
        {
            _context.Pricings.Add(pricing);
            _context.SaveChanges();
        }

        // Update Pricing details
        public void Update(Pricing updatedPricing)
        {
            var pricing = _context.Pricings.Find(updatedPricing.Pricingid);
            if (pricing != null)
            {
                pricing.Band = updatedPricing.Band;
                pricing.Price = updatedPricing.Price;
                pricing.PlayId = updatedPricing.PlayId;

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
