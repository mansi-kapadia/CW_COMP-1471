using CW_COMP_1471.Models;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public interface IPricingService 
    {

        IEnumerable<PricingBand> GetPricings();

        PricingBand? GetById(int id);

        void Add(PricingBand pricing);

        void Update(PricingBand updatedPricing);

        void Delete(int id);
    }
}
