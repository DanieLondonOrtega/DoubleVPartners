namespace DoubleVPartners.API.Models
{
    public class UserModel
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UserUpdateModel : UserModel
    {
        public int IdUser { get; set; }
    }
}
