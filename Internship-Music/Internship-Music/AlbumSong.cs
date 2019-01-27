using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internship_Music
{
    internal class AlbumSong
    {
        public int AlbumId { get; set; }
        public int SongId { get; set; }
    }
}
