using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internship_Music
{
    class Musician
    {
        [Key]
        public int MusicianId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
    }
}
