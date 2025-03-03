using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraConsoleApp
{
    internal class Share<T>
    {
        private User Sender { get; set; }
        private T SharedMedia { get; set; }
        private User Recipient { get; set; }

        public static Dictionary<User, Tuple<T,User>> SharedHistory = new Dictionary<User, Tuple<T,User>>();
        public Share(User sender, T sharedMedia, User recipient) 
        {
            Sender = sender;
            SharedMedia = sharedMedia;
            Recipient = recipient;

            SharedHistory.Add(sender, new Tuple<T,User>(sharedMedia,recipient));
            recipient.AddReceivedItems(sharedMedia);
        }
    }
}
