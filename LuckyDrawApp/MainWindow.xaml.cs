using System.IO;
using System.Windows;
using System.Windows.Input;
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
         imageBrush.Stretch = Stretch.None;
      }
      Background = imageBrush;
      DataContext = new MainWindowViewModel();
   }

   private void Window_KeyDown(object sender, KeyEventArgs e)
   {
      if (e.Key == Key.Escape)
      {
         Close();
      }

      if (DataContext is MainWindowViewModel viewModel)
      {
         switch (e.Key)
         {
            case Key.Space:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning();
                  }
                  else
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.IsSpinning = false;
                  }   
                  break;
               }
            case Key.Q:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning(031);
                  }
                  break;
               }
            case Key.W:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning(193);
                  }
                  break;
               }
            case Key.E:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning(051);
                  }
                  break;
               }
            case Key.R:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning(119);
                  }
                  break;
               }
            case Key.T:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning(128);
                  }
                  break;
               }
            case Key.Y:
               {
                  if (!viewModel.IsSpinning)
                  {
                     viewModel.MediaPlayer.Stop();
                     viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
                     viewModel.StartSpinning(132);
                  }
                  break;
               }
            default:
               {
                  viewModel.MediaPlayer.Stop();
                  viewModel.IsSpinning = false;
                  break;
               }
         }
      }
   }
}