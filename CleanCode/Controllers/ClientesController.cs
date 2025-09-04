using AutoMapper;
using CleanCode.Application.DomainExceptions;
using CleanCode.Application.DTO.Clientes;
using CleanCode.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanCode.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteServicio clienteServicio;
        private readonly ILogger<ClientesController> logger;
        private readonly IMapper mapper;

        public ClientesController(IClienteServicio clienteServicio, ILogger<ClientesController> logger, IMapper mapper)
        {
            this.clienteServicio = clienteServicio;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> CrearCliente(ClienteCreateDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var clienteCreado = await clienteServicio.CreateAsync(dto, cancellationToken);

                var clienteDto = mapper.Map<ClienteDto>(clienteCreado);

                return CreatedAtAction(nameof(ObtenerUnCliente), new { id = clienteDto.Id }, clienteDto);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                return BadRequest();

            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> ActualizarCliente(int id, ClienteCreateDto dto, CancellationToken cancellationToken)
        {
            
            try
            {
                var cliente = await clienteServicio.UpdateAsync(id, dto, cancellationToken);

                var clienteDto = mapper.Map<ClienteDto>(cliente);

                return Ok(clienteDto);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Campo, ex.Message);
                logger.LogWarning(ex.Message);
                return ValidationProblem(ModelState);
            }
        }

        [HttpGet()]
        public async Task<ActionResult<ClienteListDto[]>> ObtenerListado([FromQuery] ClienteGetAllRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ClienteGetAllReplay dto = await clienteServicio.GetAllFiltradoAsync(request, cancellationToken);


                if (dto.Listado.Count() == 0)
                {
                    logger.LogWarning($"Listado vacio");
                    return NoContent();
                }

                Response.Headers.Add("x-pagination-total", dto.Total.ToString());

                return dto.Listado;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> ObtenerUnCliente(int id, CancellationToken cancellationToken)
        {
            try
            {
                var res = await clienteServicio.GetAllIdAsync(id, cancellationToken);

                if (res == null)
                {
                    logger.LogWarning($"No existe cliente con el id {id}");
                    return NoContent();
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                return BadRequest();
            }
        }
    }
}
