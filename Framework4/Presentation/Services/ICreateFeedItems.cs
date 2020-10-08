using System.Xml.Linq;
using Tiny.Framework.Models;

namespace Tiny.Framework.Services
{
    public interface ICreateFeedItems
    {
        IContentFeedItem Create(XElement fromElement, string icon, int id, XElement usingEnclosure = null);
    }
}