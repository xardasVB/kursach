using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CountryItemViewModel
    {
        [Display(Name = "Country Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
    }

    public class CountryViewModel : PageViewModel
    {
        public List<CountryItemViewModel> Countries { get; set; }
        [Display(Name = "Search")]
        [StringLength(maximumLength: 64)]
        public string SearchName { get; set; }
    }

    public class CountryInterfereViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
