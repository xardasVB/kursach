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
    public class Manufacturer : BaseModel<int>
    {
        [Required, StringLength(maximumLength: 256)]
        public string Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        virtual public Country Country { get; set; }
        
        virtual public ICollection<Product> Products { get; set; }
    }
}
