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
    public class Order : BaseModel<int>
    {
        [Required]
        public int Count { get; set; }
        public DateTime? Date { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        virtual public Product Product { get; set; }
        
        [ForeignKey("Basket")]
        public string BasketId { get; set; }
        virtual public Basket Basket { get; set; }

        [ForeignKey("ShoppingHistory")]
        public string ShoppingHistoryId { get; set; }
        virtual public ShoppingHistory ShoppingHistory { get; set; }
    }
}
