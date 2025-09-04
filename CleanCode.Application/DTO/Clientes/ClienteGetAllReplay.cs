using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.DTO.Clientes
{
    public class ClienteGetAllReplay
    {
        public int Total { get; set; }
        public ClienteListDto[] Listado { get; set; }

        public ClienteGetAllReplay(int total, ClienteListDto[] listado)
        {
            Total = total;
            Listado = listado;
        }
    }
}
