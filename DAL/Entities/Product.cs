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
    public class Product : BaseModel<int>
    {
        [Required, StringLength(maximumLength: 256)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal PriceCMV { get; set; }
        [Required]
        public int StockCount { get; set; }
        public string Code { get; set; }
        [StringLength(maximumLength: 256)]
        public string Image { get; set; }
        [StringLength(maximumLength: 1024)]
        public string Description { get; set; }

        [ForeignKey("Subcategory")]
        public int SubcategoryId { get; set; }
        virtual public Subcategory Subcategory { get; set; }
        
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }
        virtual public Manufacturer Manufacturer { get; set; }

        virtual public ICollection<Review> Reviews { get; set; }
        virtual public ICollection<Order> Orders { get; set; }
    }
}
