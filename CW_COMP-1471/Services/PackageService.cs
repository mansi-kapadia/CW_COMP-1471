using CW_COMP_1471.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public class PackageService : IPackageService
    {
        private readonly ApplicationDbContext _context;

        public PackageService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Package> GetAllPackages()
        {
            return _context.Packages.ToList();
        }

        public Package? GetById(int id)
        {
            return _context.Packages.FirstOrDefault(p => p.PackageId == id);
        }

        public void Add(Package package)
        {
            _context.Packages.Add(package);
            _context.SaveChanges();
        }

        public void Update(Package updatedPackage)
        {
            var package = _context.Packages.Find(updatedPackage.PackageId);
            if (package != null)
            {
                package.Name = updatedPackage.Name;
                package.Description = updatedPackage.Description;
                //_context.Entry(package).State = EntityState.Modified; // Optional if using DbContext tracking
                _context.SaveChanges();
            }
        }

        public void DeletePackage(int id)
        {
            var package = _context.Packages.Find(id);
            if (package != null)
            {
                _context.Packages.Remove(package);
                _context.SaveChanges();
            }
        }
    }
}
