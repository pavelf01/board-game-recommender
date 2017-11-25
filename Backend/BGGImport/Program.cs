using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BGGImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new BL.Bootstrap.DI());

            var recommenderTester = new RecommenderTester(container);
            //var importer = new Importer(container);
            //importer.Import(ReadIds().Skip(344).Take(2).ToArray(), (string message) =>
            //{
            //    Console.WriteLine(message);
            //});
            Console.ReadLine();
        }

        static IEnumerable<int> ReadIds()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"ids.txt");
            string[] ids = File.ReadAllText(path).Split(',');
            return ids.Select(x => Int32.Parse(x.Replace("\r\n","")));
        }
    }
}
