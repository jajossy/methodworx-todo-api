namespace TodoApplication.Models.Custom
{
    public class JwtResponse
    {
        public string jwt { get; set; }
        public Guid userId { get; set; }
        public string username { get; set; }
    }
}
