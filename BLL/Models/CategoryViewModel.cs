using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CategoryItemViewModel
    {
        [Display(Name = "Category Id")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }

    public class CategoryViewModel : PageViewModel
    {
        public List<CategoryItemViewModel> Categories { get; set; }
        [Display(Name = "Search")]
        [StringLength(maximumLength: 64)]
        public string SearchName { get; set; }
    }

    public class CategoryInterfereViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
