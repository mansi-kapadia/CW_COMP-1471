using CW_COMP_1471.Models;

namespace CW_COMP_1471.Services
{
    public static class DiscountService
    {
        private static List<Discount> Discounts = new List<Discount>
        {
            new Discount
            {
                DiscountId = 1,
                Code = "SUMMER20",
                Description = "Get 20% off on summer packages!",
                DiscountAmount = 20.00m,
                ExpiryDate = new DateTime(2025, 06, 30),
                PackageId = 1
            },
            new Discount
            {
                DiscountId = 2,
                Code = "WELCOME10",
                Description = "Flat 10% discount for new customers.",
                DiscountAmount = 10.00m,
                ExpiryDate = new DateTime(2025, 12, 31),
                PackageId = 2
            },
            new Discount
            {
                DiscountId = 3,
                Code = "FESTIVE50",
                Description = "Limited-time festive season 50% discount!",
                DiscountAmount = 50.00m,
                ExpiryDate = new DateTime(2025, 11, 15),
                PackageId = 3
            }
        };

        public static List<Discount> GetAllDiscounts() => Discounts;

        public static Discount? GetById(int id) => Discounts.FirstOrDefault(d => d.DiscountId == id);

        public static void Add(Discount discount) => Discounts.Add(discount);

        public static void Update(Discount updatedDiscount)
        {
            var discount = GetById(updatedDiscount.DiscountId);
            if (discount != null)
            {
                discount.Code = updatedDiscount.Code;
                discount.Description = updatedDiscount.Description;
                discount.DiscountAmount = updatedDiscount.DiscountAmount;
                discount.ExpiryDate = updatedDiscount.ExpiryDate;
                discount.PackageId = updatedDiscount.PackageId;
            }
        }

        public static void DeleteDiscount(int id) => Discounts.RemoveAll(d => d.DiscountId == id);
    }
}
