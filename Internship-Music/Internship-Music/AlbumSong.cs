using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internship_Music
{
    class AlbumSong
    {
        public int AlbumId { get; set; }
        public int SongId { get; set; }
        [ForeignKey("AlbumId")] public virtual Album Album { get; set; }
        [ForeignKey("SongId")] public virtual Song Song { get; set; }
    }
}
