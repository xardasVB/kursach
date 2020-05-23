using DAL.Abstract;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CountryRepository : ICountryRepository
    {
        private readonly IEFContext _context;
        public CountryRepository(IEFContext context)
        {
            _context = context;
        }
        public Country AddCountry(Country country)
        {
            _context.Set<Country>().Add(country);
            _context.SaveChanges();
            return country;
        }

        public Country UpdateCountry(Country country)
        {
            var _country = _context.Set<Country>().SingleOrDefault(c => c.Id == country.Id);
            _country = country;
            _context.SaveChanges();
            return country;
        }

        public IQueryable<Country> GetCountries()
        {
            return _context.Set<Country>();
        }

        public Country GetCountryById(int id)
        {
            return GetCountries()
                .SingleOrDefault(c => c.Id == id);
        }
    }
}
