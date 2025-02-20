using CW_COMP_1471.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public static class PackageService
    {
        private static List<Package> Packages = new List<Package>
        {
            new Package
            {
                PackageId = 1,
                Name = "Premium Package",
                Description = "Includes VIP seating and backstage access.",
            },
            new Package
            {
                PackageId = 2,
                Name = "Standard Package",
                Description = "Standard seating with complimentary drinks.",
            },
            new Package
            {
                PackageId = 3,
                Name = "Basic Package",
                Description = "Affordable package with general seating.",
            }
        };

        public static List<Package> GetAllPackages() => Packages;

        public static Package? GetById(int id) => Packages.FirstOrDefault(p => p.PackageId == id);

        public static void Add(Package package)
        {
            package.PackageId = Packages.Max(p => p.PackageId) + 1;
            Packages.Add(package);
        }

        public static void Update(Package updatedPackage)
        {
            var package = GetById(updatedPackage.PackageId);
            if (package != null)
            {
                package.Name = updatedPackage.Name;
                package.Description = updatedPackage.Description;
                //package.PricingId = updatedPackage.PricingId;
            }
        }

        public static void DeletePackage(int id) => Packages.RemoveAll(p => p.PackageId == id);
    }
}
