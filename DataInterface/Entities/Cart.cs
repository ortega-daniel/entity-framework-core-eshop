using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public decimal Total { get; set; }

        public ICollection<CartDetail> CartDetails { get; set; }
    }
}
