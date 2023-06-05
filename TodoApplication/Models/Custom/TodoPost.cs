namespace TodoApplication.Models.Custom
{
    public class TodoPost
    {
        public string Description { get; set; }
        public DateTimeOffset TodoDate { get; set; }
        public Guid UserId { get; set; }
    }
}
