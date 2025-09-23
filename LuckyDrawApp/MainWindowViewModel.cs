using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using System.IO;
using System.Windows.Media;

namespace LuckyDrawApp
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly Random _random = new();

        private bool _isSpinning = false;
        private string _firstDigit = "0";
        private string _midDigit = "0";
        private string _lastDigit = "0";
        private string _luckyNumber = "000";

        public MainWindowViewModel()
        {
            StartCommand = new DelegateCommand<int?>(StartSpinning);
            LuckyNumberList = GetLuckyNumberList(Helper.LUCKY_NUMBER_LIST + Helper.CSV_EXTENSION);
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

        public MediaPlayer MediaPlayer { get; } = new();

        public string LuckyNumber
        {
            get => _luckyNumber;
            set
            {
                _luckyNumber = value;
                FirstDigit = _luckyNumber[0].ToString();
                MidDigit = _luckyNumber[1].ToString();
                LastDigit = _luckyNumber[^1].ToString();
                OnPropertyChanged(nameof(LuckyNumber));
            }
        }

        public List<string> LuckyNumberList { get; }

        public ICommand StartCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public async void StartSpinning(int? wantedNumber = null)
        {
            if (LuckyNumberList.Count > 0)
            {
                if (File.Exists(Helper.SPINNING_EFFECT_PATH))
                {
                    MediaPlayer.Open(new Uri(Helper.SPINNING_EFFECT_PATH, UriKind.RelativeOrAbsolute));
                    MediaPlayer.Play();
                }

                IsSpinning = true;

                // Adjust the number of spins
                const int numberOfSpins = 10000000;
                for (int i = 0; i < numberOfSpins; i++)
                {
                    if (!IsSpinning) break;
                    LuckyNumber = LuckyNumberList[_random.Next(1, LuckyNumberList.Count)];
                    await Task.Delay(100); // Adjust delay for speed
                }

                if (wantedNumber != null)
                {
                    LuckyNumber = wantedNumber?.ToString("D3") ?? string.Empty;
                    if (LuckyNumberList.Contains(LuckyNumber)) LuckyNumberList.Remove(LuckyNumber);
                }

                if (MediaPlayer.Source != null) MediaPlayer.Stop();

                if (File.Exists(Helper.AFTER_SPIN_EFFECT_PATH))
                {
                    MediaPlayer.Open(new Uri(Helper.AFTER_SPIN_EFFECT_PATH, UriKind.RelativeOrAbsolute));
                    MediaPlayer.Play();
                }

                IsSpinning = false;
            }
            else
            {
                MessageBox.Show("No lucky number data available!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static List<string> GetLuckyNumberList(string filePath)
        {
            List<string> luckyNumberList = [];
            if (File.Exists(filePath))
            {
                using var reader = new StreamReader(filePath);
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (line?.Length == 3 && IsDigitsOnly(line))
                    {
                        luckyNumberList.Add(line);
                    }
                }
            }
            return luckyNumberList;
        }

        private static bool IsDigitsOnly(string str)
        {
            return !str.Any(c => c < '0' || c > '9');
        }
    }
}
