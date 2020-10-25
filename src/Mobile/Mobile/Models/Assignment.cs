using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Mobile.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace Mobile.Models
{
    public class Assignment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public DateTime DateDue { get; set; }
        public string GroupName { get; set; }
        public bool IsArchived { get; set; }
        public byte[] CoverPhotoBytes { get; set; }
        public CoverColor CoverColor { get; set; }

        /// <summary>
        /// Returns DueDate as a formatted string
        /// </summary>
        public string DateDueString
        {
            get
            {
                return ("Due " + DateDue.ToString("ddd d \\o\\f MMMM yyyy")).ToUpper();
            }
        }

        public string DateDueSlashNotation
        {
            get
            {
                return "Due: " + DateDue.ToString("d/M/yyyy");
            }
        }

        public string DateDueMonthDay
        {
            get => $"{DateDue:M}";
        }

        public string ListDescription
        {
            get
            {
                if (string.IsNullOrEmpty(GroupName))
                {
                    return $"Individual | {DateDueMonthDay}";
                }
                else
                {
                    return $"{GroupName} | {DateDueMonthDay}";
                }
            }
        }

        public Assignment()
        {
            Skills = new List<Skill>();
        }


        public ImageSource CoverPhoto
        {
            get
            {
                if (CoverPhotoBytes != null)
                    return ImageSource.FromStream(() => new MemoryStream(CoverPhotoBytes));
                return null;
            }
            set
            {
                if (value != null)
                {
                    var cancellationToken = System.Threading.CancellationToken.None;
                    using (var imageStream = ((StreamImageSource)value).Stream(cancellationToken).Result)
                    using (var byteStream = new MemoryStream())
                    {
                        imageStream.CopyTo(byteStream);
                        CoverPhotoBytes = byteStream.ToArray();
                    }
                } 
                else
                {
                    CoverPhotoBytes = new byte[] { };
                }
            }
        }
    }
}
