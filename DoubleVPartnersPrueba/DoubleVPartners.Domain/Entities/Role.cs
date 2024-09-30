namespace DoubleVPartners.Domain.Entities
{
    /// <summary>
    /// Domain class for managing roles.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or sets the identifier role.
        /// </summary> 
        public int IdRole { get; set; }
        /// <summary>
        /// Gets or sets the name owner.
        /// </summary> 
        public string NameRole { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
