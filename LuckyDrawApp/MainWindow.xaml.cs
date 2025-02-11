﻿using System.IO;
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
      if (e.Key == Key.Space)
      {
         var viewModel = DataContext as MainWindowViewModel;
         if (viewModel != null && !viewModel.IsSpinning)
         {
            viewModel.MediaPlayer.Stop();
            viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
            viewModel.StartSpinning(false);
         }
      }
      else if (e.Key == Key.S)
      {
         var viewModel = DataContext as MainWindowViewModel;
         if (viewModel != null && !viewModel.IsSpinning)
         {
            viewModel.MediaPlayer.Stop();
            viewModel.LuckyNumberList.Remove(viewModel.LuckyNumber);
            viewModel.StartSpinning(true);
         }
      }
   }
}