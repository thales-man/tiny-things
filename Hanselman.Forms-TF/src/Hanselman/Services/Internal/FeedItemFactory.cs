using System;
using System.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Hanselman.Models;
using Tiny.Framework.Models;
using Tiny.Framework.Services;

namespace Hanselman.Services.Internal
{
    [Shared]
    [Export(typeof(ICreateFeedItems))]
    public sealed class FeedItemFactory :
        ICreateFeedItems
    {
        public IContentFeedItem Create(XElement fromElement, string icon, int id, XElement usingEnclosure = null)
        {
            var text = fromElement.Element("description")?.Value;
            var publishDate = fromElement.Element("pubDate")?.Value;

            var content = new FeedItem
            {
                ID = id,
                Title = fromElement.Element("title")?.Value,
                Caption = MakeCaption(text),
                Text = text,
                Category = fromElement.Element("category")?.Value,
                PublishDate = MakePublicationDate(publishDate),
                LinkCommand = fromElement.Element("link")?.Value,
                ContentPath = usingEnclosure?.Attribute("url").Value,
                ImagePath = MakeImagePath(text, icon),
            };

            return content;
        }

        internal string MakeCaption(string fromText)
        {
            // remove HTML tags
            var caption = Regex.Replace(fromText, "<[^>]*>", string.Empty);

            // remove multiple blank lines
            caption = Regex.Replace(caption, @"^\s*$\n", string.Empty, RegexOptions.Multiline);

            // consolidate
            caption = caption.Substring(0, caption.Length < 200 ? caption.Length : 200).Trim() + "...";

            return caption;
        }

        internal string MakePublicationDate(string fromText) =>
            DateTime.TryParse(fromText, out var time)
                ? $"{time.ToLocalTime():D}"
                : fromText;

        internal string MakeImagePath(string fromText, string orUseDefault)
        {
            var regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?.(?:jpg|bmp|gif|png)", RegexOptions.IgnoreCase);
            var matches = regx.Matches(fromText).OfType<Match>();

            var firstImage = matches.Any()
                ? matches.First().Value
                : orUseDefault;

            return firstImage;
        }
    }
}
