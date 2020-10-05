using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Mobile.Helpers
{
    public class SMColours
    {
        public static SMColour DarkGray = new SMColour("4F4F4F", Color.White);
        public static SMColour LightGray = new SMColour("828282", Color.White);
        public static SMColour Red = new SMColour("EB5757", Color.White);
        public static SMColour Orange = new SMColour("F2994A", Color.White);
        public static SMColour Yellow = new SMColour("F2C94C", Color.White);
        public static SMColour DarkGreen = new SMColour("219653", Color.White);
        public static SMColour Green = new SMColour("27AE60", Color.White);
        public static SMColour LightGreen = new SMColour("6FCF97", "4F4F4F"); // Gray Text
        public static SMColour DarkBlue = new SMColour("2F80ED", Color.White);
        public static SMColour Blue = new SMColour("2D9CDB", Color.White);
        public static SMColour LightBlue = new SMColour("56CCF2", Color.White);
        public static SMColour Purple = new SMColour("9B51E0", Color.White);
        public static SMColour LightPurple = new SMColour("BB6BD9", Color.White);

        public static List<SMColour> Colours = new List<SMColour>()
        {
            DarkGray,
            LightGray,
            Red,
            Orange,
            Yellow,
            DarkGreen,
            Green,
            LightGreen,
            DarkBlue,
            Blue,
            LightBlue,
            Purple,
            LightPurple
        };

        public static SMColour getColourByName(string ColourName)
        {
            return (SMColour)typeof(SMColours).GetField(ColourName).GetValue(new SMColours());
        }

        public static string getColourName(SMColour colour)
        {
            return nameof(colour);
        }
    }
}
