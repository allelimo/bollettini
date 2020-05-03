using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

using System.Collections.Generic;  // per list
using System.Collections.ObjectModel; // per observablecollection

namespace bollettini.Views
{
    public sealed partial class BollettiniPage : Page, INotifyPropertyChanged
    {
        public BollettiniPage()
        {
            InitializeComponent();
            LoadComboBollettini();
            LoadComboAltri();
            ListViewTest.ItemsSource = MyListArticoli;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private double ImportoBollettino = 0;
        private string DescrizioneBollettino = null;
        private double ImportoAltri = 0;
        private string DescrizioneAltri = null;
        private double GranTotale = 0;


        private static ObservableCollection<RiepilogoArticoli> MyListArticoli = new ObservableCollection<RiepilogoArticoli>();

        private void LoadComboBollettini()
        {
            //List<string> bollettini = new List<String>();
            Dictionary<string, string> bollettini = new Dictionary<string, string>();
            bollettini.Add("Diritti Mtc", "10.20");
            bollettini.Add("Imposta Bollo - 16", "16.00");
            bollettini.Add("Imposta Bollo - 32", "32.00");
            bollettini.Add("Imposta Bollo - 48", "48.00");
            bollettini.Add("Imposta Bollo - 64", "64.00");
            bollettini.Add("Targa ciclomotore", "13.00");
            bollettini.Add("Targa prova", "26.00");
            bollettini.Add("Targa motociclo", "23.00");
            bollettini.Add("Targa autovettura", "40.00");
            ComboBollettini.ItemsSource = bollettini;
            ComboBollettini.SelectedValuePath = "Value";
            ComboBollettini.DisplayMemberPath = "Key";
        }
        private void LoadComboAltri()
        {
            Dictionary<string, string> altriservizi = new Dictionary<string, string>();
            altriservizi.Add("Raccomandata", "5.40");
            altriservizi.Add("Lettera", "1.10");
            ComboAltro.ItemsSource = altriservizi;
            // Specify the ComboBox items text and value
            ComboAltro.SelectedValuePath = "Value";
            ComboAltro.DisplayMemberPath = "Key";
        }
        private void ComboBollettini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string selection = cb.SelectedValue.ToString();
            ImportoBollettino = double.Parse(selection);
            string key = ((KeyValuePair<string, string>)cb.SelectedItem).Key;
            DescrizioneBollettino = key;
        }
        private void ComboAltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string selection = cb.SelectedValue.ToString();
            ImportoAltri = double.Parse(selection);
            string key = ((KeyValuePair<string, string>)cb.SelectedItem).Key;
            DescrizioneAltri = key;
        }

        private void TextNrBollettini_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;

                //alle aggiungere controllo che in edit ci siano solo numeri

                if (TextNrBollettini.Text != "")
                {
                    double nrarticoli = double.Parse(TextNrBollettini.Text);
                    double totale = nrarticoli * (ImportoBollettino + 1.78); //alle rendere variabile 1.78
                    string strtotale = totale.ToString("N2");
                    string a = "Nr. ";
                    a = a + TextNrBollettini.Text;
                    a = a + " x ";
                    a = a + ImportoBollettino.ToString("N2");

                    RiepilogoArticoli artcar = new RiepilogoArticoli(strtotale, DescrizioneBollettino, a);
                    MyListArticoli.Add(artcar);

                    GranTotale += totale;
                    //alle da rimettere boxtotale.Text = "costo totale: " + totalone.ToString("N2");
                    TextTotale.Text = GranTotale.ToString("N2");
                    TextNrBollettini.Text = "";
                }
            }
        }

        private void TextNrAltro_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;

                //alle aggiungere controllo che in edit ci siano solo numeri

                if (TextNrAltro.Text != "")
                {


                    double nrarticolo = double.Parse(TextNrAltro.Text);
                    double totale = nrarticolo * ImportoAltri;

                    string strtotale = totale.ToString("N2");

                    string aaa = "Nr. ";
                    aaa = aaa + TextNrAltro.Text;
                    aaa = aaa + " x ";
                    aaa = aaa + ImportoAltri.ToString("N2");

                    RiepilogoArticoli artcar = new RiepilogoArticoli(strtotale, DescrizioneAltri, aaa);
                    MyListArticoli.Add(artcar);

  //                  ListViewTest.ItemsSource = MyListArticoli;




                    //string aa = "Nr. ";
                    //string bb = " bollettini da ";
                    //string cc = " euro";

                    //string dd = TextNrAltro.Text;
                    //string ee = importo2.ToString("N2");

                    //string ff = (" - (Totale: ");
                    //string gg = totale.ToString("N2");
                    //string hh = " euro)";

                    //string addstring2 = aa + dd + descr2 + ee + cc + ff + gg + hh;

                    //ListViewTest.Items.Add(addstring2);

                    GranTotale += totale;
                    //alle da rimettere boxtotale.Text = "costo totale: " + totalone.ToString("N2");
                    TextTotale.Text = GranTotale.ToString("N2");


                    TextNrAltro.Text = "";
                }
            }

        }
    }
}
