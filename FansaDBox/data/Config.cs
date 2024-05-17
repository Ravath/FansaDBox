using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FansaDBox.data
{
    public class Config
    {
        public string pdfFolder = "D:\\Fansadox\\";
        public string authorSave = "authors.csv";
        public string volumeSave = "volumes.csv";

        public string ressourceFolder = "C:\\Users\\Ehlion\\source\\repos\\FileManager_Python\\ressources\\";
        public string ratePointImage = "gold_star.png";
        public string rateDownImage = "grey_star.png";
        public string arrowUpImage = "arrow-up_small.png";


        public CsvConfiguration CsvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
            Delimiter = ";",
            HasHeaderRecord = false
        };
    }
}
