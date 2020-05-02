using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

using System.Collections.Generic;  // per list

namespace bollettini.Views
{
    public sealed partial class BollettiniPage : Page, INotifyPropertyChanged
    {
        public BollettiniPage()
        {
            InitializeComponent();
            loadcombo();
            loadcombo2();
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

        private double importo = 0;
        private double totalone = 0;
        private double importo2 = 0;
        private string descr2 = null;


        private void TextNrBollettini_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;

                //alle aggiungere controllo che in edit ci siano solo numeri

                if (TextNrBollettini.Text != "")
                {
                    double totale = double.Parse(TextNrBollettini.Text);
                    totale = totale * importo;



                    string addstring = totale.ToString("N2");

                    string aa = "Nr. ";
                    string bb = " bollettini da ";
                    string cc = " euro";

                    string dd = TextNrBollettini.Text;
                    string ee = importo.ToString("N2");

                    string ff = (" - (Totale: ");
                    string gg = totale.ToString("N2");
                    string hh = " euro)";

                    string addstring2 = aa + dd + bb + ee + cc + ff + gg + hh;

                    ListViewTest.Items.Add(addstring2);

                    totalone += totale;
                    //alle da rimettere boxtotale.Text = "costo totale: " + totalone.ToString("N2");
                    TextTotale.Text = totalone.ToString("N2");

                    TextNrBollettini.Text = "";
                }
            }
        }

        private void loadcombo()
        {
            //List<string> bollettini = new List<String>();
            Dictionary<string, string> bollettini = new Dictionary<string, string>();
            bollettini.Add("Diritti Mtc", "10.20");
            bollettini.Add("IdB - 16", "16.00");
            bollettini.Add("IdB - 32", "32.00");
            bollettini.Add("IdB - 48", "48.00");

            ComboBollettini.ItemsSource = bollettini;
            ComboBollettini.SelectedValuePath = "Value";
            ComboBollettini.DisplayMemberPath = "Key";


        }
        private void loadcombo2()
        {
            Dictionary<string, string> altriservizi = new Dictionary<string, string>();

            altriservizi.Add("Raccomandata", "5.40");
            altriservizi.Add("Lettera", "1.10");
            altriservizi.Add("Targa Auto", "41.50");

            ComboAltro.ItemsSource = altriservizi;

            // Specify the ComboBox items text and value
            ComboAltro.SelectedValuePath = "Value";
            ComboAltro.DisplayMemberPath = "Key";

        }






        private void ComboBollettini_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (ComboBollettini == null) return;
            //alle le tre righe sotto erano ok con combo impostata da xaml, non vanno con cs
            //var combo = (ComboBox)sender;
            //var item = (ComboBoxItem)combo.SelectedItem;
            //string selection = item.Content.ToString();

            ComboBox cb = sender as ComboBox;
            string selection = cb.SelectedValue.ToString();
            importo = double.Parse(selection);

        }

        private void ComboAltro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string selection = cb.SelectedValue.ToString();
            importo2 = double.Parse(selection);

            string key = ((KeyValuePair<string, string>)cb.SelectedItem).Key;

            descr2 = key;

        }

        private void TextNrAltro_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                e.Handled = true;

                //alle aggiungere controllo che in edit ci siano solo numeri

                if (TextNrAltro.Text != "")
                {
                    double totale = double.Parse(TextNrAltro.Text);
                    totale = totale * importo2;



                    string addstring = totale.ToString("N2");

                    string aa = "Nr. ";
                    string bb = " bollettini da ";
                    string cc = " euro";

                    string dd = TextNrAltro.Text;
                    string ee = importo2.ToString("N2");

                    string ff = (" - (Totale: ");
                    string gg = totale.ToString("N2");
                    string hh = " euro)";

                    string addstring2 = aa + dd + descr2 + ee + cc + ff + gg + hh;

                    ListViewTest.Items.Add(addstring2);

                    totalone += totale;
                    //alle da rimettere boxtotale.Text = "costo totale: " + totalone.ToString("N2");
                    TextTotale.Text = totalone.ToString("N2");


                    TextNrAltro.Text = "";
                }
            }

        }
    }
}
