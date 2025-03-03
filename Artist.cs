using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class Artist : Search
    {
        public string? Surname { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }

        public Artist(string name, string surname, int age, DateTime birthday) : base(name,"Artist")
        { 
            Surname = surname;
            Age = age;
            Birthday = birthday.Date;
        }

        public override string ToString()
        {
            return Name;
        }

        public static implicit operator Artist(List<Artist> v)
        {
            throw new NotImplementedException();
        }
    }
}
