using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class Album : Search
    {
        public List<Music> Musics { get; set; }
        public List<Artist> Artists { get; set; }
        public static Dictionary<string, Tuple<List<Artist>, List<Music>>> Albuns = new Dictionary<string, Tuple<List<Artist>, List<Music>>>();
        public Album(string name, List<Artist>? artists = null, List<Music>? musics = null) : base(name, "Album")
        {
            if (Albuns.ContainsKey(name))
            {
                Artists = Albuns[name].Item1;
                Musics = Albuns[name].Item2;

                if (artists != null)
                    Artists.AddRange(artists.Where(artist => !Artists.Contains(artist)));

                if (musics != null)
                    Musics.AddRange(musics);

                Albuns[name] = new Tuple<List<Artist>, List<Music>>(artists ?? new List<Artist>(), musics ?? new List<Music>());
            }

        }
        public override string ToString()
        {
            return Name;
        }
    }
}
