using AdoGemeenschap;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for OverzichtBrouwers.xaml
    /// </summary>
    public partial class OverzichtBrouwers : Window
    {

        private CollectionViewSource brouwerViewSource;
        public ObservableCollection<Brouwer> brouwersOb = new ObservableCollection<Brouwer>();
        public List<Brouwer> OudeBrouwers = new List<Brouwer>();

        public OverzichtBrouwers()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            VulDeGrid();
            textBoxZoeken.Focus();
        }

        private void buttonZoeken_Click(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
        }


        private void VulDeGrid()
        {
            brouwerViewSource = ((CollectionViewSource)(this.FindResource("brouwerViewSource")));
            var manager = new BrouwerManager();
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);

            labelTotalRowCount.Content = brouwersOb.Count();
            brouwerViewSource.Source = brouwersOb;
            goUpdate();
        }

        private void goUpdate()
        {
            if (brouwerViewSource.View.CurrentPosition == 0)
            {
                buttonGoToPrevious.IsEnabled = false;
                buttonGoToFirst.IsEnabled = false;
                buttonGoToNext.IsEnabled = true;
                buttonGoToLast.IsEnabled = true;
            }
            else if (brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1)
            {
                buttonGoToNext.IsEnabled = false;
                buttonGoToLast.IsEnabled = false;
                buttonGoToPrevious.IsEnabled = true;
                buttonGoToFirst.IsEnabled = true;
            }
            else
            {
                buttonGoToPrevious.IsEnabled = true;
                buttonGoToFirst.IsEnabled = true;
                buttonGoToNext.IsEnabled = true;
                buttonGoToLast.IsEnabled = true;
            }
            if (brouwerDataGrid.Items.Count != 0)
            {
                if (brouwerDataGrid.SelectedItem != null)
                {
                    brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                    listBoxBrouwers.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }
            if (brouwerDataGrid.Items.Count != 0)
            {
                if (brouwerDataGrid.SelectedItem != null)

                {
                    brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                    listBoxBrouwers.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }
        }

        private void buttonGoToFirst_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToFirst();
            goUpdate();
        }

        private void buttonGoToPrevious_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToPrevious();
            goUpdate();
        }

        private void buttonGoToNext_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToNext();
            goUpdate();
        }

        private void buttonGoToLast_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToLast();
            goUpdate();
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Brouwer oudeBrouwer in e.OldItems)
                {
                    OudeBrouwers.Add(oudeBrouwer);
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var manager = new BrouwerManager();
            if (OudeBrouwers.Count() != 0)
            {
                manager.SchrijfVerwijderingen(OudeBrouwers);
            }
            OudeBrouwers.Clear();
        }
    }
}
