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
    public class DepartmentService : IDepartmentService
    {
        private readonly EshopContext _context = new();

        public void Create(CreateDepartmentDto dto) 
        {
            try
            {
                _context.Departments.Add(new Department { Name = dto.Name });
                _context.SaveChanges();
                Console.WriteLine("Department added");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
