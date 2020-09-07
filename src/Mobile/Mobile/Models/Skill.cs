namespace Mobile.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Skill(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
