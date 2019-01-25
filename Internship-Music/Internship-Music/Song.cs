using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Internship_Music
{
    class Song
    {
        [Key]
        public int SongId { get; set; }
        public string Name { get; set; }
        public TimeSpan SongLength { get; set; }
    }
}
