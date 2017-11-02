using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BGGImport
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new BL.Bootstrap.DI());
            var importer = new Importer(container);
            importer.Import(Enumerable.Range(1, 100).ToArray(), (int i) =>
               {
                   Console.SetCursorPosition(0, 0);
                   Console.WriteLine(String.Format("Downloaded {0}/{1}", i, 100));
               }, (int i) =>
               {
                   Console.SetCursorPosition(0, 1);
                   Console.WriteLine(String.Format("Serialized {0}/{1}", i, 100));
               });
            Console.ReadLine();
        }
    }
}
