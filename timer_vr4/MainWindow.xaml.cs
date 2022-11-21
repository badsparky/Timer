using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace timer_vr4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Uri window = new Uri("/Ti.xaml", UriKind.Relative);
            framne.Source = window;
        }

        private void backButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (framne.NavigationService.CanGoBack)
            {
            }
            else
            {
                return;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (framne.NavigationService.CanGoBack)
            {
                framne.NavigationService.GoBack();
            }
            else
            {
                return;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                    DragMove();
                }
                else
                {
                    DragMove();
                }
            }
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            redx.Foreground = new SolidColorBrush(Colors.White);
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            redx.Foreground = new SolidColorBrush(Colors.DimGray);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("/Ti.xaml", UriKind.Relative);
            framne.Resources.Clear();
            framne.Refresh();
            framne.Source=uri;
        }

        private void MusicButton_Click(object sender, RoutedEventArgs e)
        {
            Uri window = new Uri("/SettingPage.xaml", UriKind.Relative);
            framne.Source = window;
        }
    }
}
