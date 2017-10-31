﻿using System;
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
            var importer = new Importer();
            var ids = Enumerable.Repeat(1, 100).ToArray();
            importer.Import(ids, (int i) =>
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(String.Format("Downloaded {0}/{1}", i, ids.Count()));
            }, (int i) =>
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine(String.Format("Serialized {0}/{1}", i, ids.Count()));
            });
            Console.ReadLine();
        }
    }
}