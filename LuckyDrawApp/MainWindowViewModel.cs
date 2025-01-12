﻿using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using System.IO;
using System.Windows.Media;

namespace LuckyDrawApp
{
   public class MainWindowViewModel : INotifyPropertyChanged
   {
      private readonly Random _random = new Random();
      private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
      private readonly ICommand _startCommand;
      private readonly List<string> _luckyNumberList = new List<string>();

      private bool _isSpinning;
      private string _firstDigit;
      private string _midDigit;
      private string _lastDigit;

      public MainWindowViewModel()
      {
         _firstDigit = "0";
         _midDigit = "0";
         _lastDigit = "0";
         _isSpinning = false;
         _startCommand = new DelegateCommand(StartSpinning);
         _luckyNumberList = GetLuckyNumberList(Helper.LUCKY_NUMBER_LIST + Helper.CSV_EXTENSION);
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
         if (_luckyNumberList.Count > 0)
         {
            if (File.Exists(Helper.SPINNING_EFFECT_PATH))
            {
               _mediaPlayer.Open(new Uri(Helper.SPINNING_EFFECT_PATH, UriKind.RelativeOrAbsolute));
               _mediaPlayer.Play();
            }

            IsSpinning = true;

            // Adjust the number of spins
            int numberOfSpins = 25;
            string? luckyNumber = null;
            for (int i = 0; i < numberOfSpins; i++)
            {
               if (!IsSpinning) break;
               luckyNumber = _luckyNumberList.ElementAt(_random.Next(0, _luckyNumberList.Count));
               FirstDigit = luckyNumber.First().ToString();
               MidDigit = luckyNumber[1].ToString();
               LastDigit = luckyNumber.Last().ToString();
               await Task.Delay(100); // Adjust delay for speed
            }

            if (_mediaPlayer.Source != null) _mediaPlayer.Stop();

            if (File.Exists(Helper.AFTER_SPIN_EFFECT_PATH))
            {
               _mediaPlayer.Open(new Uri(Helper.AFTER_SPIN_EFFECT_PATH, UriKind.RelativeOrAbsolute));
               _mediaPlayer.Play();
            }

            // Create a TaskCompletionSource to signal when the mouse click happens
            var clickTaskSource = new TaskCompletionSource<bool>();

            // Start waiting for the mouse click asynchronously
            var waitForClickTask = WaitForMouseClickAsync(clickTaskSource);

            await waitForClickTask;

            if (_mediaPlayer.Source != null) _mediaPlayer.Stop();

            if (luckyNumber != null) _luckyNumberList.Remove(luckyNumber);
            IsSpinning = false;
         }
         else
         {
            MessageBox.Show("No lucky number data available!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
         }
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

      private static List<string> GetLuckyNumberList(string filePath)
      {
         List<string> luckyNumberList = new List<string>();
         if (File.Exists(filePath))
         {
            using (var reader = new StreamReader(filePath))
            {
               while (!reader.EndOfStream)
               {
                  string? line = reader.ReadLine();
                  if (line != null && line.Length == 3 && IsDigitsOnly(line))
                  {
                     luckyNumberList.Add(line);
                  }
               }
            }
         }
         return luckyNumberList;
      }

      private static bool IsDigitsOnly(string str)
      {
         foreach (char c in str)
         {
            if (c < '0' || c > '9')
               return false;
         }

         return true;
      }
   }
}
