using DataInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class PurchaseOrderStatusDto
    {
        public int Id { get; set; }
        public PurchaseOrderStatus Status { get; set; }
    }
}
