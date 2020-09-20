namespace Mobile.Data.Entites
{
    public class UserGroup
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }

        // Referenced Entities
        public User User { get; set; }
        public Group Group { get; set; }
    }
}
