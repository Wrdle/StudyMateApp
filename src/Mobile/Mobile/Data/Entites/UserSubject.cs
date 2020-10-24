namespace Mobile.Data.Entites
{
    public class UserSubject
    {
        public string Subject { get; set; }
        public bool IsCurrent { get; set; }
        public long UserId { get; set; }

        // Referenced Entities
        public User User { get; set; }

        public UserSubject()
        {

        }
    }
}
