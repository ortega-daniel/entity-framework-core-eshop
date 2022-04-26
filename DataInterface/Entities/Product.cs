using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInterface.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }

        public int SubdepartmentId { get; set; }
        public Subdepartment Subdepartment { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
