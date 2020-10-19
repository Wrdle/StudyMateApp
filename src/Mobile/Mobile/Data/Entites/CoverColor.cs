using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Xamarin.Forms;

namespace Mobile.Data.Entites
{
    public class CoverColor
    {
        public int Id { get; set; }

        [Column(TypeName = "char(6)")]
        public string BackgroundColorHex { get; set; }
        [Column(TypeName = "char(6)")]
        public string FontColorHex { get; set; }

        // Referencing Entities
        public ICollection<Group> Groups { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

        // Not Mapped
        [NotMapped]
        public Color BackgroundColorFromHex
        {
            get
            {
                return Color.FromHex(BackgroundColorHex);
            }
        }
        [NotMapped]
        public Color FontColorFromHex
        {
            get
            {
                return Color.FromHex(FontColorHex);
            }
        }

    }
}