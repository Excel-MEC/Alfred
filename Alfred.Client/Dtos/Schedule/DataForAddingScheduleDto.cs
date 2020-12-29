using System;

namespace Alfred.Client.Dtos.Schedule
{
    public class DataForAddingScheduleDto
    {
        public int EventId { get; set; }
        public int RoundId { get; set; }
        public string Round { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}