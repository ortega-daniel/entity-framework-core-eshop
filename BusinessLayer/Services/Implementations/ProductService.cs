using BusinessLayer.Services.Abstractions;
using DataInterface;
using DataInterface.Entities;
using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly EshopContext _context = new();

        public void AddProduct(CreateProductDto dto)
        {
            try
            {
                var subdepartment = _context.Subdepartments.FirstOrDefault(s => s.Id.Equals(dto.SubdepartmentId));

                if (subdepartment is null)
                    throw new Exception("Subdepartment does not exist");

                var product = new Product
                {
                    Name = dto.Name,
                    Stock = dto.Stock,
                    Price = dto.Price,
                    Sku = dto.Sku,
                    Description = dto.Description,
                    Brand = dto.Brand,
                    SubdepartmentId = dto.SubdepartmentId
                };

                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
