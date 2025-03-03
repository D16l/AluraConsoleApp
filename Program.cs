using AluraConsoleApp;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;


namespace AluraConsoleApp
{
    class Program
    {
        #region Start of the application
        static ThreadLocal<User> CurrentUser = new ThreadLocal<User>();
        static void Main(string[] args)
        {
            Artist abel = new Artist("Abel Makkonen", "Tesfaye", 34, new DateTime(1990, 2, 16, 7, 10, 24));
            Artist freddie = new Artist("Freddie", "Mercury", 45, new DateTime(1946, 9, 5, 0, 0, 0));
            Artist brian = new Artist("Brian", "May", 77, new DateTime(1947, 7, 19, 0, 0, 0));
            Artist roger = new Artist("Roger", "Taylor", 75, new DateTime(1949, 7, 26, 0, 0, 0));
            Artist john = new Artist("John", "Deacon", 73, new DateTime(1951, 8, 19, 0, 0, 0));
            Artist edward = new Artist("Edward Christopher", "Sheeran", 33, new DateTime(1991, 2, 17, 0, 0, 0));
            Artist adele = new Artist("Adele Laurie Blue", "Adkins", 35, new DateTime(1988, 5, 5, 0, 0, 0));
            Artist kurt = new Artist("Kurt", "Cobain", 27, new DateTime(1967, 2, 20, 0, 0, 0));
            Artist krist = new Artist("Krist", "Novoselic", 59, new DateTime(1965, 5, 16, 0, 0, 0));
            Artist dave = new Artist("Dave", "Grohl", 56, new DateTime(1969, 1, 14, 0, 0, 0));
            Artist johnWinston = new Artist("John Winston Ono", "Lennon", 40, new DateTime(1940, 10, 9, 0, 0, 0));

            new Music("Blinding Lights",
                      "Um hit que mistura elementos do pop e synthwave, com uma vibe dos anos 80. A música fala sobre a busca por uma pessoa amada, mas também reflete a solidão e o desejo de reconexão.",
                      Genre.Pop,
                      "I said, ooh, I'm blinded by the lights / No, I can't sleep until I feel your touch...",
                      "After Hours (2020)",
                      new List<Artist> { abel });

            new Music("Bohemian Rhapsody",
                      "Uma das músicas mais icônicas do rock, conhecida pela sua complexidade e mistura de estilos. A letra é um mistério aberto à interpretação, mas muitos consideram uma reflexão sobre arrependimentos e escolhas difíceis.",
                      Genre.Rock,
                      "Is this the real life? Is this just fantasy? / Caught in a landslide, no escape from reality...",
                      "A Night at the Opera (1975)",
                      new List<Artist> { freddie });

            new Music("Shape of You",
                      "Uma música dançante e cativante, com influências de dancehall. Fala sobre uma atração física e uma conexão imediata com alguém.",
                      Genre.Pop,
                      "The club isn't the best place to find a lover / So the bar is where I go...",
                      "Divide (2017)",
                      new List<Artist> { edward });

            new Music("Rolling in the Deep",
                      "Uma música de poder e emoção que fala sobre traição e a dor de um coração partido. A voz poderosa de Adele brilha em cada verso, tornando essa música um hino de superação.",
                      Genre.Soul,
                      "We could have had it all / Rolling in the deep...",
                      "21 (2011)",
                      new List<Artist> { adele });

            new Music("Smells Like Teen Spirit",
                      "Conhecida como a 'hino de uma geração', essa música capturou a essência do movimento grunge. Com riffs pesados e uma atitude rebelde, ela aborda o desespero e a frustração de ser jovem.",
                      Genre.Grunge,
                      "With the lights out, it's less dangerous / Here we are now, entertain us...",
                      "Nevermind (1991)",
                      new List<Artist> { kurt, dave });

            new Music("Imagine",
                      "Uma música que se tornou um hino da paz. John Lennon imagina um mundo sem fronteiras, sem guerras e sem divisões. Uma reflexão profunda sobre como a humanidade poderia ser mais unida.",
                      Genre.Pop,
                      "Imagine there's no heaven / It's easy if you try...",
                      "Imagine (1971)",
                      new List<Artist> { johnWinston });

            StartMenuThread();
        }
        #endregion
        
        //Funções primárias
        #region Login and Thread Start Handling
        static void StartMenuThread()
        {
            Console.Clear();
            Task.Run(() => StartingMenu()).Wait();
        }
        static User LoginMenu()
        {
            Title("Login");
            Console.WriteLine("1. Entrar");
            Console.WriteLine("2. Registrar");
            Console.WriteLine("\nAperte qualquer botão para sair");

            var key = Console.ReadKey().Key;
            if (key == ConsoleKey.D1)
            {
                return HandleLogin();
            }
            else if (key == ConsoleKey.D2)
            {
                return HandleRegistration();
            }
            else
            {
                Environment.Exit(0);
                return null;
            }
        }
        #endregion

        #region Menus UI
        static void Title(string title)
        {
            var arrayOfTitle = title.Length;
            string decoration = "";
            for (var i = 0; i < arrayOfTitle; i++)
            {
                decoration += "_";
            }
            Console.WriteLine(decoration + "\n" + title + "\n" + decoration + "\n");
        }
        static void StartingMenu()
        {
            if (CurrentUser.Value == null)
            {
                CurrentUser.Value = LoginMenu();
            }
            Console.Clear();
            Title("App de Músicas" + " - Usuário: " + CurrentUser);
            Console.WriteLine("1. Pesquisar");
            Console.WriteLine("2. Compartilhar");
            Console.WriteLine("3. Tocar música");
            Console.WriteLine("4. Avaliação");
            Console.WriteLine("5. Sair da conta");
            Console.WriteLine("\nAperte qualquer outra tecla para fechar o programa.");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    SearchForMedia();
                    break;
                case ConsoleKey.D2:
                    ShareMedia();
                    break;
                case ConsoleKey.D3:
                    PlayMusic();
                    break;
                case ConsoleKey.D4:
                    RatingMedia();
                    break;
                case ConsoleKey.D5:
                    CurrentUser.Value = null;
                    StartMenuThread();
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }

        }
        #endregion

        #region Menu Opitions
        static void SearchForMedia()
        {
            bool continueSearching = true;

            while (continueSearching)
            {
                Console.Clear();
                Title("Pesquisar" + " - Usuário: " + CurrentUser);
                Console.WriteLine("1. Pesquisar por nome");
                Console.WriteLine("2. Pesquisar pelo tipo");
                Console.WriteLine("\nAperte qualquer outra tecla para voltar");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        HandleNameSearch();
                        break;
                    case ConsoleKey.D2:
                        HandleTypeSearch();
                        break;
                    default:
                        continueSearching = false;
                        break;
                }
            }
            StartingMenu();
        }
        static void ShareMedia()
        {
            Title("Compartilhar");
            Console.Clear();
            Console.WriteLine("Tipos: Artist, Playlist, Music, Album");

            var searchForType = Console.ReadLine();
            if (string.IsNullOrEmpty(searchForType)) StartingMenu();

            var searchResult = Search.SearchByType(searchForType);
            if (!searchResult.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhum resultado encontrado.");
                Console.ReadKey();
                StartingMenu();
            }
            Console.Clear();
            var mediaDictionary = DisplaySearchResults(searchResult);
            var mediaChoice = ReadChoice(mediaDictionary);
            if (mediaDictionary == null) StartingMenu();

            var media = mediaDictionary[mediaChoice.Value];
            if (media == null) StartingMenu();

            var userDictionary = DisplayUserList();
            if (userDictionary.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("Nenhum usuário encontrado");
                Console.ReadKey();
                StartingMenu();
            }

            var userChoice = ReadChoice(userDictionary);
            if (userChoice == null) ShareMedia();

            User receiver = userDictionary[userChoice.Value];
            new Share<object>(CurrentUser, media, receiver);
            Console.Clear();
            Console.WriteLine($"Você compartilhou a {media.GetType().Name} com {receiver.Name}");
            Console.ReadKey();
            StartingMenu();
        }
        static void PlayMusic()
        {
            Console.Clear();
            Title("Escolher uma música" + " - Usuário: " + CurrentUser);

            var musicDictionary = DisplaySearchResults(Search.SearchByType("Music"));
            Console.WriteLine("Digite o número da música ou pressione Enter para sair:");

            while (true)
            {
                var mediaChoice = ReadChoice(musicDictionary);
                if (mediaChoice == null) continue;

                Console.Clear();
                Console.WriteLine("Aperte enter para pausar/despausar\n");

                var music = musicDictionary[mediaChoice.Value];
                new MusicPlayer(CurrentUser, (Music)music).Play((Music)music);
                Thread.Sleep(2000);
                StartingMenu();
                break;
            }

        }
        static void RatingMedia()
        {
            Title("Avaliação");
            Console.Clear();
            Console.WriteLine("Tipos: Artist, Playlist, Music, Album");
            var searchForType = Console.ReadLine();
            if (string.IsNullOrEmpty(searchForType)) StartingMenu();

            var searchResult = Search.SearchByType(searchForType);
            if (!searchResult.Any())
            {
                Console.Clear();
                Console.WriteLine("Nenhum resultado encontrado.");
                Console.ReadKey();
            }
            Console.Clear();
            var mediaDictionary = DisplaySearchResults(searchResult);
            var mediaChoice = ReadChoice(mediaDictionary);
            if (mediaChoice == null) RatingMedia();

            RateMedia(mediaDictionary[mediaChoice.Value]);
        }
        #endregion

        //Funções secundárias
        #region Reading Input Data
        private static string ReadValidInputs()
        {
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.Clear();
                Console.WriteLine("Por favor digite um nome valido");
                Console.ReadKey();
            }
            return input;
        }
        private static int? ReadChoice<T>(Dictionary<int, T> dictionary)
        {
            var choice = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(choice) || !int.TryParse(choice, out int number) || !dictionary.ContainsKey(number))
            {
                Console.Clear();
                Console.WriteLine("Número inválido ou vazio.");
                Console.ReadKey();
                return null;
            }

            return int.Parse(choice);
        }
        private static int ReadRating()
        {
            int rating = 0; ;
            while (rating == 0)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int ratingValue) && ratingValue >= 1 && ratingValue <= 10)
                {
                    rating = ratingValue;
                }
                else
                {
                    Console.WriteLine("Entrada inválida! Por favor, digite um número inteiro de 1 a 10");
                }
            }
            return rating;
        }
        private static (string user, string password) GetUserCredencials()
        {
            Console.WriteLine("Usuário:");
            var user = Console.ReadLine() ?? "";
            Console.WriteLine("Senha:");
            var password = Console.ReadLine() ?? "";
            return (user, password);
        }
        #endregion

        #region Displaying and Manipulating Search Results
        private static Dictionary<int, object> DisplaySearchResults(IEnumerable<object> searchResult)
        {
            var mediaDictionary = new Dictionary<int, object>();
            int index = 0;

            Console.WriteLine("Resultados:\n");
            foreach (var search in searchResult)
            {
                index++;
                Console.WriteLine($"{index}. {search}");
                mediaDictionary.Add(index, search);
            }

            return mediaDictionary;
        }
        private static Dictionary<int, User> DisplayUserList()
        {
            int index = 0;
            var listOfUsers = User.Users.Keys.Where(p => p.Name != CurrentUser.Value?.Name).ToList();
            var userDictionary = new Dictionary<int, User>();

            Console.WriteLine("Escolha o usuário para compartilhar:\n");
            foreach (var user in listOfUsers)
            {
                index++;
                Console.WriteLine($"{index}. {user.Name}");
                userDictionary.Add(index, user);
            }

            return userDictionary;
        }
        private static void ShowSearchResult(object result)
        {
            if (result == null)
            {
                Console.WriteLine("Não encontrado");
            }
            else
            {
                Console.WriteLine($"Resultado: {result.ToString()} ({result.GetType().Name})");
            }
            Console.ReadKey();
        }
        private static void ShowSearchResults(IEnumerable<object> results)
        {
            if (results.Any())
            {
                Console.WriteLine("Resultados:\n");
                foreach (var search in results)
                {
                    Console.WriteLine(search.ToString());
                }
            }
            else
            {
                Console.WriteLine("Nenhum resultado encontrado");
            }
            Console.WriteLine("Aperte qualquer outra tecla para voltar");
            Console.ReadKey();
        }
        private static void HandleNameSearch()
        {
            Console.Clear();
            var searchForName = Console.ReadLine();

            if (string.IsNullOrEmpty(searchForName))
            {
                ShowInvalidInputMessage();
            }
            else
            {
                Console.Clear();
                var result = Search.SearchForName(searchForName);
                ShowSearchResult(result);
            }
        }
        private static void HandleTypeSearch()
        {
            Console.Clear();
            Console.WriteLine("Tipos: Artist, Playlist, Music, Album");
            var searchForType = Console.ReadLine();

            if (string.IsNullOrEmpty(searchForType))
            {
                ShowInvalidInputMessage();
            }
            else
            {
                Console.Clear();
                var result = Search.SearchByType(searchForType);
                ShowSearchResults(result);
            }
        }
        #endregion

        #region User Registration and Login
        private static User HandleRegistration()
        {
            bool registrationSuccessful = false;

            while (!registrationSuccessful)
            {
                Console.Clear();
                var (user, password) = GetUserCredencials();

                if (User.FindUser(user, password))
                {
                    Console.WriteLine("Usuário já cadastrado. Por favor tente com outro nome");
                    continue;
                }
                else
                {
                    Console.WriteLine("Registro concluído com sucesso");
                    return new User(user, password);
                }
            }
            return null; //Fallback
        }
        private static User HandleLogin()
        {
            bool userExists = false;

            while (!userExists)
            {
                Console.Clear();
                var (user, password) = GetUserCredencials();

                if (User.FindUser(user, password))
                {
                    Console.WriteLine("Login feito com sucesso!");
                    userExists = true;
                    return User.Users.FirstOrDefault(p => p.Value.Item1 == user && p.Value.Item2 == password).Key;
                }
                else
                {
                    Console.WriteLine("Usuário ou senha incorretos. Tente novamente");
                }
            }
            return null;
        }
        #endregion

        #region Media Classification and Evaluation
        private static void RateMedia(object media)
        {
            while (true)
            {
                Console.Clear();
                Title("Avaliação");
                Console.WriteLine($"De 1 a 10, qual a sua nota para: {media.ToString()}");

                int rating = ReadRating();
                if (rating == -1) continue;

                Console.WriteLine("Comentário:");
                var comment = Console.ReadLine();
                new Rating<object>(CurrentUser, media, rating, comment);

                Console.WriteLine("Obrigado pela avaliação!");
                Console.ReadKey();
                StartingMenu();
                break;
            }
        }
        #endregion

        #region Error/Informational Messages
        private static void ShowInvalidInputMessage()
        {
            Console.Clear();
            Console.WriteLine("Por favor digite um nome válido");
            Console.ReadKey();
        }
        #endregion
    }
}