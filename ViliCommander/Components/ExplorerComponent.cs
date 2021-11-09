﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViliCommander.Services;

namespace ViliCommander.Components
{
    public class ExplorerComponent
    {
        string leftPath = @"C:\";
        int leftStartPosition = 0;


        string rightPath = @"C:\Users";
        int rightStartPosition = 0;


        int explorerHeight = 20;
        public void draw()
        {
            Console.SetWindowSize(151, 40);
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            this.drawHeader();
            this.drawBody();
            this.drawFooter();
            Console.ResetColor();
        }

        public void keyHandler(ConsoleKeyInfo keyInfo)
        {
            Console.WriteLine(keyInfo.Key);
        }

        private void drawHeader()
        {

            Console.WriteLine();
            Console.Write("┌─<─" + leftPath.PadRight(67, '─') + "─>─┐");
            Console.Write("┌─<─" + rightPath.PadRight(67, '─') + "─>─┐");
            Console.WriteLine();
        }

        private void drawBody()
        {
            this.drawTitles();
            this.drawTitles();
            Console.WriteLine();

            DirectoryReaderService drs = new DirectoryReaderService();
            List<ItemInfo> leftDir = drs.getFolderContent(this.leftPath);
            List<ItemInfo> rightDir = drs.getFolderContent(this.rightPath);


            for (int line = 0; line < explorerHeight; line++)
            {
                Console.Write("|");
                if (leftDir.Count > line)
                {
                    this.drawBodyLine(leftDir[line + this.leftStartPosition]);
                }
                else
                {
                    this.drawEmptyBodyLine();
                }
                Console.Write("|");
                if (rightDir.Count > line)
                {
                    this.drawBodyLine(rightDir[line + this.rightStartPosition]);
                }
                else
                {
                    this.drawEmptyBodyLine();
                }
                Console.WriteLine();

            }



        }

        private void drawBodyLine(ItemInfo item)
        {

            if (item.Type == ItemInfo.ItemType.File)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" " + item.Name.PadRight(33, ' '));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("|");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("/" + item.Name.PadRight(33, ' ') + "|");
            }

            Console.Write(item.Size.PadLeft(18, ' ') + "|");
            Console.Write(item.LastModifyDate.PadLeft(19, ' ') + "|");

        }

        private void drawEmptyBodyLine()
        {
            Console.Write("".PadRight(34, ' ') + "| ");
            Console.Write("".PadLeft(17, ' ') + "| ");
            Console.Write("".PadLeft(18, ' ') + "|");
        }

        private void drawTitles()
        {
            Console.Write("│");
            this.writeHeader("Name", 15);
            Console.Write("│");
            this.writeHeader("Size", 7);
            Console.Write("│");
            this.writeHeader("Modify time", 4);
            Console.Write("│");

        }

        private void writeHeader(string text, int padSize)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text.PadLeft(padSize + text.Length, ' ') + " ".PadRight(padSize, ' '));
            Console.ForegroundColor = ConsoleColor.White;

        }

        private void drawFooter()
        {
            Console.Write("├".PadRight(74, '─') + "┤");
            Console.Write("├".PadRight(74, '─') + "┤");
            Console.WriteLine();
        }

    }
}
