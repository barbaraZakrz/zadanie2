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
using System.Globalization;

namespace zadanie2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<List<double>> dane = new List<List<double>>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Input_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Generuj_Click(object sender, RoutedEventArgs e)
        {
            dane.Clear();
            string[] tekstSplit = Input.Text.Split(' ');
            List<int> struktura = new List<int>();
            for(int i = 0; i<tekstSplit.Length; i++)
            {
                struktura.Add(int.Parse(tekstSplit[i]));
            }
            int a = struktura.Count;
            Random rnd = new Random();
            for(int i = 0; i < a - 3; i++)
            {
                List<double> warstwa = new List<double>();
                for(int j = 0; j < struktura[i+1]*(struktura[i] + 1); j++)
                {
                    warstwa.Add((rnd.NextDouble()*(struktura[a-1]-struktura[a-2]))+struktura[a-2]);
                }
                dane.Add(warstwa);
            }
        }

        private void Zapis_Click(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists("dane.txt"))
            {
                System.IO.File.Delete("dane.txt");
            }

            String wiersz = "";

            using (System.IO.StreamWriter sw = System.IO.File.CreateText("dane.txt"))
            {
                for (int i = 0; i < dane.Count; i++)
                {
                    wiersz = "";
                    for (int j = 0; j < dane[i].Count; j++)
                    {
                        String liczba = dane[i][j].ToString();
                        wiersz = String.Concat(wiersz, liczba, " ");
                    }
                    sw.WriteLine(wiersz);
                }
            }
        }

        private void Odczyt_Click(object sender, RoutedEventArgs e)
        {
            dane.Clear();
            string fileName = "dane.txt";
            string wiersz;
            string[] wierszSplit;
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((wiersz = file.ReadLine()) != null)
            {
                List<double> liczby = new List<double>();
                wierszSplit = wiersz.Split(' ');
                for (int i = 0; i < (wierszSplit.Length - 1); i++)
                {
                    liczby.Add(double.Parse(wierszSplit[i], CultureInfo.InvariantCulture));
                }
                dane.Add(liczby);
            }
            file.Close();

        }
    }
}
