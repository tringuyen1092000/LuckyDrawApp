using System.Globalization;
using System.Windows.Data;

namespace LuckyDrawApp
{
   public class PrizeNameConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value is int availablePrize)
         {
            return availablePrize switch
            {
               3 => "FIRST PRIZE",
               8 => "SECOND PRIZE",
               10 => "THIRD PRIZE",
               15 => "FOURTH PRIZE",
               60 => "CONSOLATION PRIZE",
               _ => string.Empty
            };
         }
         return string.Empty;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }
}
