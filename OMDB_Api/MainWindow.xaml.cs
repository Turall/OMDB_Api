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
using System.Windows.Media;
using Newtonsoft.Json;

namespace OMDB_Api
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> films;
        public MainWindow()
        {
            InitializeComponent();

        }
        public void GetFilmInfo(string filmName)
        {
            WebClient webClient = new WebClient();
            var result = webClient.DownloadString($"http://www.omdbapi.com/?apikey=cb2c41fe&t={filmName}");
            dynamic data = JsonConvert.DeserializeObject(result);
            Dispatcher.Invoke(new Action(() => richtxt.Text +="Title : " + data.Title + "\nPlot : " + data.Plot + "\n\n"));
            Dispatcher.Invoke(new Action(() => richtxt.Foreground = Brushes.WhiteSmoke));
            Dispatcher.Invoke(new Action(() => richtxt.FontWeight = FontWeights.Bold));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var filminfo = filmTxt.Text;
            films = filmTxt.Text.Split(',').ToList();
            if (!string.IsNullOrWhiteSpace(richtxt.Text))
            {
                richtxt.Clear();
            }
            foreach (var item in films)
            {
                NewThread(item);

            }
        }

        public void NewThread(string film)
        {
            var thread = new Thread(new ThreadStart(
                () =>
                {
                    GetFilmInfo(film);
                }));
            thread.Start();

        }

        
    }
}
