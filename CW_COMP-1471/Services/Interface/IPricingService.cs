using CW_COMP_1471.Models;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public interface IPricingService 
    {

        IEnumerable<Pricing> GetPricings();

        Pricing? GetById(int id);

        void Add(Pricing pricing);

        void Update(Pricing updatedPricing);

        void Delete(int id);
    }
}
