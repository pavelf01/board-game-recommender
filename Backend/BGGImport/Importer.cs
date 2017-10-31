using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Xml2CSharp;

namespace BGGImport
{
    class Importer
    {
        const string API_URI = "https://www.boardgamegeek.com/xmlapi2/thing?id={0}&type=boardgame";
        private XmlSerializer serializer;

        private ObservableCollection<string> downloadedXmls;
        public Importer()
        {
            this.serializer = new XmlSerializer(typeof(Items));
            this.downloadedXmls = new ObservableCollection<string>();
        }

        public void Import(int[] Ids, Action<int> DownloadedProgressChanged, Action<int> EntityPersistedProgressChanged)
        {
            new Task(() =>
            {
                this.DownloadXmls(Ids, DownloadedProgressChanged);
            }).Start();
            new Task(() =>
            {
                this.WatchDownloadChanges(EntityPersistedProgressChanged);
            }).Start();
        }

        private void DownloadXmls(int[] Ids, Action<int> DownloadedProgressChanged)
        {
            using (var wc = new WebClient())
            {
                int i = 1;
                foreach (var id in Ids)
                {
                    try
                    {
                        var a = String.Format(API_URI, id);
                        var b = wc.DownloadString(a);
                        this.downloadedXmls.Add(b);
                        DownloadedProgressChanged(i++);
                        if (i % 15 == 0) Thread.Sleep(4000);
                    }
                    catch (Exception exc)
                    {

                    }
                }
            }
        }

        private void WatchDownloadChanges(Action<int> EntityPersistedProgressChanged)
        {
            this.downloadedXmls.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems.Count == 0) return;
                foreach (var item in args.NewItems)
                {
                    this.ConvertAndPersist(this.Deserialize(item as string).Item);
                    EntityPersistedProgressChanged(this.downloadedXmls.Count);
                }
            };
        }

        private void ConvertAndPersist(Item item)
        {
            var game = this.MapToEntity(item);
            //persist
        }

        private Items Deserialize(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                return (Items)this.serializer.Deserialize(new StringReader(xml));
            }
        }

        private BoardGame MapToEntity(Item item)
        {
            return new BoardGame
            {
                Id = Int32.Parse(item.Id),
                Name = "dummy",
                ThumbnailImageURL = item.Thumbnail,
                Description = item.Description,
                Published = new DateTime(Int32.Parse(item.Yearpublished.Value), 1, 1),
                MinimalPlayers = Int32.Parse(item.Maxplayers.Value ?? item.Minplayers.Value),
                MaximalPlayers = Int32.Parse(item.Minplayers.Value ?? item.Maxplayers.Value),
                MinimalPlayingTime = Int32.Parse(item.Minplaytime.Value ?? (item.Playingtime.Value ?? item.Maxplaytime.Value)),
                MaximalPlayingTime = Int32.Parse(item.Maxplaytime.Value ?? (item.Playingtime.Value ?? item.Minplaytime.Value)),
                MinimalPlayerAge = Int32.Parse(item.Minage.Value),
                Categories = this.MapLink<BoardGameGategory>(
                    item.Link.Where(x => x.Type == "boardgamecategory"), (i) => new BoardGameGategory { Name = i.Value }).ToList(),
                Artists = this.MapLink<BoardGameArtist>(
                    item.Link.Where(x => x.Type == "boardgameartist"), (i) => new BoardGameArtist { Name = i.Value }).ToList(),
                Designers = this.MapLink<BoardGameDeisgner>(
                    item.Link.Where(x => x.Type == "boardgamedesigner"), (i) => new BoardGameDeisgner { Name = i.Value }).ToList(),
                Publishers = this.MapLink<BoardGamePublisher>(
                    item.Link.Where(x => x.Type == "boardgamepublisher"), (i) => new BoardGamePublisher { Name = i.Value }).ToList(),
                Ratings = this.MapComments(item.Comments).ToList()
            };
        }

        private IEnumerable<T> MapLink<T>(IEnumerable<Link> Links, Func<Link, T> Factory/*,service*/)
        {
            foreach (var link in Links)
            {
                //var entity = entity service get 
                //if entity = null
                var entity = Factory(link);
                //persist entity
                yield return entity;
            }
        }

        private IEnumerable<UserRating> MapComments(Comments Comments)
        {
            foreach (var comment in Comments.Comment)
            {
                //var user = entity service get comment.Username
                //if user = null
                var user = new User
                {
                    UserName = comment.Username
                };
                //persist user
                var entity = new UserRating
                {
                    Comment = comment.Value,
                    Rating = Int32.Parse(comment.Rating),
                    User = user
                };
                //persist entity
                yield return entity;
            }
        }
    }
}
