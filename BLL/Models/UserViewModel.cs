using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class UserItemViewModel
    {
        [Display(Name = "User Id")]
        public string Id { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public bool Banned { get; set; }
        public List<string> Roles { get; set; }
    }

    public class UserViewModel : PageViewModel
    {
        public List<UserItemViewModel> Users { get; set; }
        [Display(Name = "Search")]
        [StringLength(maximumLength: 64)]
        public string SearchName { get; set; }
    }

    public class UserInterfereViewModel
    {
        [Required(ErrorMessage = "Email name is required")]
        public string Email { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool Banned { get; set; }
        public string Image { get; set; }
        [Display(Name = "Role")]
        public List<SelectItemViewModel<string>> Roles { get; set; }
    }
}
