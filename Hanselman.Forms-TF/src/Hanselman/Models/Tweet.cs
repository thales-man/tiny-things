using System;
using Newtonsoft.Json;
using Tiny.Framework.Models;

namespace Hanselman.Models
{
    public class Tweet :
        ITweetFeedItem
    {
        public int ID { get; set; }

        public ulong StatusID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        //[JsonIgnore]
        public string PublishDate => CreatedAt.ToString("g");

        public string ImagePath { get; set; }

        //[JsonIgnore]
        public string TimesRetweeted =>
            CurrentUserRetweet == 0
                ? string.Empty
                : CurrentUserRetweet + " RT";

        public DateTime CreatedAt { get; set; }

        public ulong CurrentUserRetweet { get; set; }
    }
}

