namespace Mobile.Data.Entites
{
    public class ChecklistItem
    {
        public long Id { get; set; }
        public long CheckpointId { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }

        // Referenced Entities
        public Checkpoint Checkpoint { get; set; }
    }
}
