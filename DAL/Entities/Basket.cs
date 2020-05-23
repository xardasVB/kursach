using DAL.Abstract;
using DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Basket //: BaseModel<int>
    {
        [Key, ForeignKey("UserProfileOf")]
        [StringLength(maximumLength: 128)]
        public string Id { get; set; }
        virtual public UserProfile UserProfileOf { get; set; }

        virtual public ICollection<Order> Orders { get; set; }
    }
}
