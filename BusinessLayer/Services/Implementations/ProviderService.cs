using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Dtos;
using BusinessLayer.Services.Abstractions;
using DataInterface;
using DataInterface.Entities;

namespace BusinessLayer.Services.Implementations
{
    public class ProviderService : IProviderService
    {
        private readonly EshopContext _context = new();

        public void Create(CreateProviderDto dto) 
        {
            try
            {
                _context.Providers.Add(new Provider 
                { 
                    Name = dto.Name,
                    Address = dto.Address,
                    PhoneNumber = dto.PhoneNumber,
                    EmailAddress = dto.EmailAddress,
                    City = dto.City
                });

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ProviderDto> Get() 
        {
            try
            {
                return _context.Providers
                    .Select(p => new ProviderDto { Id = p.Id, Name = p.Name })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ProviderDto? GetById(int id)
        {
            try
            {
                return _context.Providers
                    .Where(p => p.Id.Equals(id))
                    .Select(p => new ProviderDto { Id = p.Id, Name = p.Name })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
