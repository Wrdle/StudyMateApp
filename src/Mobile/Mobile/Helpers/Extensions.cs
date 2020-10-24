using Mobile.Models;
using System.Collections.Generic;

namespace Mobile.Helpers
{
    public static class Extensions
    {
        public static ICollection<Skill> DeepCopy(this ICollection<Skill> ic)
        {
            ICollection<Skill> newCollection = new List<Skill>();
            foreach (Skill skill in ic)
            {
                newCollection.Add(skill.DeepCopy());
            }

            return newCollection;
        }
    }
}
