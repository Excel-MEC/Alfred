using System.Collections.Generic;

namespace Alfred.Client.Dtos.Admin
{
    public class StaffForListViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Role { get; set; }
        public string Picture { get; set; }
        public string QRCodeUrl { get; set; }
        public int? InstitutionId { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int? ReferrerAmbassadorId { get; set; }
        public bool IsPaid { get; set; }
    }
}