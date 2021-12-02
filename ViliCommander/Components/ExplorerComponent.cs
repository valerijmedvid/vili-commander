using System;
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
        int leftSelected = 0;
        List<ItemInfo> leftDir;


        string rightPath = @"C:\Users";
        int rightStartPosition = 0;
        int rightSelected = 0;
        List<ItemInfo> rightDir;


        int explorerHeight = 33;
        bool isLeftActive = true;
        public void draw()
        {
            Console.SetWindowSize(150, 41);
            Console.SetCursorPosition(0,0);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            this.drawHeader();
            this.drawBody();
            this.drawFooter();
            Console.ResetColor();
        }

        public void keyHandler(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (isLeftActive && this.leftSelected < this.leftDir.Count - 1)
                {
                    this.leftSelected++;
                    if (this.leftSelected > this.explorerHeight - 1)
                    {
                        this.leftStartPosition++;
                    }
                }
                else if (!isLeftActive && this.rightSelected < this.rightDir.Count - 1)
                {

                    this.rightSelected++;
                    if (this.rightSelected > this.explorerHeight - 1)
                    {
                        this.rightStartPosition++;
                    }
                }
            }

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                if (isLeftActive && this.leftSelected > 0)
                {
                    this.leftSelected--;
                    if (this.leftSelected > this.explorerHeight - this.leftStartPosition - 1)
                    {
                        this.leftStartPosition--;
                    }
                }
                else if (!isLeftActive && this.rightSelected > 0)
                {
                    this.rightSelected--;
                }
            }

            if (keyInfo.Key == ConsoleKey.Tab)
            {
                isLeftActive = isLeftActive ? false : true;
            }

            if (keyInfo.Key == ConsoleKey.Enter)
            {
                if (isLeftActive)
                {
                    if (this.leftDir[this.leftSelected].Type == ItemInfo.ItemType.Folder)
                    {
                        var temp = this.leftPath + @"\" + this.leftDir[this.leftSelected].Name;
                        this.leftPath = temp.Replace(@"\\", @"\");
                        this.leftSelected = 0;
                        this.leftStartPosition = 0;
                    }
                    else if (this.leftDir[this.leftSelected].Type == ItemInfo.ItemType.UpDir)
                    {
                        this.leftPath = @"C:\" + String.Join(@"\", this.leftPath.Replace(@"C:\", "").Split(@"\").SkipLast(1));
                        this.leftSelected = 0;
                        this.leftStartPosition = 0;
                    }

                }
                else
                {
                    if (this.rightDir[this.rightSelected].Type == ItemInfo.ItemType.Folder)
                    {
                        var temp = this.rightPath + @"\" + this.rightDir[this.rightSelected].Name;
                        this.rightPath = temp.Replace(@"\\", @"\");
                        this.rightSelected = 0;
                        this.rightStartPosition = 0;
                    }
                    else if (this.rightDir[this.rightSelected].Type == ItemInfo.ItemType.UpDir)
                    {
                        this.rightPath = @"C:\" + String.Join(@"\", this.rightPath.Replace(@"C:\", "").Split(@"\").SkipLast(1));
                        this.rightSelected = 0;
                        this.rightStartPosition = 0;
                    }
                }
            }
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
            this.leftDir = drs.getFolderContent(this.leftPath);
            this.rightDir = drs.getFolderContent(this.rightPath);


            for (int line = 0; line < explorerHeight; line++)
            {
                Console.Write("|");
                if (leftDir.Count > line)
                {
                    this.drawBodyLine(leftDir[line + this.leftStartPosition], line == leftSelected - this.leftStartPosition && this.isLeftActive);
                }
                else
                {
                    this.drawEmptyBodyLine();
                }
                Console.Write("|");
                if (rightDir.Count > line)
                {
                    this.drawBodyLine(rightDir[line + this.rightStartPosition], line == rightSelected - this.rightStartPosition && !this.isLeftActive);
                }
                else
                {
                    this.drawEmptyBodyLine();
                }
                Console.WriteLine();
            }
        }

        private void drawBodyLine(ItemInfo item, bool selected)
        {
            if (selected)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (item.Type == ItemInfo.ItemType.File)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }


            Console.Write(item.Type == ItemInfo.ItemType.File ? " " : "/");
            if (item.Name.Length > 32)
            {
                Console.Write(item.Name.Substring(0, 30) + "...");
            }
            else { Console.Write(item.Name.PadRight(33, ' ')); }


            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("|");
            Console.Write(item.Size.PadLeft(18, ' ') + "|");
            Console.Write(item.LastModifyDate.PadLeft(19, ' '));
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write('|');
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
            Console.WriteLine("├".PadRight(74, '─') + "┤");
            Console.Write("│");
            Console.Write(leftDir[this.leftSelected].Type == ItemInfo.ItemType.UpDir ? "UP--DIR".PadRight(73, ' ') : leftDir[this.leftSelected].Name.ToUpper().PadRight(73, ' '));
            Console.Write("││");
            Console.Write(rightDir[this.rightSelected].Type == ItemInfo.ItemType.UpDir ? "UP--DIR".PadRight(73, ' ') : rightDir[this.rightSelected].Name.ToUpper().PadRight(73, ' '));
            Console.WriteLine("│");
            Console.Write("└".PadRight(74, '─') + "┘");
            Console.WriteLine("└".PadRight(74, '─') + "┘");
        }

    }
}
