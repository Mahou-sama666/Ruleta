using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public List<int> listResult = new List<int>();
        int nMood = -1;
        double nPariu = 0;
        double nCW = 0;
        double nCont = 500;
        int[] nValori = new int[4];
        int nVictorii = 0;
        public MainWindow()
        {
            InitializeComponent();

            image1.Source = new BitmapImage(new Uri(@"/Images/1.bmp", UriKind.Relative));
            image2.Source = new BitmapImage(new Uri(@"/Images/1.bmp", UriKind.Relative));
            image3.Source = new BitmapImage(new Uri(@"/Images/1.bmp", UriKind.Relative));

            this.txtBanca.Text = nCont.ToString();
            this.txtVictorii.Text = nVictorii.ToString();
            this.txtCW.Text = nCW.ToString();
            btnJoaca.IsEnabled = false;

            nValori[0] = 0;
            nValori[1] = 0;
            nValori[2] = 0;
            nValori[3] = 0;
            SetRedOrBlackImages();
            btnDublu.IsEnabled = false;
        }
        private void Fair_game()
        {
            if (nMood == 4 && nPariu > 250 && listResult[0] == listResult[1])
            {
                if (listResult[1] == 3)
                    listResult[1]--;
                else listResult[1]++;

            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            btnJoaca.IsEnabled = false;

            nCont = nCont - nPariu;
            this.txtBanca.Text = nCont.ToString();
            for (int i = 0; i <= 9; i++)
            {

                listResult.Clear();
                await Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(millisecondsTimeout: (500 + 20 * i));

                });
                var random = new Random();

                for (int j = 0; j <= 2; j++)
                {
                    int nr = random.Next(1, 4);
                    listResult.Add(nr);
                    var bitmapImage = new BitmapImage(new Uri(@"/Images/" + nr + ".bmp", UriKind.Relative));

                    var imageControl = this.FindName("image" + (j + 1)) as Image;

                    imageControl.Source = bitmapImage;
                }

            }
            Calcul(listResult);
            if (nCont > 0) btnJoaca.IsEnabled = true;
        }
        private async Task<List<int>> Joaca()
        {
            var listResult = new List<int>();

            return listResult;
        }
        private void ValidareDuble()
        {
            if (nCW > 0) btnDublu.IsEnabled = true;
            else btnDublu.IsEnabled = false;
        }
        private void Calcul(List<int> listResult)
        {
            Fair_game();
            if ((listResult[0] == listResult[1]) && (listResult[1] == listResult[2]))
            {
                Fair_game();

                nCW = 2 * nPariu + nPariu * nVictorii / 10;
                nVictorii = nVictorii + 1;
                nCont = nCont + nCW;
                MessageBox.Show("Jackpot! " + System.Environment.NewLine + "Ai castigat: " + nCW);

            }
            else if (listResult[0] == listResult[1])
            {
                Fair_game();

                nCW = 1.25 * nPariu + nPariu * nVictorii / 10;
                nVictorii = nVictorii + 1;
                nCont = nCont + nCW;
                MessageBox.Show("Mini Jackpot! " + System.Environment.NewLine + "Ai castigat: " + nCW);

            }
            else
            {
                nVictorii = 0;
                nCW = 0;
                MessageBox.Show("Mai incearca!");
            }

            this.txtBanca.Text = nCont.ToString();
            this.txtVictorii.Text = nVictorii.ToString();
            this.txtCW.Text = nCW.ToString();
            ValidarePariuri();
            ValidareDuble();

            if (nVictorii > 0)
            {
                btnDublu.IsEnabled = true;
            }
        }

        private void ValidarePariuri()
        {
            
            if (nCont < 500)
            {
                btnPariu500.IsEnabled = false;
            }
            else
            {
                btnPariu500.IsEnabled = true;
            }

            if (nCont < 100)
            {
                btnPariu100.IsEnabled = false;
            }
            else
            {
                btnPariu100.IsEnabled = true;
            }

            if (nCont < 50)
            {
                btnPariu50.IsEnabled = false;
            }
            else
            {
                btnPariu50.IsEnabled = true;
            }

            if (nCont < 10)
            {
                btnPariu10.IsEnabled = false;
            }
            else
            {
                btnPariu10.IsEnabled = true;
            }

            if (nCont <= 0)
            {
                btnPariuMax.IsEnabled = false;
                btnJoaca.IsEnabled = false;
            }
            else
            {
                btnPariuMax.IsEnabled = true;
                btnJoaca.IsEnabled = true;
            }
            if (nMood == 0) btnJoaca.IsEnabled = false;
        }

        private void ToggleButton10_Click(object sender, RoutedEventArgs e)
        {
            Mood();
            if (nMood == 6)
            {
                btnJoaca.IsEnabled = false;
            }
            else
            {
                btnJoaca.IsEnabled = true;
                nPariu = 10;
                btnPariu50.IsChecked = false;
                btnPariu100.IsChecked = false;
                btnPariu500.IsChecked = false;
                btnPariuMax.IsChecked = false;
                nMood = 6;
            }
            Mood();
        }
        private void ToggleButton100_Click(object sender, RoutedEventArgs e)
        {
            Mood();
            if (nMood == 1)
            {
                btnJoaca.IsEnabled = false;
            }
            else
            {
                btnJoaca.IsEnabled = true;
                nPariu = 100;
                btnPariu50.IsChecked = false;
                btnPariu10.IsChecked = false;
                btnPariu500.IsChecked = false;
                btnPariuMax.IsChecked = false;
                nMood = 1;
            }
            Mood();
        }
        private void ToggleButton50_Click(object sender, RoutedEventArgs e)
        {
            Mood();
            if (nMood == 2)
            {
                btnJoaca.IsEnabled = false;
            }
            else
            {
                btnJoaca.IsEnabled = true;
                nPariu = 50;
                btnPariu10.IsChecked = false;
                btnPariu100.IsChecked = false;
                btnPariu500.IsChecked = false;
                btnPariuMax.IsChecked = false;
                nMood = 2;
            }
            Mood();
        }
        private void ToggleButton500_Click(object sender, RoutedEventArgs e)
        {
            Mood();
            if (nMood == 3)
            {
                btnJoaca.IsEnabled = false;
            }
            else
            {
                btnJoaca.IsEnabled = true;
                nPariu = 500;
                btnPariu10.IsChecked = false;
                btnPariu100.IsChecked = false;
                btnPariu50.IsChecked = false;
                btnPariuMax.IsChecked = false;
                nMood = 3;
            }
            Mood();
        }
        private void ToggleButtonMax_Click(object sender, RoutedEventArgs e)
        {
            Mood();
            if (nMood == 4)
            {
                btnJoaca.IsEnabled = false;
            }
            else
            {
                btnJoaca.IsEnabled = true;
                nPariu = nCont;
                btnPariu10.IsChecked = false;
                btnPariu100.IsChecked = false;
                btnPariu50.IsChecked = false;
                btnPariu500.IsChecked = false;
                nMood = 4;
            }
            Mood();
        }
        private void Mood()
        {
            if (btnPariu10.IsChecked == false && btnPariu50.IsChecked == false && btnPariu100.IsChecked == false && btnPariu500.IsChecked == false && btnPariuMax.IsChecked == false)
                nMood = 0;
            ValidarePariuri();
        }
        private void btnPariu50_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnPariu10_Checked(object sender, RoutedEventArgs e)
        {

        }
        int nrJocDublu = 0;
        private void btnDublu_Click(object sender, RoutedEventArgs e)
        {
            this.Canvas1.Visibility = Visibility.Visible;
            nrJocDublu = 0;
            btnBlack.Background.Freeze();
            btnRed.Background.Freeze();
            //todo: add canvas1 as double function, red and black with all the neccessary functions : close auto if banca=0 or if close button is clicked
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Canvas1.Visibility = Visibility.Collapsed;
            nCont = nCont + nCW;
            nCW = 0;
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            //7 pentru rosu 8 pentru negru 0 pentru niciuna

            RedOrBlack(7);
        }

        private void RedOrBlack(int nRedBlack)
        {
            if (nrJocDublu == 0)
            {
                nCont = nCont - nCW;
                txtBanca.Text = nCont.ToString();
            }

            nrJocDublu++;

            var random = new Random();
            int nRasp = random.Next(7, 9);

            for (int i = 3; i >= 1; i--)
                nValori[i] = nValori[i - 1];
            nValori[0] = nRasp;
            SetRedOrBlackImages();

            if (nRedBlack == nRasp)
                nCW = 2 * nCW;
            else
            {
                nCW = 0;
                // this.Canvas1.Visibility = Visibility.Collapsed;
            }
            txtCW.Text = nCW.ToString();
            ValidarePariuri();
            ValidareDuble();
        }

        private void SetRedOrBlackImages()
        {
            for (int i = 0; i <= 3; i++)
            {
                BitmapImage bitmapImage;
                if (nValori[i] == 0)
                {
                    bitmapImage = new BitmapImage(new Uri(@"/Images/1.bmp", UriKind.Relative));
                }
                else if (nValori[i] == 7)
                {
                    bitmapImage = new BitmapImage(new Uri(@"/1_heart.png", UriKind.Relative));
                }
                else if (nValori[i] == 8)
                {
                    bitmapImage = new BitmapImage(new Uri(@"/1_pica.png", UriKind.Relative));
                }
                else return;


                var imageControl = this.FindName("dublu" + (i + 1)) as Image;

                imageControl.Source = bitmapImage;
            }
        }

        private void btnBlack_Click(object sender, RoutedEventArgs e)
        {
            
            RedOrBlack(8);
        }
    }
}
