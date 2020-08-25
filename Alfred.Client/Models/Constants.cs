using System;
using System.Collections.Generic;

namespace Alfred.Client.Models
{
    public class Constants
    {
        public List<string> Category { get; set; }
        public List<string> EventType { get; set; }
        public List<string> EventStatus { get; set; }
        public Constants()
        {
            Category = new List<string>()
            {
                "All"
            };
            EventType = new List<string>()
            {
                "All"
            };
            EventStatus = new List<string>()
            {
                "All"
            };
        }
    }
}