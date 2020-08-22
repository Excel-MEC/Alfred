using System.IO;

namespace Alfred.Client.Models.Components
{
    public class CustomFile
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public Stream Data { get; set; }

        public CustomFile()
        {
            Name = string.Empty;
            Type = string.Empty;
            Size = string.Empty;
            Path = null;
            Data = null;
        }
    }
}