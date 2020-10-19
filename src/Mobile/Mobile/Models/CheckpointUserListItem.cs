namespace Mobile.Models
{
    public class CheckpointUserListItem
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDone { get; set; }
    }
}
