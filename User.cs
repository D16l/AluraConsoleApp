using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class User
    {
        private string? _name;
        public string? Name 
        {
            get 
            {
                return _name;
            }
            set
            {
                if (Users != null && Users.Any(p => p.Value.Item1 == value))
                {
                    Console.WriteLine("Usuário já cadastrado");
                } 
                else
                {
                    _name = value;
                }
            } 
        }
        public string? Password { get; set; }
        public List<Music> Favorite { get; private set; }
        public List<Playlist> Playlists { get; set; }
        public List<Music> PlayedSongs { get; set; }
        public Dictionary<Type, HashSet<object>> ReceivedItems { get; set; }

        public static Dictionary<User,Tuple<string, string>> Users = new Dictionary<User,Tuple<string, string>>();
        public User(string name, string password)
        {
            _name = name;
            Password = password;
            Favorite = new List<Music>();
            Playlists = new List<Playlist>();
            PlayedSongs = new List<Music>();
            ReceivedItems = new Dictionary<Type, HashSet<object>>();
            Users.Add(this, new Tuple<string,string>(name, password));
        }
        public void AddPlayedSongs(Music music)
        {
            if (!PlayedSongs.Contains(music))
            {
                PlayedSongs.Add(music);
            }
        }

        public void AddFavoriteMusic(Music music)
        {
            Favorite.Add(music);
        }

        public void AddReceivedItems<T>(T item)
        {
            Type type = typeof(T);

            if (!ReceivedItems.ContainsKey(type))
            {
                ReceivedItems[type] = new HashSet<object>();

            }
            ReceivedItems[type].Add(item);
        }
        public static bool FindUser(string name, string password)
        {
            if (Users == null || !Users.Any())
            {
                return false;
            }

            var user = Users.Any(p => 
                p.Key != null &&
                p.Value != null &&
                p.Value.Item1 != null &&
                p.Value.Item2 != null &&
                p.Value.Item1 == name && 
                p.Value.Item2 == password
            );
            return user;
        }

        public override string ToString()
        {
            return Name;
        }
        public static implicit operator User(ThreadLocal<User> threadLocalUser)
        {
            return threadLocalUser.Value;
        }

    }
    
}
