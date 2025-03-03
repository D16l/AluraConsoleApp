using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class MusicPlayer
    {
        private string[] LyricsSplited;
        private int LyricsIndex;
        private bool StopMusic;
        private User User;
        public MusicPlayer(User user, Music music)
        {
            User = user;

            if (music.Lyrics != null)
            {
                LyricsSplited = music.Lyrics.Split(' ');
            } else
            {
                LyricsSplited = new string[0];
            }
        }
        public void Play (Music music) 
        {
            User.AddPlayedSongs(music);
            StopMusic = false;

            if (LyricsSplited.Length == 0)
            {
                Console.WriteLine("Não há letra cadastrada nessa música!");
                return;
            }

            Random random = new Random();

            while (LyricsIndex < LyricsSplited.Length)
            {
                HandleMusicControl();

                if (StopMusic)
                {
                    while (StopMusic)
                    {
                        PauseMusic();
                    }
                }
                
                Console.Write(" " + LyricsSplited[LyricsIndex]);

                int randomNumber = random.Next(1, 6);

                Thread.Sleep(randomNumber * 1000);

                LyricsIndex++;
            }

            LyricsIndex = 0;
            Console.WriteLine("\n\nMúsica terminou!");
        }
        private void HandleMusicControl()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;
                if (key == ConsoleKey.Enter)
                {
                    StopMusic = !StopMusic;
                }
            }
        }
        private void PauseMusic()
        {
            while (StopMusic)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true).Key;
                    if (key == ConsoleKey.Enter)
                    {
                        StopMusic = false;
                    }
                }
                Thread.Sleep(500);
            }
        }
    }
}
