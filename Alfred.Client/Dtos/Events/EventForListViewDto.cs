using System;

namespace Alfred.Client.Dtos.Events
{
    public class EventForListViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string EventType { get; set; }
        public string Category { get; set; }
        public bool? NeedRegistration { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }

}