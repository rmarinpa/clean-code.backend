using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.DTO.Clientes
{
    public class ClienteGetAllRequest
    {
        public int Pagina { get; set; } = 1;
        public int CantidadPorPagina { get; set; }
    }
}
