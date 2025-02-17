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
      if (File.Exists(Helper.BACKGROUND_IMAGE_PATH))
      {
         imageBrush.ImageSource = new BitmapImage(new Uri(Helper.BACKGROUND_IMAGE_PATH, UriKind.RelativeOrAbsolute));
         imageBrush.Stretch = Stretch.None;
      }
      Background = imageBrush;
      DataContext = new MainWindowViewModel();
   }
   private async void Window_KeyDown(object sender, KeyEventArgs e)
   {
      MainWindowViewModel? viewModel = DataContext as MainWindowViewModel;
      if (viewModel != null)
      {
         switch (e.Key)
         {
            case Key.NumPad1 or Key.D1:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 1;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 3;
                  viewModel.ColumnAmount = 1;
                  viewModel.LuckyEmployees.Clear();
               }
               break;
            case Key.NumPad2 or Key.D2:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 1;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 8;
                  viewModel.ColumnAmount = 1;
                  viewModel.LuckyEmployees.Clear();
               }
               break;
            case Key.NumPad3 or Key.D3:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 2;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 10;
                  viewModel.ColumnAmount = 2;
                  viewModel.LuckyEmployees.Clear();
               }
               break;
            case Key.NumPad4 or Key.D4:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 5;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 15;
                  viewModel.ColumnAmount = 1;
                  viewModel.LuckyEmployees.Clear();
               }
               break;
            case Key.NumPad5 or Key.D5:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 20;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 60;
                  viewModel.ColumnAmount = 2;
                  viewModel.LuckyEmployees.Clear();
               }
               break;
            case Key.Space:
               if (!viewModel.IsSpinning && viewModel.PrizeTaken < viewModel.AvailablePrize)
               {
                  viewModel.MediaPlayer.Stop();
                  viewModel.StartSpinning();
               }
               break;
            case Key.Enter:
               if (viewModel.IsSpinning)
               {
                  await Task.Delay(2000);
                  viewModel.IsSpinning = false;
                  viewModel.MediaPlayer.Stop();
                  viewModel.MediaPlayer.Open(new Uri(Helper.AFTER_SPIN_EFFECT_PATH, UriKind.RelativeOrAbsolute));
                  viewModel.MediaPlayer.Play();
               }
               break;
            default:
               break;
         }
      }
   }
}