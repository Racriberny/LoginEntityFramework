namespace LoginEntityFramework.Models
{
    public class Pers_Portal_Servicios_Acta_Instalacion_Comercial
    {
        public int Id { get; set; }
        public String IdCliente{ get; set; }
        public int IdProyecto { get; set; }
        public int IdComercial { get; set; }
        public Boolean Mantenimiento { get; set; }
        public String Puestos { get; set; }
        public String SO { get; set;}
        public Boolean ConexionRed { get; set;}
        public Boolean RetiradaMaquina { get; set; }
        public String MaquinaRetirada { get; set;}
        public int Contadores{ get; set; }
        public String DireccionEnvio { get; set; }
        public String Contacto { get; set; }
        public String Telefono { get; set; }
        public String Horario { get; set; }
        public String Observaciones { get; set; }
        public DateTime fecha { get; set; }
    }
}
