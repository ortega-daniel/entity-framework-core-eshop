using BusinessLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Abstractions
{
    public interface IProviderService
    {
        void Create(CreateProviderDto dto);
        List<ProviderDto> Get();
        ProviderDto? GetById(int id);
    }
}
