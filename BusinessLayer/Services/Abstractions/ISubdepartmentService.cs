using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface ISubdepartmentService
    {
        void Create(CreateSubdepartmentDto dto);
        List<SubdepartmentDto> Get();
        List<SubdepartmentDto> GetByDepartmentId(int id);
        SubdepartmentDto? GetById(int id);
    }
}
