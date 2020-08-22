using System;

namespace Alfred.Client.Dtos.Events
{
    public class EventRoundForViewDto
    {
        public int Id { get; set; }
        public string Round { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}