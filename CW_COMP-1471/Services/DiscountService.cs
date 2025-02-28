using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Discount> GetAllDiscounts()
        {
            return _context.Discounts.ToList();
        }

        public Discount? GetById(int id)
        {
            return _context.Discounts.FirstOrDefault(d => d.DiscountId == id);
        }

        public void Add(Discount discount)
        {
            _context.Discounts.Add(discount);
            _context.SaveChanges();
        }

        public void Update(Discount updatedDiscount)
        {
            var discount = _context.Discounts.Find(updatedDiscount.DiscountId);
            if (discount != null)
            {
                discount.Code = updatedDiscount.Code;
                discount.Description = updatedDiscount.Description;
                discount.DiscountAmount = updatedDiscount.DiscountAmount;
                discount.ExpiryDate = updatedDiscount.ExpiryDate;
                discount.PackageId = updatedDiscount.PackageId;

                _context.SaveChanges();
            }
        }

        public void DeleteDiscount(int id)
        {
            var discount = _context.Discounts.Find(id);
            if (discount != null)
            {
                _context.Discounts.Remove(discount);
                _context.SaveChanges();
            }
        }
    }
}
