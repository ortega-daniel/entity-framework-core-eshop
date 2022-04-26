using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Entities
{
    public class OrderDetail
    {
        //public int Id { get; set; }
        public int Quantity { get; set; }

        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
