using BLL.Abstract;
using BLL.Models;
using DAL.Abstract;
using DAL.Concrete;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class CountryProvider : ICountryProvider
    {
        private readonly ISqlRepository _sqlRepository;

        public CountryProvider(ISqlRepository sqlRepository)
        {
            _sqlRepository = sqlRepository;
        }

        public StatusViewModel CreateCountry(CountryInterfereViewModel country)
        {
            IEnumerable<string> countryNames = GetCountries().Select(c => c.Name);

            if (countryNames.Contains(country.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Insert<Country>(new Country
            {
                Name = country.Name
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public StatusViewModel DeleteCountry(int id)
        {
            Country country = _sqlRepository.GetById<Country>(id);
            _sqlRepository.Delete<Country>(country);
            _sqlRepository.SaveChanges();

            return StatusViewModel.Success;
        }

        public StatusViewModel EditCountry(CountryInterfereViewModel country, int id)
        {
            List<string> countryNames = GetCountries()
                .Select(c => c.Name)
                .ToList();
            countryNames.Remove(_sqlRepository.GetById<Country>(id).Name);

            if (countryNames.Contains(country.Name))
                return StatusViewModel.Dublication;

            _sqlRepository.Update<Country>(new Country
            {
                Id = id,
                Name = country.Name
            });
            _sqlRepository.SaveChanges();
            return StatusViewModel.Success;
        }

        public CountryViewModel GetCountriesByPage(int page, int items, string searchName)
        {
            IEnumerable<Country> query = _sqlRepository.GetAll<Country>();
            CountryViewModel model = new CountryViewModel();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(c => c.Name.ToLower().Contains(searchName.ToLower()));
            }
            
            model.Countries = query
                .Skip((page - 1) * items)
                .Take(items)
                .Select(c => new CountryItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / items);
            model.CurrentPage = page;
            model.SearchName = searchName;
            model.Items = items;

            return model;
        }

        public IEnumerable<CountryItemViewModel> GetCountries()
        {
            return _sqlRepository
                .GetAll<Country>()
                .Select(c => new CountryItemViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                });
        }

        public CountryItemViewModel GetCountryById(int id)
        {
            Country country = _sqlRepository.GetAll<Country>().FirstOrDefault(c => c.Id == id);
            return new CountryItemViewModel
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
