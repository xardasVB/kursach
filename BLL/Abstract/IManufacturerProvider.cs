using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IManufacturerProvider
    {
        ManufacturerViewModel GetManufacturersByPage(int page, int items, string searchName);
        IEnumerable<ManufacturerItemViewModel> GetManufacturers();
        ManufacturerItemViewModel GetManufacturerById(int id);
        StatusViewModel DeleteManufacturer(int id);
        StatusViewModel CreateManufacturer(ManufacturerInterfereViewModel Manufacturer);
        StatusViewModel EditManufacturer(ManufacturerInterfereViewModel Manufacturer, int id);
    }
}
