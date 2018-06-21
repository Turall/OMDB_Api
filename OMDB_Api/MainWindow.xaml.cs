using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Newtonsoft.Json;

namespace OMDB_Api
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcher = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();

        }
        public void GetFilmInfo()
        {
            WebClient webClient = new WebClient();
            var title = filmTxt.Text.Split(',');
            for (int i = 0; i < title.Length; i++)
            {
                var result = webClient.DownloadString($"http://www.omdbapi.com/?apikey=cb2c41fe&t={title[i]}");
                dynamic data = JsonConvert.DeserializeObject(result);
                richtxt.FontWeight = FontWeights.UltraBold;
                richtxt.Text += data.Title + "\n PLot : " + data.Plot + "\n";
            }
            dispatcher.Stop();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            dispatcher.Tick += Dispatcher_Tick;
            dispatcher.Interval = new TimeSpan(0, 0, 2);
            dispatcher.Start();
            GetFilmInfo();

        }

        private void Dispatcher_Tick(object sender, EventArgs e)
        {

            GetFilmInfo();

        }
    }
}
