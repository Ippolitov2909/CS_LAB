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
using Lab;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public V3MainCollection collection;
        public MainWindow()
        {
            InitializeComponent();
            collection = new V3MainCollection();
            collection.AddDefaults();
            CollectionView collViewGrid = new CollectionView(collection);
            collViewGrid.Filter = collection.FilterByGrid;
            this.DataContext = collection;
            lisBox_DataOnGrid.DataContext = collViewGrid;
        }
        private void buttonGet(object sender, RoutedEventArgs e)
        {
            string text = lisBox_DataCollection.SelectedItem.ToString();
            MessageBox.Show("Pressed button Get");
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                MessageBox.Show(collection.ToString());
            }

        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            SaveUnsaved();
            collection = new V3MainCollection();
            this.DataContext = collection;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            SaveUnsaved();
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                try
                {
                    collection = V3MainCollection.Load(dlg.FileName);
                    DataContext = null;
                    DataContext = collection;
                    collection.changed_not_saved = false;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                try
                {
                    collection.Save(filename);
                    collection.changed_not_saved = false;
                } 
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void Add_Defaults_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.AddDefaults();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDefaultsV3DataCollection_Click(object sender, RoutedEventArgs e)
        {
            V3DataCollection item = new V3DataCollection("default", new DateTime());
            item.InitRandom(5, 10.0f, 10.0f, 0.0f, 0.0f);
            try
            {
                collection.Add(item);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddDefaultsV3DataOnGrid_Click(object sender, RoutedEventArgs e)
        {
            V3DataOnGrid item = new V3DataOnGrid(new Grid1D(1.0f, 2), new Grid1D(1.0f, 3), "default", new DateTime());
            item.InitRandom(0.0, 10.0);
            collection.Add(item);
        }

        private void AddElementFromFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            try
            {
                if (dlg.ShowDialog() == true)
                {
                    V3DataCollection item = new V3DataCollection(dlg.FileName);
                    collection.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.Remove((V3Data)lisBox_Main.SelectedItem);
            } finally
            {
                ;
            }
        }


        private void byGrid_Filter (object senderr, FilterEventArgs args)
        {
            V3Data item = args.Item as V3Data;
            if (item is V3DataOnGrid) args.Accepted = true;
            else args.Accepted = false;

        }
        private void byCollection_Filter(object senderr, FilterEventArgs args)
        {
            V3Data item = args.Item as V3Data;
            if (item is V3DataOnGrid) args.Accepted = false;
            else args.Accepted = true;

        }
        private void SaveUnsaved ()
        {

            if (collection.changed_not_saved)
            {
                MessageBoxResult result = MessageBox.Show("Несохраненные данные буду утеряны. Вы хотите сохранить изменения?", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    if (dlg.ShowDialog() == true)
                    {
                        string filename = dlg.FileName;
                        try
                        {
                            collection.Save(filename);
                            collection.changed_not_saved = false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            SaveUnsaved();
        }
    }
}
