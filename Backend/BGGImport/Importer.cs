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

        public void Import(int[] ids, Action<int> downloaded,Action<int> serialized)
        {
            (new Task(() =>
            {
                using (var wc = new WebClient())
                {
                    int i = 1;
                    foreach (var id in ids)
                    {
                        try
                        {
                            var a = String.Format(API_URI, id);
                            var b = wc.DownloadString(a);
                            this.downloadedXmls.Add(b);
                            downloaded(i++);
                            if (i % 15 == 0) Thread.Sleep(4000);
                        }
                        catch(Exception exc)
                        {

                        }
                    }
                }
            })).Start();
            (new Task(() =>
            {
                var i = 1;
                this.downloadedXmls.CollectionChanged += (sender, args) =>
                {
                    if (args.NewItems.Count == 0) return;
                    foreach (var item in args.NewItems)
                    {
                        this.Deserialize(item as string);
                        serialized(i++);
                    }
                };
            })).Start();
        }

        private Items Deserialize(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                return (Items)this.serializer.Deserialize(new StringReader(xml));
            }
        }

        private void MapToEntity(Items item)
        {

        }
    }

}
