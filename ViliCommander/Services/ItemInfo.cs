using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViliCommander.Services
{


    public class ItemInfo
    {
        public enum ItemType
        {
            Folder,
            File,
            UpDir
        }

        public string Name { get; set; }
        public string Size { get; set; }
        public string LastModifyDate { get; set; }
        public ItemType Type { get; set; }


        public ItemInfo(string name, string size, string lastModifyDate, ItemType type)
        {
            this.Name = name;
            this.Size = size;
            this.LastModifyDate = lastModifyDate;
            this.Type = type;
        }

    }
}
