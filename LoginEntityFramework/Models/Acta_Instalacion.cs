namespace LoginEntityFramework.Models
{
    public class Acta_Instalacion
    {
        public int Id { get; set; }
        public int IdTecnico { get; set; }
        public int IdParte { get; set; }
        public String Modelo { get; set; }
        public String Serie { get; set; }
        public int IdContacto { get; set; }
        public String Telefono { get; set; }
        public String Horario { get; set; }
        public String Email { get; set; }
       
        public String DireccionIp {get; set; }
        public int IdTipo { get; set; }
        public String Ubicacion { get; set; }
        public Boolean CajaToner { get; set; }
        public String Adhesivo { get; set; }
        public Boolean Retirar { get; set; }
        public String Marca { get; set; }
        public String ModeloMaquina { get; set; }
        public String SerieMaquina { get; set; }
        public int N_Copias { get; set; }
        public Boolean B_N { get; set; }
        public Boolean CL { get; set; }
        public String Observaciones { get; set; }



    }
}
