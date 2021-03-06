﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Specialized;
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
        public CustomDataCollection customDataCollection;
        public static RoutedCommand AddCommand = new RoutedCommand("Add", typeof(WpfApp1.MainWindow));
        public MainWindow()
        {
            InitializeComponent();
            collection = new V3MainCollection();
            CollectionView collViewGrid = new CollectionView(collection);
            collViewGrid.Filter = collection.FilterByGrid;
            this.DataContext = collection;
            lisBox_DataOnGrid.DataContext = collViewGrid;
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
                string output = "При открытии файла или создании объекта V3DataCollection проиозшла ошибка и было брошено исключение. Причина: \n" + ex.Message;
                MessageBox.Show(output);
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.Remove((V3Data)lisBox_Main.SelectedItem);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
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


        private void lisBox_DataCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                customDataCollection = new CustomDataCollection(lisBox_DataCollection.SelectedItem as V3DataCollection);
                item_x.DataContext = customDataCollection;
                item_y.DataContext = customDataCollection;
                item_val.DataContext = customDataCollection;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddCustom_Click(object sender, RoutedEventArgs e)
        {
            if (customDataCollection !=null)
            {
                customDataCollection.AddDataitem();
                collection.changed();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
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


        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
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
        private void SaveCommandHandler_Executed(object sender, ExecutedRoutedEventArgs e)
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

        private void SaveCommandHandler_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((collection != null) && (collection.changed_not_saved == true))
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }

        }
        private void DeleteCommandHandler_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((lisBox_Main != null) && (lisBox_Main.SelectedItem != null))
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void DeleteCommandHandler_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                collection.Remove((V3Data)lisBox_Main.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddCommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (customDataCollection != null)
            {
                customDataCollection.AddDataitem();
                collection.changed();
            }

        }
        private void CanAddCommandHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            if ((lisBox_DataCollection == null) || (lisBox_DataCollection.SelectedItem == null))
            {
                e.CanExecute = false;
                return;
            }
            if (Validation.GetHasError(item_val) == true)
            {
                e.CanExecute = false;
                return;
            }
            if (Validation.GetHasError(item_x) == true)
            {
                e.CanExecute = false;
                return;
            }
            if (Validation.GetHasError(item_y) == true)
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = true;
            return;

        }
    }
}
