using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class Search
    {
        public string Name { get; set; }
        protected string Type { get; }

        private static Dictionary<string, Search> library = new Dictionary<string, Search>();

        public Search(string name, string type)
        {
            Name = name;
            Type = type;

            if (!library.ContainsKey(name)) 
            {
                library[name] = this;
            }
        }

        public static Search? SearchForName(string name) 
        {
            Search? closestSearch = null;
            int smallestDistance = int.MaxValue;

            foreach (var key in library.Keys)
            {
                int distance = LevenshteinDistance(name.ToLower(), key.ToLower());

                if (distance < smallestDistance) 
                {
                    smallestDistance = distance;
                    closestSearch = library[key];
                }
            }
            return closestSearch;
        }
        public static List<Search> SearchByType(string type) 
        {
            return library.Values.Where(m => m.Type == type).ToList();
        }

        private static int LevenshteinDistance(string str1, string str2)
        {
            int n = str1.Length;
            int m = str2.Length;

            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for(int j = 1; j <= m; j++)
                {
                    int cost = (str1[i - 1] == str2[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            return d[n,m];
        }

        public static implicit operator string(Search v)
        {
            throw new NotImplementedException();
        }
    }
}
