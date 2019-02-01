using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _count;

        public MainWindow()
        {
            InitializeComponent();
            TextBox.Text = _count.ToString();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TextBox.Text = (++_count).ToString();
        }
    }
}