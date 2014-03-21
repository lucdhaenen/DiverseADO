using AdoGemeenschap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for Stripfiguren.xaml
    /// </summary>
    public partial class Stripfiguren : Window
    {
        public Stripfiguren()
        {
            InitializeComponent();
        }

        private List<Figuur> figuren = new List<Figuur>();
        public List<Figuur> GewijzigdeFiguren = new List<Figuur>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            System.Windows.Data.CollectionViewSource figuurViewSource = ((System.Windows.Data.CollectionViewSource)
                (this.FindResource("figuurViewSource")));
            FiguurManager manager = new FiguurManager();
            figuren = manager.GetFiguren();
            figuurViewSource.Source = figuren;
        }

        private void figuurDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            object o = figuurDataGrid.ItemContainerGenerator.ItemFromContainer(e.Row);
            if (figuren.Contains(o))
            {
                GewijzigdeFiguren.Add((Figuur)figuurDataGrid.ItemContainerGenerator.ItemFromContainer(e.Row));
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var manager = new FiguurManager();
            if (GewijzigdeFiguren.Count() != 0)
            {
                try
                {
                    manager.SchrijfWijzigingen(GewijzigdeFiguren);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                GewijzigdeFiguren.Clear();                
            }
        }       
    }
}
