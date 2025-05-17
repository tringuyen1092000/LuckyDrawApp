using System.Globalization;
using System.Windows.Data;

namespace LuckyDrawApp
{
   public class TotalEmployeesConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is int count)
         {
            return $"{count} ON AIR";
         }
         return "0 ON AIR";
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}