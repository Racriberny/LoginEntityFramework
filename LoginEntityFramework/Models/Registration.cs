namespace LoginEntityFramework.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String Token { get; set; }
    }
}
