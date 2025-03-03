using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    public enum Genre
    {
        Rock,
        Jazz,
        Pop,
        Funk,
        HipHop,
        Country,
        Samba,
        Sertanejo,
        Reggae,
        MPB,
        Soul,
        Grunge
    }
    internal class Music : Search
    {
        static int counter = 0;
        public int Id { get; private set; }
        public string? Description { get; set; }
        public Genre Genre { get; set; }
        public string? Lyrics { get; set; }
        public List<Artist> Artists { get; set; }
        public static List<Music> AllMusics { get; set; } = new List<Music>();
        public Album Album { get; set; }
        public Music(string name, string description, Genre genre, string lyrics,string album,List<Artist>? artists = null) : base(name,"Music")
        {
            counter++;
            Id = counter;
            Description = description;
            Genre = genre;
            Lyrics = lyrics;
            Artists = artists ?? new List<Artist>();
            AllMusics.Add(this);
            Album = new Album(album, artists, new List<Music> { this });
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
