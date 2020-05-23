using BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICountryProvider
    {
        CountryViewModel GetCountriesByPage(int page, int items, string searchName);
        IEnumerable<CountryItemViewModel> GetCountries();
        CountryItemViewModel GetCountryById(int id);
        StatusViewModel DeleteCountry(int id);
        StatusViewModel CreateCountry(CountryInterfereViewModel country);
        StatusViewModel EditCountry(CountryInterfereViewModel country, int id);
    }
}
