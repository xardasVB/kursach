using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Subcategory : BaseModel<int>
    {
        [Required, StringLength(maximumLength: 256)]
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        virtual public Category Category { get; set; }

        virtual public ICollection<Product> Products { get; set; }
    }
}
