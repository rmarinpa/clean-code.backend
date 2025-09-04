using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Core.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombre{ get; set; }
        public bool Status{ get; set; }
        public int Id_Agencia{ get; set; }
        public int IdClienteMms{ get; set; }
        public Cliente()
        {

        }
        public Cliente(string? nombre, bool status, int id_Agencia, int idClienteMms)
        {
            Nombre = nombre;
            Status = status;
            Id_Agencia = id_Agencia;
            IdClienteMms = idClienteMms;
        }

        public void Actualizar(string? nombre, bool status, int id_Agencia, int idClienteMms)
        {
            Nombre = nombre;
            Status = status;
            Id_Agencia = id_Agencia;
            IdClienteMms = idClienteMms;
        }

    }
}
