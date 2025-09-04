using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.DTO.Clientes
{
    public class ClienteCreateDto
    {
        public string? Nombre { get; set; }
        public bool Status { get; set; }
        public int Id_Agencia { get; set; }
        public int IdClienteMms { get; set; }
    }
}
