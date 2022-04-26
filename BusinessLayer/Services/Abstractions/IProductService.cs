using BusinessLayer.Dtos;
using DataInterface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IProductService
    {
        void CreateProduct(CreateProductDto dto);
        void DeleteProduct(int id);
        List<ProductDto> Get();
        ProductDto? GetById(int id);
        void UpdateProduct(UpdateProductDto dto);
    }
}
