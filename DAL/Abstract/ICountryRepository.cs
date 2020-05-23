using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICountryRepository
    {
        Country AddCountry(Country country);
        Country UpdateCountry(Country country);
        IQueryable<Country> GetCountries();
        Country GetCountryById(int id);
    }
}
