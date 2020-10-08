using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using Hanselman.Models;
using Newtonsoft.Json;

namespace Hanselman.Services.Internal
{
    [Shared]
    [Export(typeof(ITweetStore))]
    public class TweetStore : ITweetStore
    {
        /// <summary>
        /// Save the specified tweets.
        /// this needs to be replaced with an abstract storage medium
        /// </summary>
        /// <param name="tweets">Tweets.</param>
        public void Save(IReadOnlyCollection<Tweet> tweets)
        {

            var FileManager = new Foundation.NSFileManager();
            var appGroupContainer = FileManager.GetContainerUrl("group.com.refactored.hanselman");

            if (appGroupContainer == null)
            {
                Console.WriteLine("You must go into apple developer console and create a new app group");

                return;
            }

            var path = Path.Combine(appGroupContainer.Path, "tweets.xml");
            Console.WriteLine("agcpath: " + path);

            var json = JsonConvert.SerializeObject(tweets);

            File.WriteAllText(path, json);

            /*
            var serializer = new XmlSerializer(typeof(List<Tweet>));
            using (var stream = File.Open(path, FileMode.CreateNew, FileAccess.ReadWrite))
            {
                serializer.Serialize(stream, tweets);
            }
            */
        }

        // List<Hanselman.Shared.Tweet> Load ();
    }
}

