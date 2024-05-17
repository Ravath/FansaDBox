using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace FansaDBox.data
{
    public class Database
    {
        public Config Config { get; } = new Config();
        public List<Author> Authors { get; } = new List<Author>();
        public List<Volume> Volumes { get; } = new List<Volume>();

        public Database()
        {

        }

        public void LoadData()
        {
            // Load Authors
            using (var reader = new StreamReader(Config.pdfFolder+Config.authorSave))
            using (var cswReader = new CsvReader(reader, Config.CsvConfig))
            {
                var records = cswReader.GetRecords<Author>();
                Authors.AddRange(records);
            }

            // Load Volumes
            using (var reader = new StreamReader(Config.pdfFolder + Config.volumeSave))
            using (var cswReader = new CsvReader(reader, Config.CsvConfig))
            {
                var records = cswReader.GetRecords<Volume>();
                Volumes.AddRange(records);

                // Bind Volume to Authors relation
                foreach (Volume volume in Volumes)
                {
                    foreach (var item in volume._author_to_csv.Split(','))
                    {
                        int authorId = int.Parse(item);
                        Author author = Authors.Single(a => a.Id == authorId);
                        volume.Authors.Add(author);

                        // Count nbr of volumes per Author
                        author.NbrVolumes++;
                    }
                    volume.Authors.Sort(new AuthorComparer());
                }
            }
        }

        public void SaveAuthors()
        {
            // Save Authors
            using (var writer = new StreamWriter(Config.pdfFolder + Config.authorSave))
            using (var csvWriter = new CsvWriter(writer, Config.CsvConfig))
            {
                csvWriter.WriteRecords(Authors);
            }
        }

        public void SaveVolumes()
        {
            // Save Volumes
            using (var writer = new StreamWriter(Config.pdfFolder + Config.volumeSave))
            using (var csvWriter = new CsvWriter(writer, Config.CsvConfig))
            {
                // update save data (authors)
                foreach (Volume item in Volumes)
                {
                    item._author_to_csv = string.Join(",", item.Authors.Select(a => a.Id));
                }

                // do the actual save
                csvWriter.WriteRecords(Volumes);
            }
        }

        public class AuthorComparer : IComparer<data.Author>
        {
            public int Compare(Author x, Author y)
            {
                int compareDate = x.Name.CompareTo(y.Name);
                if (compareDate == 0)
                {
                    return x.Id.CompareTo(y.Id);
                }
                return compareDate;
            }
        }
    }
}
