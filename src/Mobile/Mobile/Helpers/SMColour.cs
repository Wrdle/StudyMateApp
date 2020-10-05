using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Mobile.Helpers
{
    public class SMColour
    {
        public Color BackgroundColour { get; set; }
        public Color FontColour { get; set; }

        public SMColour(Color backgroundColour, Color fontColour)
        {
            BackgroundColour = backgroundColour;
            FontColour = fontColour;
        }

        public SMColour(string hexBackgroundColour, string hexFontColour)
        {
            BackgroundColour = Color.FromHex(hexBackgroundColour);
            FontColour = Color.FromHex(hexFontColour);
        }

        public SMColour(string hexBackgroundColour, Color fontColour)
        {
            BackgroundColour = Color.FromHex(hexBackgroundColour);
            FontColour = fontColour;
        }
    }
}
