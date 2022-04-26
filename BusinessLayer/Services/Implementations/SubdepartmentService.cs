using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstractions;
using BusinessLayer.Dtos;
using DataInterface.Entities;
using DataInterface;


namespace BusinessLayer.Services.Implementations
{
    public class SubdepartmentService : ISubdepartmentService
    {
        private readonly EshopContext _context = new();

        public void Create(CreateSubdepartmentDto dto) 
        {
            try
            {
                var department = _context.Departments
                    .FirstOrDefault(d => d.Id.Equals(dto.DepartmentId));

                if (department is null)
                    throw new Exception($"Department {dto.DepartmentId} does not exist");

                _context.Subdepartments.Add(new Subdepartment { Name = dto.Name, DepartmentId = dto.DepartmentId });
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SubdepartmentDto> Get() 
        {
            try
            {
                return _context.Subdepartments
                    .Select(s => new SubdepartmentDto { Id = s.Id, Name = s.Name })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public SubdepartmentDto? GetById(int id) 
        {
            try
            {
                return _context.Subdepartments
                    .Where(s => s.Id.Equals(id))
                    .Select(s => new SubdepartmentDto { Id = s.Id, Name = s.Name })
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SubdepartmentDto> GetByDepartmentId(int id) 
        {
            try
            {
                return _context.Subdepartments
                    .Where(s => s.DepartmentId.Equals(id))
                    .Select(s => new SubdepartmentDto { Id = s.Id, Name = s.Name })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
