using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ViliCommander.Services
{
    public class DirectoryReaderService
    {

        public List<ItemInfo> getFolderContent(string path)
        {
            List<ItemInfo> dirs = getFoldersFromPath(path);
            List<ItemInfo> files = getFilesFromPath(path);
            return dirs.Concat(files).ToList();
        }

        private List<ItemInfo> getFoldersFromPath(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            DirectoryInfo[] dirs = di.GetDirectories();
            var result = new List<ItemInfo>();

            if (path.ToLower() != @"c:\")
            {
                result.Add(new ItemInfo("..", "UP--DIR", "", ItemInfo.ItemType.UpDir));
            }
            foreach (DirectoryInfo dir in dirs)
            {
                //string dirSize = formatFileSize(getDirSize(dir)).ToString();
                string dirSize = "0";
                string dirLastAccessTime = dir.LastAccessTime.ToShortDateString();

                result.Add(new ItemInfo(dir.Name, dirSize, dirLastAccessTime, ItemInfo.ItemType.Folder));
            }

            return result;
        }


        private List<ItemInfo> getFilesFromPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            var result = new List<ItemInfo>();
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                string fileName = file.Replace(@"C:\", "").Split(@"\").Last();
                string fileSize = formatFileSize(fileInfo.Length).ToString();
                string fileLastModify = fileInfo.LastWriteTime.ToShortDateString();

                result.Add(new ItemInfo(fileName, fileSize, fileLastModify, ItemInfo.ItemType.File));
            }

            return result;
        }


        private static long getDirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis;
            try
            {
                fis = d.GetFiles();
            }
            catch (UnauthorizedAccessException)
            {
                return 0;
            }

            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += getDirSize(di);
            }

            return size;
        }

        private static string formatFileSize(long bytes)
        {
            var unit = 1024;
            if (bytes < unit) { return $"{bytes} B"; }

            var exp = (int)(Math.Log(bytes) / Math.Log(unit));

            return $"{bytes / Math.Pow(unit, exp):F1} {("KMGTPE")[exp - 1]}B";
        }
    }
}
