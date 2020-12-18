namespace Alfred.Client.Dtos.Events
{
    public class RegistrationForViewDto
    {
        public int Id { get; set; }
        public int ExcelId { get; set; }
        public int? TeamId { get; set; }
        public string TeamName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }
        public string QRCodeUrl { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Category { get; set; }
        public bool IsPaid { get; set; }
    }
}