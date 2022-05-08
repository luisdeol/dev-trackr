using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevTrackR.API.Entities;

namespace DevTrackR.API.Persistence.Repository
{
    public interface IPackageRepository
    {
        List<Package> GetAll();
        Package GetByCode(string code);
        void Add(Package package);
        void Update(Package package);
    }
}