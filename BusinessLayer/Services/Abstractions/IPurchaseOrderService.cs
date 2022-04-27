using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IPurchaseOrderService
    {
        void Create(CreatePurchaseOrderDto dto);
        List<PurchaseOrderDto> Get();
        PurchaseOrderDto? GetById(int id);
        void SetStatus(PurchaseOrderStatusDto dto);
    }
}
