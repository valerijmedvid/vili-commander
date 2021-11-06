using System;
using System.IO;
using ViliCommander.Services;
using ViliCommander.Components;

namespace ViliCommander
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            ExplorerComponent explorer = new ExplorerComponent();
            do
            {
                explorer.draw();

                keyInfo = Console.ReadKey(true);
                explorer.keyHandler(keyInfo);
            } while (keyInfo.Key != ConsoleKey.Escape);
        }
    }
}
