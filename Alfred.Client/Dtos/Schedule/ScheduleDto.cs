using System.Collections.Generic;

namespace Alfred.Client.Dtos.Schedule
{
    public class ScheduleDto
    {
        public int Day { get; set; }
        public List<EventForScheduleViewDto> Events { get; set; }
    }
}