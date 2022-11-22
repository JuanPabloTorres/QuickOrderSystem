using System;
using System.IO;
using Xamarin.Forms;

namespace QuickOrderApp.Utilities.Converters
{
    public class BytesToImg : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ImageSource retSource;

            byte[] imageAsBytes = (byte[]) value;

            retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

            return retSource;
        }

        public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}