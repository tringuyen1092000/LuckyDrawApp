using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
   private void Window_KeyDown(object sender, KeyEventArgs e)
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
                  _txtPrizeTakenOnAvailable.Text = viewModel.PrizesTakenOnAvailable;
                  viewModel.ColumnAmount = 1;
                  viewModel.LuckyEmployees.Clear();
                  viewModel.PrizeList.Clear();
                  viewModel.ResultFontSize = 100;
                  viewModel.HeaderFontSize = 150;
               }
               break;
            case Key.NumPad2 or Key.D2:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 1;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 8;
                  _txtPrizeTakenOnAvailable.Text = viewModel.PrizesTakenOnAvailable;
                  viewModel.ColumnAmount = 1;
                  viewModel.LuckyEmployees.Clear();
                  viewModel.PrizeList.Clear();
                  viewModel.ResultFontSize = 60;
                  viewModel.HeaderFontSize = 130;
               }
               break;
            case Key.NumPad3 or Key.D3:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 2;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 10;
                  _txtPrizeTakenOnAvailable.Text = viewModel.PrizesTakenOnAvailable;
                  viewModel.ColumnAmount = 2;
                  viewModel.LuckyEmployees.Clear();
                  viewModel.PrizeList.Clear();
                  viewModel.ResultFontSize = 50;
                  viewModel.HeaderFontSize = 130;
               }
               break;
            case Key.NumPad4 or Key.D4:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 5;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 15;
                  _txtPrizeTakenOnAvailable.Text = viewModel.PrizesTakenOnAvailable;
                  viewModel.ColumnAmount = 1;
                  viewModel.LuckyEmployees.Clear();
                  viewModel.PrizeList.Clear();
                  viewModel.ResultFontSize = 40;
                  viewModel.HeaderFontSize = 120;
               }
               break;
            case Key.NumPad5 or Key.D5:
               if (!viewModel.IsSpinning)
               {
                  viewModel.PrizeAmountPerSpin = 20;
                  viewModel.PrizeTaken = 0;
                  viewModel.AvailablePrize = 60;
                  _txtPrizeTakenOnAvailable.Text = viewModel.PrizesTakenOnAvailable;
                  viewModel.ColumnAmount = 2;
                  viewModel.LuckyEmployees.Clear();
                  viewModel.PrizeList.Clear();
                  viewModel.ResultFontSize = 30;
                  viewModel.HeaderFontSize = 100;
               }
               break;
            case Key.Space:
               Start_Click(sender, e);
               break;
            default:
               break;
         }
      }
   }

   private void Start_Click(object sender, RoutedEventArgs e)
   {
      MainWindowViewModel? viewModel = DataContext as MainWindowViewModel;
      if (viewModel != null && !viewModel.IsSpinning)
      {
         if (viewModel.PrizeTaken < viewModel.AvailablePrize)
         {
            viewModel.MediaPlayer.Stop();
            viewModel.StartSpinning();
         }
         else if (viewModel.PrizeTaken == viewModel.AvailablePrize)
         {
            viewModel.MediaPlayer.Stop();
            viewModel.PrizeAmountPerSpin = 1;
            _txtPrizeTakenOnAvailable.Text = "RE-DRAW";
            viewModel.ColumnAmount = 1;
            viewModel.LuckyEmployees.Clear();
            viewModel.PrizeList.Clear();
            viewModel.ResultFontSize = 110;
            viewModel.HeaderFontSize = 150;
            viewModel.StartSpinning(true);
         }
         else
         {
            _txtPrizeTakenOnAvailable.Text = "RE-DRAW";
            viewModel.MediaPlayer.Stop();
            viewModel.StartSpinning();
         }
      }
   }

   public void ShowStars()
   {
      Random random = new Random();
      for (int i = 0; i < 40; i++)
      {
         // Create a star shape
         Polygon star = CreateStar(5, 20, 10);
         star.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCC33"));

         FireworksCanvas.Children.Add(star);

         double width = FireworksCanvas.ActualWidth;
         double height = FireworksCanvas.ActualHeight;
         double centerX = width / 2;
         double centerY = height / 2;
         double startX = centerX - 20 + random.NextDouble() * 40;
         double startY = centerY - 10 + random.NextDouble() * 20;
         double endX = startX < centerX ? random.NextDouble() * startX : (width - startX) * random.NextDouble() + startX;
         double endY = startY < centerY ? random.NextDouble() * startY * 2 : (height - startY) / 2 * random.NextDouble() + startY;

         Canvas.SetLeft(star, startX);
         Canvas.SetTop(star, startY);

         Storyboard storyboard = new Storyboard();

         // Scale animation for zoom in effect
         ScaleTransform scaleTransform = new ScaleTransform(0, 0);
         star.RenderTransform = scaleTransform;
         star.RenderTransformOrigin = new Point(0.5, 0.5);

         DoubleAnimation scaleXAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(scaleXAnimation, star);
         Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));

         DoubleAnimation scaleYAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(scaleYAnimation, star);
         Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));

         // Opacity animation for fade out effect
         DoubleAnimation opacityAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(opacityAnimation, star);
         Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));

         // Translation animations for moving the star
         DoubleAnimation translateXAnimation = new DoubleAnimation(startX, endX, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(translateXAnimation, star);
         Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath("(Canvas.Left)"));

         DoubleAnimation translateYAnimation = new DoubleAnimation(startY, endY, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(translateYAnimation, star);
         Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath("(Canvas.Top)"));

         storyboard.Children.Add(scaleXAnimation);
         storyboard.Children.Add(scaleYAnimation);
         storyboard.Children.Add(opacityAnimation);
         storyboard.Children.Add(translateXAnimation);
         storyboard.Children.Add(translateYAnimation);

         storyboard.Completed += (s, e) => FireworksCanvas.Children.Remove(star);
         storyboard.Begin();
      }
   }

   public void ShowCircles()
   {
      Random random = new Random();
      for (int i = 0; i < 20; i++)
      {
         int circleWidth = random.Next(10, 80);

         // Create a circular outline
         Ellipse circle = new Ellipse
         {
            Width = circleWidth, // Set the width of the circle
            Height = circleWidth, // Set the height of the circle
            Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCC33"))
         };

         FireworksCanvas.Children.Add(circle);

         double width = FireworksCanvas.ActualWidth;
         double height = FireworksCanvas.ActualHeight;
         double centerX = width / 2;
         double centerY = height / 2;
         double startX = centerX - 100 + random.NextDouble() * 200;
         double startY = centerY - 50 + random.NextDouble() * 100;
         double endX = startX < centerX ? random.NextDouble() * startX : (width - startX) * random.NextDouble() + startX;
         double endY = startY < centerY ? random.NextDouble() * startY * 2 : (height - startY) / 2 * random.NextDouble() + startY;

         Canvas.SetLeft(circle, startX);
         Canvas.SetTop(circle, startY);

         Storyboard storyboard = new Storyboard();

         // Scale animation for zoom in effect
         ScaleTransform scaleTransform = new ScaleTransform(0, 0);
         circle.RenderTransform = scaleTransform;
         circle.RenderTransformOrigin = new Point(0.5, 0.5);

         DoubleAnimation scaleXAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(scaleXAnimation, circle);
         Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("RenderTransform.ScaleX"));

         DoubleAnimation scaleYAnimation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(scaleYAnimation, circle);
         Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("RenderTransform.ScaleY"));

         // Opacity animation for fade out effect
         DoubleAnimation opacityAnimation = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(opacityAnimation, circle);
         Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath(OpacityProperty));

         // Translation animations for moving the star
         DoubleAnimation translateXAnimation = new DoubleAnimation(startX, endX, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(translateXAnimation, circle);
         Storyboard.SetTargetProperty(translateXAnimation, new PropertyPath("(Canvas.Left)"));

         DoubleAnimation translateYAnimation = new DoubleAnimation(startY, endY, new Duration(TimeSpan.FromSeconds(2)));
         Storyboard.SetTarget(translateYAnimation, circle);
         Storyboard.SetTargetProperty(translateYAnimation, new PropertyPath("(Canvas.Top)"));

         storyboard.Children.Add(scaleXAnimation);
         storyboard.Children.Add(scaleYAnimation);
         storyboard.Children.Add(opacityAnimation);
         storyboard.Children.Add(translateXAnimation);
         storyboard.Children.Add(translateYAnimation);

         storyboard.Completed += (s, e) => FireworksCanvas.Children.Remove(circle);
         storyboard.Begin();
      }
   }

   private Polygon CreateStar(int numPoints, double outerRadius, double innerRadius)
   {
      Polygon star = new Polygon();
      PointCollection points = new PointCollection();
      double angle = Math.PI / numPoints;

      for (int i = 0; i < 2 * numPoints; i++)
      {
         double r = (i % 2 == 0) ? outerRadius : innerRadius;
         double x = r * Math.Sin(i * angle);
         double y = -r * Math.Cos(i * angle);
         points.Add(new Point(x, y));
      }

      star.Points = points;
      return star;
   }
}