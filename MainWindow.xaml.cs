using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace Paster
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title += System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("tick");
            int n = (int)timer.Tag;
            if (n > 0)
            {
                timer.Tag = n - 1;
                Dispatcher.Invoke(() =>
                {
                    BtnStart.Content = $"{n}s left";
                    BtnStart.IsEnabled = false;

                });
            }
            else
            {
                var s = TbInput.Text;
                s = s.Replace(Environment.NewLine, "\n");
                //Console.WriteLine(s);
                SendKeys.SendWait(s);

                Dispatcher.Invoke(() =>
                {
                    BtnStart.Content = "点击后3s开始";
                    BtnStart.IsEnabled = true;
                });
                timer.IsEnabled = false;
            }
        }
        private readonly DispatcherTimer timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            int seconds = 3;
            timer.Tag = seconds;
            timer.Start();
            Console.WriteLine(timer.IsEnabled);
        }

        private void HlProject_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }

        private void HlAuthor_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));

        }
    }
}
