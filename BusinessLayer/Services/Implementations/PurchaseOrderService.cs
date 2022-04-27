using BusinessLayer.Services.Abstractions;
using BusinessLayer.Dtos;
using DataInterface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataInterface;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly EshopContext _context = new();

        public void Create(CreatePurchaseOrderDto dto) 
        {
            try
            {
                var purchaseOrderHeader = new PurchaseOrderHeader 
                { 
                    ProviderId = dto.ProviderId
                };

                var createdHeader = _context.PurchaseOrderHeaders.Add(purchaseOrderHeader);

                foreach (var product in dto.Products)
                {
                    _context.PurchaseOrderDetails.Add(new PurchaseOrderDetail
                    {
                        PurchaseOrderHeader  = purchaseOrderHeader,
                        ProductId = product.Id,
                        Quantity = product.Quantity
                    });
                }

                _context.SaveChanges();   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<PurchaseOrderDto> Get() 
        {
            try
            {
                return _context.PurchaseOrderHeaders
                    .Select(poh => new PurchaseOrderDto 
                    { 
                        Id = poh.Id,
                        Provider = new ProviderDto 
                        { 
                            Id = poh.Provider.Id,
                            Name = poh.Provider.Name
                        },
                        Products = poh.PurchaseOrderDetails.Select(pod => new PurchaseOrderProductDto 
                        { 
                            Id = pod.Product.Id,
                            Name = pod.Product.Name,
                            Quantity = pod.Quantity
                        })
                        .ToList()
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public PurchaseOrderDto? GetById(int id) 
        {
            try
            {
                return _context.PurchaseOrderHeaders
                    .Where(poh => poh.Id == id)
                    .Select(poh => new PurchaseOrderDto
                    {
                        Id = poh.Id,
                        Provider = new ProviderDto
                        {
                            Id = poh.Provider.Id,
                            Name = poh.Provider.Name
                        },
                        Products = poh.PurchaseOrderDetails.Select(pod => new PurchaseOrderProductDto
                        {
                            Id = pod.Product.Id,
                            Name = pod.Product.Name,
                            Quantity = pod.Quantity
                        })
                        .ToList()
                    })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SetStatus(PurchaseOrderStatusDto dto) 
        {
            try
            {
                var purchaseOrderHeader = _context.PurchaseOrderHeaders
                    .FirstOrDefault(poh => poh.Id == dto.Id);

                if (purchaseOrderHeader is null)
                    throw new Exception($"Purchase order {dto.Id} does not exist");

                purchaseOrderHeader.Status = dto.Status;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
