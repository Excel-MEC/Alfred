using System.Collections.Generic;

namespace Alfred.Client.Models.Components
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public List<MenuItem> Children { get; set; }
    }
}