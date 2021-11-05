using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ViliCommander
{
    public class DirectoryReaderService
    {
        string path;

        public object ByteSize { get; private set; }

        public DirectoryReaderService(string path = @"C:\")
        {
            this.path = path;
        }


        public List<string[]> getFoldersFromPath()
        {
            DirectoryInfo di = new DirectoryInfo(this.path);
            DirectoryInfo[] dirs = di.GetDirectories();
            List<string[]> result = new List<string[]>();
            foreach (DirectoryInfo dir in dirs)
            {
                string dirSize = formatFileSize(getDirSize(dir)).ToString();
                string dirLastAccessTime = dir.LastAccessTime.ToShortDateString();

                result.Add(new string[] { dir.Name, dirSize, dirLastAccessTime });
            }

            return result;
        }


        public List<string[]> getFilesFromPath()
        {
            string[] files = Directory.GetFiles(this.path);
            List<string[]> result = new List<string[]>();
            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                string fileName = file.Replace(@"C:\", "").Split(@"\").Last();
                string fileSize = formatFileSize(fileInfo.Length).ToString();
                string fileLastModify = fileInfo.LastWriteTime.ToShortDateString();

                result.Add(new string[] { fileName, fileSize, fileLastModify });
            }

            return result;
        }


        public static long getDirSize(DirectoryInfo d)
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

        public static string formatFileSize(long bytes)
        {
            var unit = 1024;
            if (bytes < unit) { return $"{bytes} B"; }

            var exp = (int)(Math.Log(bytes) / Math.Log(unit));
            return $"{bytes / Math.Pow(unit, exp):F1} {("KMGTPE")[exp - 1]}B";
        }
    }
}
