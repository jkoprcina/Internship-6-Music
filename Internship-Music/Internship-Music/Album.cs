using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internship_Music
{
    class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public DateTime YearOfRelease { get; set; }
        public int MusicianId { get; set; }
        [ForeignKey("MusicianId")] public virtual Musician Musician { get; set; }
    }
}
