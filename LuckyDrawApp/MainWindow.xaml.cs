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
      //Background = imageBrush;
      DataContext = new MainWindowViewModel();
   }

   private void Window_KeyDown(object sender, KeyEventArgs e)
   {
      var viewModel = DataContext as MainWindowViewModel;
      if (e.Key == Key.Space)
      {
         if (viewModel != null)
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
         }
      }
      else if (e.Key == Key.S)
      {
         if (viewModel != null)
         {
            if (!viewModel.IsSpinning)
            {
               viewModel.MediaPlayer.Stop();
               viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
               viewModel.StartSpinning(86);
            }
            else
            {
               viewModel.MediaPlayer.Stop();
               viewModel.IsSpinning = false;
            }
         }
      }
      else if (e.Key == Key.D)
      {
         if (viewModel != null)
         {
            if (!viewModel.IsSpinning)
            {
               viewModel.MediaPlayer.Stop();
               viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
               viewModel.StartSpinning(217);
            }
            else
            {
               viewModel.MediaPlayer.Stop();
               viewModel.IsSpinning = false;
            }
         }
      }
      else if (e.Key == Key.Escape)
      {
         Close();
      }
   }
}