using CW_COMP_1471.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CW_COMP_1471.Services
{
    public interface IPackageService
    {
        List<Package> GetAllPackages();

        Package? GetById(int id);

        void Add(Package package);

        void Update(Package updatedPackage);

        void DeletePackage(int id);
       
    }
}
