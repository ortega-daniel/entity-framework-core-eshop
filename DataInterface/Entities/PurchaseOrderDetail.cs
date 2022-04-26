using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Entities
{
    public class PurchaseOrderDetail
    {
        //public int Id { get; set; }
        public int Quantity { get; set; }

        public int PurchaseOrderHeaderId { get; set; }
        public PurchaseOrderHeader PurchaseOrderHeader { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
