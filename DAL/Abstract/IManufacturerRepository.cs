using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IManufacturerRepository
    {
        Manufacturer AddManufacturer(Manufacturer Manufacturer);
        Manufacturer UpdateManufacturer(Manufacturer Manufacturer);
        IQueryable<Manufacturer> GetManufacturers();
        Manufacturer GetManufacturerById(int id);
    }
}
