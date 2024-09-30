namespace DoubleVPartners.Aplication.Dtos
{
    public class UserDto
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }
}
