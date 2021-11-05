using System;
using System.IO;

namespace ViliCommander
{
    internal class Program
    {
        static void Main(string[] args)


        {

            DirectoryReaderService drs = new DirectoryReaderService(@"C:\");
            foreach (string[] item in drs.getFoldersFromPath())
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(item[0].PadRight(30, ' ') + "| ");
                Console.Write(item[1].PadLeft(8, ' ') + "| ");
                Console.WriteLine(item[2].PadLeft(10, ' ') + "| ");

            }
            foreach (string[] item in drs.getFilesFromPath())
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(item[0].PadRight(30, ' ') + "| ");
                Console.Write(item[1].PadLeft(8, ' ') + "| ");
                Console.WriteLine(item[2].PadLeft(10, ' ') + "| ");

            }
            Console.ReadKey();
        }
    }
}
