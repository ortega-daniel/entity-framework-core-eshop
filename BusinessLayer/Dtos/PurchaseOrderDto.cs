using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }
        public ProviderDto Provider { get; set; }
        public List<PurchaseOrderProductDto> Products { get; set; }
    }
}
