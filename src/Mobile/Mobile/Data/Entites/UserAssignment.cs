namespace Mobile.Data.Entites
{
    public class UserAssignment
    {
        public long UserId { get; set; }
        public long AssignmentId { get; set; }

        // Referenced Entities
        public User User { get; set; }
        public Assignment Assignment { get; set; }
    }
}
