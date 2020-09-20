namespace Mobile.Data.Entites
{
    public class UserSkill
    {
        public long UserId { get; set; }
        public int SkillId { get; set; }

        // Referenced Entities
        public User User { get; set; }
        public Skill Skill { get; set; }
    }
}
