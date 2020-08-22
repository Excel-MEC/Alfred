using System;

namespace Alfred.Client.Dtos.Events
{
    public class EventHead: ICloneable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public void Set(EventHead unchanged)
        {
            Id = unchanged.Id;
            Name = unchanged.Name;
            Email = unchanged.Email;
            PhoneNumber = unchanged.PhoneNumber;
        }
    }
}