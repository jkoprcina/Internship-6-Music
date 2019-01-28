using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Internship_Music
{
    internal class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public TimeSpan SongDuration { get; set; }

    }
}
