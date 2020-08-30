using System;
using System.Collections.Generic;
using Alfred.Client.Pages.Events.Components;

namespace Alfred.Client.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string EventType { get; set; }
        public string About { get; set; }
        public string Format { get; set; }
        public string Rules { get; set; }
        public string Venue { get; set; }
        public int? Day { get; set; }
        public DateTime Datetime { get; set; }
        public int? EntryFee { get; set; }
        public int? PrizeMoney { get; set; }
        public int? EventHead1Id { get; set; }
        public EventHead EventHead1 { get; set; }
        public int? EventHead2Id { get; set; }
        public EventHead EventHead2 { get; set; }
        public bool IsTeam { get; set; }
        public int? TeamSize { get; set; }
        public int EventStatusId { get; set; }
        public string EventStatus { get; set; }
        public int? NumberOfRounds { get; set; }
        public int? CurrentRound { get; set; }
        public bool? NeedRegistration { get; set; }
    }
}