using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using Alfred.Client.Data.Interfaces;

namespace Alfred.Client.Data
{
    public class ConstantRepository: IConstantRepository
    {
        public event Action OnChange;
        public List<string> Category { get; set; }
        public List<string> EventType{ get;  set; }
        public List<string> EventStatus{ get; set; }

        public void Set(ConstantRepository constants)
        {
            Category = constants.Category;
            EventStatus = constants.EventStatus;
            EventType = constants.EventType;
            OnChange?.Invoke();
        }

        public ConstantRepository()
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