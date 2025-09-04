using CleanCode.Application.DTO.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.Interfaces
{
    public interface IClienteServicio
    {
        //CRUD:
        //CREATE
        Task<ClienteDto> CreateAsync(ClienteCreateDto dto, CancellationToken cancellationToken);
        //READ (Por Id)
        Task<ClienteDto> GetAllIdAsync(int id, CancellationToken cancellationToken);
        //READ
        Task<ClienteGetAllReplay> GetAllFiltradoAsync(ClienteGetAllRequest input, CancellationToken cancellationToken);
        //UPDATE
        Task<ClienteDto> UpdateAsync(int id, ClienteCreateDto dto, CancellationToken cancellationToken);
        //DELETE
        Task<ClienteDto> DeleteAsync(int id, ClienteCreateDto dto, CancellationToken cancellationToken);
    }
}
