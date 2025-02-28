using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public interface IDiscountService
    {

        List<Discount> GetAllDiscounts();

        Discount? GetById(int id);

        void Add(Discount discount);

        void Update(Discount updatedDiscount);

        void DeleteDiscount(int id);
    }
}
