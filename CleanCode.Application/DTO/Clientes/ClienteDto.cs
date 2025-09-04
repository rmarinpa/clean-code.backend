namespace CleanCode.Application.DTO.Clientes
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Status { get; set; }
        public int Id_Agencia { get; set; }
        public int IdClienteMms { get; set; }
    }
}
