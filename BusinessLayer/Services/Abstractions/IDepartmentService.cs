using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IDepartmentService
    {
        void Create(CreateDepartmentDto dto);
    }
}
