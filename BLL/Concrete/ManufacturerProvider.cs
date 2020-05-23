using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Abstract;
using DAL.Entities;

namespace BLL.Concrete
{
    public class ManufacturerProvider : IManufacturerProvider
    {
        private readonly ISqlRepository _sqlRepository;

        public ManufacturerProvider(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public StatusViewModel CreateManufacturer(ManufacturerInterfereViewModel Manufacturer)
        {
            IEnumerable<string> ManufacturerNames = GetManufacturers()
                .Where(m => m.CountryId == Manufacturer.CountryId)
                .Select(m => m.Name);

            if (ManufacturerNames.Contains(Manufacturer.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Insert<Manufacturer>(new Manufacturer
            {
                Name = Manufacturer.Name,
                CountryId = Manufacturer.CountryId
            });
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel DeleteManufacturer(int id)
        {
            Manufacturer manufacturer = _sqlRepository.GetById<Manufacturer>(id);
            _sqlRepository.Delete<Manufacturer>(manufacturer);
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel EditManufacturer(ManufacturerInterfereViewModel Manufacturer, int id)
        {
            List<string> ManufacturerNames = GetManufacturers()
                .Where(m => m.CountryId == Manufacturer.CountryId)
                .Select(m => m.Name)
                .ToList();
            ManufacturerNames.Remove(_sqlRepository.GetById<Manufacturer>(id).Name);

            if (ManufacturerNames.Contains(Manufacturer.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Update<Manufacturer>(new Manufacturer
            {
                Id = id,
                Name = Manufacturer.Name,
                CountryId = Manufacturer.CountryId
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public ManufacturerItemViewModel GetManufacturerById(int id)
        {
            Manufacturer manufacturer = _sqlRepository.GetAll<Manufacturer>().FirstOrDefault(m => m.Id == id);
            return new ManufacturerItemViewModel
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country = manufacturer.Country.Name,
                CountryId = manufacturer.CountryId
            };
        }

        public IEnumerable<ManufacturerItemViewModel> GetManufacturers()
        {
            return _sqlRepository
                .GetAll<Manufacturer>()
                .Select(m => new ManufacturerItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Country = m.Country.Name,
                    CountryId = m.CountryId
                });
        }

        public ManufacturerViewModel GetManufacturersByPage(int page, int items, string searchName)
        {
            IEnumerable<Manufacturer> query = _sqlRepository.GetAll<Manufacturer>();
            ManufacturerViewModel model = new ManufacturerViewModel();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(m => m.Name.ToLower().Contains(searchName.ToLower()));
            }

            model.Manufacturers = query
                .Skip((page - 1) * items)
                .Take(items)
                .Select(m => new ManufacturerItemViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Country = m.Country.Name,
                    CountryId = m.CountryId
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / items);
            model.CurrentPage = page;
            model.SearchName = searchName;
            model.Items = items;

            return model;
        }
    }
}
