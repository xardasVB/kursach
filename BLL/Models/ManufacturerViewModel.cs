using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class ManufacturerItemViewModel
    {
        [Display(Name = "Manufacturer Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        public int CountryId { get; set; }
    }

    public class ManufacturerViewModel : PageViewModel
    {
        public List<ManufacturerItemViewModel> Manufacturers { get; set; }
        [Display(Name = "Search")]
        [StringLength(maximumLength: 64)]
        public string SearchName { get; set; }
    }

    public class ManufacturerInterfereViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public List<SelectItemViewModel<int>> Countries { get; set; }
    }
}
