using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class SubcategoryItemViewModel
    {
        [Display(Name = "Subcategory Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Category")]
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public int ProductCount { get; set; }
    }

    public class SubcategoryViewModel : PageViewModel
    {
        public List<SubcategoryItemViewModel> Subcategories { get; set; }
        [Display(Name = "Search")]
        [StringLength(maximumLength: 64)]
        public string SearchName { get; set; }
    }

    public class SubcategoryInterfereViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectItemViewModel<int>> Categories { get; set; }
    }
}
