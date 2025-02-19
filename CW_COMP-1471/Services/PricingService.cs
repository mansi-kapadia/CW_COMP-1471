using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class PricingService
    {
        private static List<Pricing> Pricings = new List<Pricing>
            {
                new Pricing { Pricingid = 1, Band = "A" , Price = 100 , PlayId = 1  },
                new Pricing { Pricingid = 2, Band = "B" , Price = 200 , PlayId = 2  },
                new Pricing { Pricingid = 3, Band = "C" , Price = 300 , PlayId = 3  },
            };

        public static List<Pricing> GetPricings()
        {
            return Pricings;
        }

        public static Pricing GetById(int id)
        {
            return Pricings.FirstOrDefault(u => u.Pricingid == id);
        }

        // Add a new user
        public static void Add(Pricing Pricing)
        {
            Pricing.Pricingid = Pricings.Any() ? Pricings.Max(u => u.Pricingid) + 1 : 1;
            Pricings.Add(Pricing);
        }

        // Update an existing Pricing
        public static void Update(Pricing updatedPricing)
        {
            var Pricing = Pricings.FirstOrDefault(u => u.Pricingid == updatedPricing.Pricingid);
            if (Pricing != null)
            {
                Pricing.Band = updatedPricing.Band;
                Pricing.Pricingid = updatedPricing.Pricingid;
                Pricing.PlayId = updatedPricing.PlayId;
            }
        }

        public static void Delete(int id)
        {
            var band = Pricings.FirstOrDefault(u => u.Pricingid == id);
            if (band != null)
            {
                Pricings.Remove(band);
            }
        }
    }
}
