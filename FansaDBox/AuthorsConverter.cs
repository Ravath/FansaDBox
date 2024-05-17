using FansaDBox.data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FansaDBox
{
    [ValueConversion(typeof(List<Author>), typeof(String))]
    public class AuthorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<Author> authors = (List<Author>)value;
            return string.Join(", ", authors.Select(a => a.Name));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
