using AdoGemeenschap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
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

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonBieren_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var conString = ConfigurationManager.ConnectionStrings["Bieren"];
                //var factory = DbProviderFactories.GetFactory(conString.ProviderName);
                var manager = new BierenDbManager();

                using (var conBieren = manager.GetConnection())
                {
                    //ConnectionStringSettings conString = ConfigurationManager.ConnectionStrings["Bieren"];
                    //conBieren.ConnectionString = conString.ConnectionString;
                    //conBieren.ConnectionString = Application.Current.Properties["Bieren2"].ToString();
                    conBieren.Open();
                    labelStatus.Content = "Bieren geopend";
                }
            }
            catch (Exception ex)
            {
                
                labelStatus.Content = ex.Message;
            }
        }

        private void buttonBonus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new RekeningenManager();
                labelStatus.Content = manager.SaldoBonus() + " rekeningen aangepast";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;             
            }
        }

        private void buttonStorten_Click(object sender, RoutedEventArgs e)
        {
            Decimal teStorten;
            if (decimal.TryParse(textBoxTeStorten.Text, out teStorten))
            {
                try
                {
                    var manager = new RekeningenManager();
                    if (manager.Storten(teStorten,textBoxRekeningNr.Text))
                    {
                       labelStatus.Content = "OK";
                    }
                    else
                    {
                        labelStatus.Content = "Rekening niet gevonden";
                    }
                }
                catch (Exception ex)
                {
                    labelStatus.Content = ex.Message;                    
                }
            }
            else
            {
                labelStatus.Content = "Geef een bedrag om te storten";
            }
        }
    }
}
