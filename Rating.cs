using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class Rating<T>
    {
        private User User;
        public int RatingValue { get; }
        public T Media { get; set; }
        public string? Comment { get; set; }

        public Rating(User user, T media, int rating, string? comment)
        {
            if (rating < 1 || rating > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(rating), "A nota deve estar entre 1 a 10");
            }
            User = user;
            Media = media;
            RatingValue = rating;
            Comment = comment;
        }
    }
}
