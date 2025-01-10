using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LuckyDrawApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Random _random = new Random();
    private bool _isSpinning = false;
    
    public MainWindow()
    {
        InitializeComponent();
        // ImageBrush myBrush = new ImageBrush();
        // myBrush.ImageSource = new BitmapImage(new Uri("./Images/LuckyDrawBG.png", UriKind.Relative));
        // this.Background = myBrush;
    }
    
    private async void StartButton_Click(object sender, RoutedEventArgs e)
    {
        _isSpinning = true;
        for (int i = 0; i < 20; i++) // Adjust the number of spins
        {
            if (!_isSpinning)
                break;
            NumberDisplay.Text = _random.Next(100, 1000).ToString("000");
            await Task.Delay(100); // Adjust delay for speed
        }
        _isSpinning = false;
    }
}