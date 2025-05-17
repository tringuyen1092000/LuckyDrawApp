using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Windows.Media;
using ExcelDataReader;
using System.Data;
using System.Collections.ObjectModel;

namespace LuckyDrawApp
{
   public class MainWindowViewModel : INotifyPropertyChanged
   {
      private readonly Random _random = new Random();
      private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
      private ObservableCollection<Employee> _employeeList = new ObservableCollection<Employee>();
      private ObservableCollection<Employee> _luckyEmployees = new ObservableCollection<Employee>();
      private ObservableCollection<Employee> _prizeList = new ObservableCollection<Employee>();
      private string _prizesTakenOnAvailable;
      private bool _isSpinning = false;
      private int _prizeTaken = 0;
      private int _availablePrize = 0;
      private int _prizeAmountPerSpin = 0;
      private int _columnAmount = 1;
      private int _resultFontSize = 110;
      private int _headerFontSize = 150;

      public MainWindowViewModel()
      {
         _employeeList = GetEmployeeList(Helper.LUCKY_NUMBER_LIST + Helper.XLSX_EXTENSION);
         _prizesTakenOnAvailable = $"PRIZES TAKEN ({PrizeTaken:D2}/{AvailablePrize:D2})";
      }

      public MediaPlayer MediaPlayer => _mediaPlayer;

      public int PrizeTaken
      {
         get => _prizeTaken;
         set
         {
            _prizeTaken = value;
            OnPropertyChanged(nameof(PrizeTaken));
            OnPropertyChanged(nameof(PrizesTakenOnAvailable));
         }
      }

      public int AvailablePrize
      {
         get => _availablePrize;
         set
         {
            _availablePrize = value;
            OnPropertyChanged(nameof(AvailablePrize));
            OnPropertyChanged(nameof(PrizesTakenOnAvailable));
         }
      }

      public int PrizeAmountPerSpin
      {
         get => _prizeAmountPerSpin;
         set
         {
            _prizeAmountPerSpin = value;
            OnPropertyChanged(nameof(PrizeAmountPerSpin));
         }
      }

      public int ColumnAmount
      {
         get => _columnAmount;
         set
         {
            _columnAmount = value;
            OnPropertyChanged(nameof(ColumnAmount));
         }
      }

      public int ResultFontSize
      {
         get => _resultFontSize;
         set
         {
            _resultFontSize = value;
            OnPropertyChanged(nameof(ResultFontSize));
            OnPropertyChanged(nameof(NameMinWidth));
            OnPropertyChanged(nameof(IdMinWidth));
         }
      }

      public double NameMinWidth => ResultFontSize * (ColumnAmount == 3 ? 11 : 12) + (AvailablePrize == 60 ? 100 : 0);

      public double IdMinWidth => ResultFontSize * (ColumnAmount == 3 ? 4 : 5);

      public string PrizesTakenOnAvailable
      {
         get => $"PRIZES TAKEN ({PrizeTaken:D2}/{AvailablePrize:D2})";
         set
         {
            OnPropertyChanged(nameof(PrizesTakenOnAvailable));
         }
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

      public ObservableCollection<Employee> LuckyEmployees
      {
         get => _luckyEmployees;
         set
         {
            _luckyEmployees = value;
            OnPropertyChanged(nameof(LuckyEmployees));
         }
      }

      public ObservableCollection<Employee> EmployeeList
      {
         get => _employeeList;
         set
         {
            _employeeList = value;
            OnPropertyChanged(nameof(EmployeeList));
         }
      }


      public ObservableCollection<Employee> PrizeList
      {
         get => _prizeList;
         set
         {
            _prizeList = value;
            OnPropertyChanged(nameof(PrizeList));
         }
      }

      public int HeaderFontSize
      {
         get => _headerFontSize;
         set
         {
            _headerFontSize = value;
            OnPropertyChanged(nameof(HeaderFontSize));
         }
      }

      public event PropertyChangedEventHandler? PropertyChanged;

      public async void StartSpinning(bool isRedraw = false)
      {
         if (_employeeList.Count > 0)
         {
            if (File.Exists(Helper.SPINNING_EFFECT_PATH))
            {
               _mediaPlayer.Open(new Uri(Helper.SPINNING_EFFECT_PATH, UriKind.RelativeOrAbsolute));
               _mediaPlayer.Play();
            }

            IsSpinning = true;

            // Adjust the number of spins
            int numberOfSpins = 50;
            for (int i = 0; i < numberOfSpins; i++)
            {
               if (!IsSpinning) break;
               LuckyEmployees = GetLuckyEmployees(PrizeAmountPerSpin);
               await Task.Delay(100); // Adjust delay for speed
            }

            PrizeTaken += LuckyEmployees.Count;
            if (!isRedraw)
            {
               Application.Current.Dispatcher.Invoke(() =>
               {
                  var mainWindow = (MainWindow)Application.Current.MainWindow;
                  mainWindow._txtPrizeTakenOnAvailable.Text = PrizesTakenOnAvailable;
               });
            }

            foreach (Employee employee in LuckyEmployees)
            {
               EmployeeList.Remove(employee);
               employee.IsAwarded = true;
               employee.PrizeAwarded = AvailablePrize switch
               {
                  3 => 1,
                  8 => 2,
                  10 => 3,
                  15 => 4,
                  60 => 5,
                  _ => employee.PrizeAwarded
               };
               PrizeList.Add(employee);
            }

            if (_mediaPlayer.Source != null) _mediaPlayer.Stop();

            if (File.Exists(Helper.AFTER_SPIN_EFFECT_PATH))
            {
               _mediaPlayer.Open(new Uri(Helper.AFTER_SPIN_EFFECT_PATH, UriKind.RelativeOrAbsolute));
               _mediaPlayer.Play();
            }

            // Show congratulation effect
            Application.Current.Dispatcher.Invoke(() =>
            {
               var mainWindow = (MainWindow)Application.Current.MainWindow;
               mainWindow.ShowStars();
               mainWindow.ShowCircles();
            });

            if (PrizeList.Count == AvailablePrize && AvailablePrize != 60)
            {
               await Task.Delay(2000);
               ColumnAmount = AvailablePrize switch
               {
                  3 => 1,
                  8 => 1,
                  10 => 2,
                  15 => 3,
                  _ => ColumnAmount
               };
               LuckyEmployees = PrizeList;

               // Show congratulation effect
               Application.Current.Dispatcher.Invoke(() =>
               {
                  var mainWindow = (MainWindow)Application.Current.MainWindow;
                  mainWindow.ShowStars();
                  mainWindow.ShowCircles();
               });
            }

            IsSpinning = false;
         }
         else
         {
            MessageBox.Show("No data available!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
         }
      }

      private void OnPropertyChanged(string propertyName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      private ObservableCollection<Employee> GetLuckyEmployees(int amount)
      {
         ObservableCollection<Employee> luckyEmployeeList = new ObservableCollection<Employee>();
         for (int i = 0; i < amount; i++)
         {
            int index = _random.Next(0, _employeeList.Count);
            if (luckyEmployeeList.Contains(EmployeeList[index]) == false)
            {
               luckyEmployeeList.Add(_employeeList[index]);
            }
            else
            {
               i--;
            }
         }

         return luckyEmployeeList;
      }

      private static ObservableCollection<Employee> GetEmployeeList(string filePath)
      {
         ObservableCollection<Employee> employeeList = new ObservableCollection<Employee>();

         if (File.Exists(filePath))
         {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
               using (var reader = ExcelReaderFactory.CreateReader(stream))
               {
                  var result = reader.AsDataSet();
                  var table = result.Tables[0];

                  foreach (DataRow row in table.Rows)
                  {
                     string? cellValue = row[0]?.ToString();
                     if (cellValue != null)
                     {
                        string name = cellValue[..(cellValue.IndexOf("-"))].TrimEnd();
                        string id = cellValue[(cellValue.IndexOf("-") + 2)..].TrimStart();
                        if (string.IsNullOrWhiteSpace(id) == false && string.IsNullOrWhiteSpace(name) == false)
                        {
                           employeeList.Add(new Employee(id, name, false));
                        }
                     }
                  }
               }
            }
         }

         return employeeList;
      }
   }
}
