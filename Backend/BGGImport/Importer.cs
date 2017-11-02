using BL.Services;
using Castle.Windsor;
using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xml2CSharp;

namespace BGGImport
{
    class Importer
    {
        const string API_URI = "https://www.boardgamegeek.com/xmlapi2/thing?id={0}&type=boardgame&stats=1&ratingcomments=1";
        private XmlSerializer serializer;

        private BoardGamesService boardGameService;
        private UsersService userService;
        private UserRatingsService userRatingsService;

        private IWindsorContainer Container;

        private ObservableCollection<string> downloadedXmls;
        public Importer(IWindsorContainer Container)
        {
            this.boardGameService = Container.Resolve<BoardGamesService>();
            this.userRatingsService = Container.Resolve<UserRatingsService>();

            this.Container = Container;

            this.userService = Container.Resolve<UsersService>();
            this.serializer = new XmlSerializer(typeof(Items));
            this.downloadedXmls = new ObservableCollection<string>();
        }

        public void Import(int[] Ids, Action<int> DownloadedProgressChanged, Action<int> EntityPersistedProgressChanged)
        {
            //this.WatchDownloadChanges(EntityPersistedProgressChanged);
            //this.DownloadXmls(Ids, DownloadedProgressChanged);
            //foreach (var item in this.downloadedXmls)
            //{
            //    this.ConvertAndPersist(this.Deserialize(item as string).Item);
            //    EntityPersistedProgressChanged(this.downloadedXmls.Count);
            //}
            new Task(() =>
            {
                this.DownloadXmls(Ids, DownloadedProgressChanged);
            }).Start();
            this.WatchDownloadChanges(EntityPersistedProgressChanged);
        }

        private void DownloadXmls(int[] Ids, Action<int> DownloadedProgressChanged)
        {
            using (var wc = new WebClient())
            {
                int i = 0;
                while (i < Ids.Length)
                {
                    try
                    {
                        var xml = wc.DownloadString(String.Format(API_URI, Ids[i]));
                        this.downloadedXmls.Add(xml);
                        DownloadedProgressChanged(i++);
                    }
                    catch (Exception exc)
                    {
                        Thread.Sleep(3000);
                    }
                }
            }
        }

        private void WatchDownloadChanges(Action<int> EntityPersistedProgressChanged)
        {
            this.downloadedXmls.CollectionChanged += (sender, args) =>
            {
                if (args.NewItems.Count == 0) return;
                try
                {
                    foreach (var item in args.NewItems)
                    {
                        this.ConvertAndPersist(this.Deserialize(item as string).Item);
                        EntityPersistedProgressChanged(this.downloadedXmls.Count);
                    }
                }
                catch (Exception e)
                {

                }
            };
        }

        private void ConvertAndPersist(Item item)
        {
            var game = this.MapToEntity(item);
            this.boardGameService.Create(game);
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
            var boardGame = this.boardGameService.GetByBGGIdentifier(Int32.Parse(item.Id)) ?? new BoardGame();
            boardGame.BGGId = Int32.Parse(item.Id);
            boardGame.Name = item.Name.Where(x => x.Type == "primary").FirstOrDefault().Value;
            boardGame.ThumbnailImageURL = item.Thumbnail;
            boardGame.Description = item.Description;
            boardGame.Published = new DateTime(Int32.Parse(item.Yearpublished.Value), 1, 1);
            boardGame.MinimalPlayers = Int32.Parse(item.Minplayers.Value ?? item.Maxplayers.Value);
            boardGame.MaximalPlayers = Int32.Parse(item.Maxplayers.Value ?? item.Minplayers.Value);
            boardGame.MinimalPlayingTime = Int32.Parse(item.Minplaytime.Value ?? (item.Playingtime.Value ?? item.Maxplaytime.Value));
            boardGame.MaximalPlayingTime = Int32.Parse(item.Maxplaytime.Value ?? (item.Playingtime.Value ?? item.Minplaytime.Value));
            boardGame.MinimalPlayerAge = Int32.Parse(item.Minage.Value);
            boardGame.Categories = this.MapLink<BoardGameCategory>(
                                item.Link.Where(x => x.Type == "boardgamecategory"), (i) => new BoardGameCategory { Name = i.Value }, Container.Resolve<BoardGameCategoriesService>()).ToList();
            boardGame.Artists = this.MapLink<BoardGameArtist>(
                                item.Link.Where(x => x.Type == "boardgameartist"), (i) => new BoardGameArtist { Name = i.Value }, Container.Resolve<BoardGameArtistsService>()).ToList();
            boardGame.Designers = this.MapLink<BoardGameDesigner>(
                                item.Link.Where(x => x.Type == "boardgamedesigner"), (i) => new BoardGameDesigner { Name = i.Value }, Container.Resolve<BoardGameDesignersService>()).ToList();
            boardGame.Publishers = this.MapLink<BoardGamePublisher>(
                                item.Link.Where(x => x.Type == "boardgamepublisher"), (i) => new BoardGamePublisher { Name = i.Value }, Container.Resolve<BoardGamePublishersService>()).ToList();
            boardGame.Ratings = this.MapComments(item.Comments).ToList();
            return boardGame;
        }

        private IEnumerable<T> MapLink<T>(IEnumerable<Link> Links, Func<Link, T> Factory, BGGItemService<T, int, string> service)
        {
            foreach (var link in Links)
            {
                if(link.Value == "Economic")
                {

                }
                var entity = service.GetByBGGIdentifier(link.Value);
                yield return entity == null ? Factory(link) : entity;
            }
        }

        private IEnumerable<UserRating> MapComments(Comments Comments)
        {
            var comments = new List<UserRating>();
            foreach (var comment in Comments.Comment)
            {
                if (comments.Count(x => x.User.UserName == comment.Username) > 0) continue;
                comments.Add(new UserRating
                {
                    Comment = comment.Value,
                    Rating = float.Parse(comment.Rating, CultureInfo.InvariantCulture.NumberFormat),
                    User = userService.GetByBGGIdentifier(comment.Username) ?? new User
                    {
                        UserName = comment.Username
                    }
                });
            }
            return comments;
        }
    }
}