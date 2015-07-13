using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using RSSReader.Data.Models;

namespace RSSReader.Data
{
    public static class RSSDataManeger
    {
        public static void SaveChannels(IEnumerable<RSSChannel> channels,string fileName)
        {
            var document = new XDocument();;
            document.AddFirst(new XElement("channels"));
            foreach (var channel in channels)
            {
                var title = new XElement("title", channel.Title);
                var link = new XElement("link", channel.Link);
                document.Root.Add(new XElement("channel", title, link));
            }
            document.Save(fileName, SaveOptions.None);
        }

        public static IEnumerable<RSSChannel> GetChannels(string fileName)
        {
            if (!File.Exists(fileName)) 
                return new RSSChannel[] { };
            var document = XDocument.Load(fileName);
            var channels = from channel in document.Element("channels").Elements("channel")
                select new RSSChannel()
                {
                    Link = channel.Element("link").Value,
                    Title = channel.Element("title").Value
                };
            return channels;
        }

        public static IEnumerable<RSSItem> GetItemsFrom(string link)
        {
            var document = XDocument.Load(link);
            var items = from item in document
                .Element("rss")
                    .Element("channel")
                        .Elements("item")
                select new RSSItem()
                {
                    Title = item.Element("title").Value,
                    Description = item.Element("description").Value,
                    Link = item.Element("link").Value,
                    PublishDate = item.Element("pubDate").Value
                };
            return items;
        }

        public static bool IsValidChannel(string link)
        {
            bool isValid = true;
            try
            {
                var doc = XDocument.Load(link);
                var root = doc.Element("rss");
                var channels = root.Element("channel");
                var items = channels.Elements("item");
                return isValid;
            }
            catch
            {
                return false;
            }
        }
    }
}
