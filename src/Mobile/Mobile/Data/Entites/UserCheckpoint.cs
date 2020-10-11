namespace Mobile.Data.Entites
{
    public class UserCheckpoint
    {
        public long UserId { get; set; }
        public long CheckpointId { get; set; }
        public bool IsDone { get; set; }

        // Referenced Entities
        public User User { get; set; }
        public Checkpoint Checkpoint { get; set; }

    }
}
