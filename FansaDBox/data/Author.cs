using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansaDBox.data
{

    public class Author
    {
        [Index(0)]
        public int Id { get; set; }
        [Index(1)]
        public string Name { get; set; }
        [Index(2)]
        public int Notation { get; set; }
        [Ignore]
        public int NbrVolumes { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-{1}", Id, Name);
        }
    }
}
