
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanCode.Application.DTO.Clientes;
using CleanCode.Application.Interfaces;
using CleanCode.Core.Entities;
using CleanCode.Infraestructura.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanCode.Application.Services
{
    public class ClienteServicio : IClienteServicio
    {

        private readonly CleanCodeDbContext cleanCodeDbContext;
        private readonly ILogger<ClienteServicio> logger;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ClienteServicio(CleanCodeDbContext cleanCodeDbContext, ILogger<ClienteServicio> logger, IMapper mapper, IMediator mediator)
        {
            this.cleanCodeDbContext = cleanCodeDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<ClienteDto> CreateAsync(ClienteCreateDto dto, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Creación de Cliente {dto.Nombre}");

            try
            {
                var cliente = new Cliente(dto.Nombre, dto.Status, dto.Id_Agencia, dto.IdClienteMms);

                await cleanCodeDbContext.Cliente.AddAsync(cliente, cancellationToken);
                await cleanCodeDbContext.SaveChangesAsync(cancellationToken);
                return mapper.Map<ClienteDto>(cliente);


            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error, cae en catch: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public Task<ClienteDto> DeleteAsync(int id, ClienteCreateDto dto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ClienteGetAllReplay> GetAllFiltradoAsync(ClienteGetAllRequest input, CancellationToken cancellationToken)
        {
            try
            {
                var qry = cleanCodeDbContext.Cliente.AsNoTracking().AsQueryable();

                var total = await qry.CountAsync();

                logger.LogInformation($"Cantidad de clientes según filtro {total}");

                var page = input.Pagina < 1 ? 1 : input.Pagina;

                var listado = await qry
                    .OrderBy(c => c.Id)
                    .Skip((page - 1) * input.CantidadPorPagina)
                    .Take(input.CantidadPorPagina)
                    .ProjectTo<ClienteListDto>(mapper.ConfigurationProvider)
                    .ToArrayAsync();

                ClienteGetAllReplay dto = new(total, listado);

                return dto;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error, cae en catch: {ex.Message}");
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> GetAllIdAsync(int id, CancellationToken cancellationToken)
        {
            ClienteDto? result = await cleanCodeDbContext.Cliente
             .ProjectTo<ClienteDto>(mapper.ConfigurationProvider)
             .FirstOrDefaultAsync(c => c.Id == id);

            return result;
        }

        public async Task<ClienteDto> UpdateAsync(int id, ClienteCreateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await cleanCodeDbContext.Cliente.FirstOrDefaultAsync(c => c.Id == id);

                if (cliente == null)
                {
                    throw new Exception("Cliente no encontrado");
                }

                cliente.Actualizar(dto.Nombre, dto.Status, dto.Id_Agencia, dto.IdClienteMms);
                
                await cleanCodeDbContext.SaveChangesAsync(cancellationToken);

                return mapper.Map<ClienteDto>(cliente);
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Error, cae en catch: {ex.Message}");
                throw new Exception(ex.Message);
            }


        }
    }
}
