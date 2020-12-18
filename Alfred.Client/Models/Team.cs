namespace Alfred.Client.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}