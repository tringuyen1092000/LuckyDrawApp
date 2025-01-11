using Prism.Commands;
using System.ComponentModel;
using System.Windows.Input;

namespace LuckyDrawApp
{
   public class MainWindowViewModel : INotifyPropertyChanged
   {
      private Random _random = new Random();
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

      public async void StartSpinning()
      {
         IsSpinning = true;
         for (int i = 0; i < 20; i++) // Adjust the number of spins
         {
            if (!IsSpinning) break;
            string luckyNumber = _random.Next(100, 1000).ToString("000");
            FirstDigit = luckyNumber.First().ToString();
            MidDigit = luckyNumber[1].ToString();
            LastDigit = luckyNumber.Last().ToString();
            await Task.Delay(150); // Adjust delay for speed
         }
         IsSpinning = false;
      }

      protected void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
