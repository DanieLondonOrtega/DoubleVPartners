namespace DoubleVPartners.Domain.Entities
{
    public class User
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public Role Role { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
