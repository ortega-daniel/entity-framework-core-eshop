﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Dtos
{
    public class CreatePurchaseOrderDto
    {
        public int ProviderId { get; set; }
        public List<PurchaseOrderProductDto> Products { get; set; }
    }
}
