using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class Playlist : Search
    {
        private List<Music> Musics { get; set; }

        public Playlist(string name) : base(name,"Playlist")
        {
            Musics = new List<Music>();
        }

        public void AddMusic(Music music) 
        {
            Musics.Add(music);
            Console.WriteLine($"Música '{music.Name}' adicionada à playlist '{Name}.'");
        }

        public void RemoveMusic(Music music)
        {
            Musics.Remove(music);
            Console.WriteLine($"Música '{music.Name}' removida à playlist '{Name}.'");
        }

        public void ShowPlaylist()
        {
            Console.WriteLine($"\nPlaylist: {Name}");
            int i = 0;
            foreach(var music in Musics)
            {
                i++;
                Console.WriteLine($"\n{i}. {music.Name} - {string.Join(", ", music.Artists.Select(a => a.Name))}");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
