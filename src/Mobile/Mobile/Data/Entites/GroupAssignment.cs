namespace Mobile.Data.Entites
{
    public class GroupAssignment
    {
        public long GroupId { get; set; }
        public long AssignmentId { get; set; }

        // Referenced Entities
        public Group Group { get; set; }
        public Assignment Assignment { get; set; }
    }
}
