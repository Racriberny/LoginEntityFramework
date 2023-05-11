namespace LoginEntityFramework.Models
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public String Passwd { get; set; }
        public String Correo { get; set; }
        public String Token { get; set; }
    }
}
