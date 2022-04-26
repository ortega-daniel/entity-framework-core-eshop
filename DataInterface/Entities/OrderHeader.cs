using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Entities
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
