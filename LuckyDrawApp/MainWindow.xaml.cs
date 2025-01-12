using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LuckyDrawApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
   public MainWindow()
   {
      InitializeComponent();
      ImageBrush imageBrush = new ImageBrush();
      if (File.Exists(Helper.LUCKY_DRAW_BACKGROUND_IMAGE_PATH))
      {
         imageBrush.ImageSource = new BitmapImage(new Uri(Helper.LUCKY_DRAW_BACKGROUND_IMAGE_PATH, UriKind.RelativeOrAbsolute));
      }
      Background = imageBrush;
      DataContext = new MainWindowViewModel();
   }
}