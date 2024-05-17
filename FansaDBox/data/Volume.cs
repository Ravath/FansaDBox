using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansaDBox.data
{
    public class Volume
    {
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public string Name { get; set; }
        [Index(4)]
        public string _author_to_csv { get; set; }
        [Ignore]
        public List<Author> Authors { get; } = new List<Author>();
        [Index(2)]
        public int Notation { get; set; } = -1;
        [Index(3)]
        public string Filepath { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-{1}", Id, Name);
        }
    }
}
