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

        public void CreateProduct(CreateProductDto dto)
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

        public List<ProductDto> Get()
        {
            try
            {
                return _context.Products
                    .Select(p => new ProductDto 
                    { 
                        Id = p.Id,
                        Name = p.Name,
                        Stock = p.Stock,
                        Price = p.Price,
                        Sku = p.Sku,
                        Description = p.Description,
                        Brand = p.Brand,
                        SubdepartmentId= p.SubdepartmentId
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProductDto? GetById(int id)
        {
            try
            {
                return _context.Products
                    .Where(p => p.Id.Equals(id))
                    .Select(p => new ProductDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Stock = p.Stock,
                        Price = p.Price,
                        Sku = p.Sku,
                        Description = p.Description,
                        Brand = p.Brand,
                        SubdepartmentId = p.SubdepartmentId
                    })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(UpdateProductDto dto)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Id.Equals(dto.Id));

                if (product is null)
                    throw new Exception($"Product {dto.Id} doest not exist");

                product.Name = String.IsNullOrEmpty(dto.Name) ? product.Name : dto.Name;
                product.Price = dto.Price;
                product.Sku = String.IsNullOrEmpty(dto.Sku) ? product.Sku: dto.Sku;
                product.Description = String.IsNullOrEmpty(dto.Description) ? product.Description: dto.Description;
                product.Brand = String.IsNullOrEmpty(dto.Brand) ? product.Brand: dto.Brand;

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(int id)
        {
            try
            {
                var product = _context.Products
                    .FirstOrDefault(p => p.Id.Equals(id));

                if (product is null)
                    throw new Exception($"Product {id} does not exist");

                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
