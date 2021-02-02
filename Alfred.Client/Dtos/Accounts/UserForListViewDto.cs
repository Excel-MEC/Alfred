﻿namespace Alfred.Client.Dtos.Accounts
{
    public class UserForListViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }
        public string QRCodeUrl { get; set; }
        public string Gender { get; set; }
        public string MobileNumber { get; set; }
        public string Category { get; set; }
        public bool IsPaid { get; set; }
        public int? InstitutionId { get; set; }
        public string Institution { get; set; }
    }
}