using AutoMapper;
using CleanCode.Application.DTO.Clientes;
using CleanCode.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.Mapping.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteDto>();
            CreateMap<Cliente, ClienteListDto>();
        }
    }
}
