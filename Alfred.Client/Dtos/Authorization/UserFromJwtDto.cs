namespace Alfred.Client.Dtos.Authorization
{
    public class UserFromJwtDto
    {
        public int user_id { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public int nbf { get; set; }
        public int exp { get; set; }
        public int iat { get; set; }
        public string iss { get; set; }
    }
}