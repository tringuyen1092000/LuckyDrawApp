using System.Media;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;

namespace LuckyDrawApp
{
   public class MainWindowViewModel : INotifyPropertyChanged
   {
      private readonly Random _random = new Random();

      private readonly SoundPlayer _soundPlayer = new SoundPlayer("Sounds/SpinningEffect.wav");
      private bool _isSpinning;
      private string _firstDigit;
      private string _midDigit;
      private string _lastDigit;

      private readonly ICommand _startCommand;

      public MainWindowViewModel()
      {
         _firstDigit = "0";
         _midDigit = "0";
         _lastDigit = "0";
         _isSpinning = false;
         _startCommand = new DelegateCommand(StartSpinning);
         _soundPlayer.Load();
      }

      public bool IsSpinning
      {
         get => _isSpinning;
         set
         {
            _isSpinning = value;
            OnPropertyChanged(nameof(IsSpinning));
         }
      }
      public string FirstDigit
      {
         get => _firstDigit;
         set
         {
            _firstDigit = value;
            OnPropertyChanged(nameof(FirstDigit));
         }
      }
      public string MidDigit
      {
         get => _midDigit;
         set
         {
            _midDigit = value;
            OnPropertyChanged(nameof(MidDigit));
         }
      }
      public string LastDigit
      {
         get => _lastDigit;
         set
         {
            _lastDigit = value;
            OnPropertyChanged(nameof(LastDigit));
         }
      }

      public ICommand StartCommand => _startCommand;

      public event PropertyChangedEventHandler? PropertyChanged;

      private async void StartSpinning()
      {
         _soundPlayer.Play();
         IsSpinning = true;

         // Create a TaskCompletionSource to signal when the mouse click happens
         var clickTaskSource = new TaskCompletionSource<bool>();

         // Start waiting for the mouse click asynchronously
         var waitForClickTask = WaitForMouseClickAsync(clickTaskSource);

         for (int i = 0; i < 30; i++) // Adjust the number of spins
         {
            if (!IsSpinning) break;
            string luckyNumber = _random.Next(100, 1000).ToString("000");
            FirstDigit = luckyNumber.First().ToString();
            MidDigit = luckyNumber[1].ToString();
            LastDigit = luckyNumber.Last().ToString();
            await Task.Delay(100); // Adjust delay for speed
         }
         
         _soundPlayer.Stop();
         await waitForClickTask;

         IsSpinning = false;
      }

      // Helper method to wait for mouse click asynchronously
      private static async Task WaitForMouseClickAsync(TaskCompletionSource<bool> clickTaskSource)
      {
         MouseButtonEventHandler handler = null;
         handler = (s, e) =>
         {
            clickTaskSource.SetResult(true);
            Mouse.RemovePreviewMouseDownHandler(Application.Current.MainWindow, handler);
         };

         Mouse.AddPreviewMouseDownHandler(Application.Current.MainWindow, handler);

         await clickTaskSource.Task;
      }

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
