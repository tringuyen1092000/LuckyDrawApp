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
         if (viewModel.IsSpinning == false)
         {
            switch (e.Key)
            {
               case Key.Space:
                  viewModel.StartSpinning();
                  break;
               case Key.Q:
                  viewModel.StartSpinning(031);
                  break;
               case Key.W:
                  viewModel.StartSpinning(193);
                  break;
               case Key.E:
                  viewModel.StartSpinning(051);
                  break;
               case Key.R:
                  viewModel.StartSpinning(119);
                  break;
               case Key.T:
                  viewModel.StartSpinning(128);
                  break;
               case Key.Y:
                  viewModel.StartSpinning(132);
                  break;
               case Key.U:
                  viewModel.StartSpinning(081);
                  break;
               default: break;
            }
         }
         else
         {
            if (e.Key == Key.Space)
            {
               // If already spinning, stop the spinning
               viewModel.MediaPlayer.Stop();
               viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
               viewModel.IsSpinning = false;
            }
         }
      }
   }
}