using ComputerCoursesSystem.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ComputerCoursesSystem.UI.Convertors
{
    public class ImagePathConverter : IValueConverter
    {
        Dictionary<TeachingStyle, BitmapImage> cache = new Dictionary<TeachingStyle, BitmapImage>();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var teachingStyle = (TeachingStyle)value;
            if(!cache.ContainsKey(teachingStyle))
            {
                var uri = new Uri($"../Images/{parameter}/{teachingStyle}.png", UriKind.Relative);
                cache.Add(teachingStyle, new BitmapImage(uri));
            }
            
            return cache[teachingStyle];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
