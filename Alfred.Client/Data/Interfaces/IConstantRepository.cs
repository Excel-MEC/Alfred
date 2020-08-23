using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alfred.Client.Data.Interfaces
{
    public interface IConstantRepository
    {
        public event Action OnChange;
        public List<string> Category { get; }
        public List<string> EventType{ get;}
        public List<string> EventStatus{ get; }

        public void Set(ConstantRepository constants);
    }
}