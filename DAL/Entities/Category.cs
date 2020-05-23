using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Category : BaseModel<int>
    {
        [Required, StringLength(maximumLength: 256)]
        public string Name { get; set; }

        virtual public ICollection<Subcategory> Subcategories { get; set; }
    }
}
