using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.Services.Concrete;
using KH.DataAccessLayer.ViewModels;
using KinderHouseApp.Tools.Abstract;
using KinderHouseApp.Tools.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections;

namespace KinderHouseApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private IDataService DataService { get; set; }
        public HomeWindow()
        {
            DataService = new DataService();
            Loaded += HomeWindow_Loaded;
            InitializeComponent();
        }
        private void HomeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dtgrdHome.ItemsSource = DataService.GetData<PupilVM>();
            dtgrdHome.Style = ChangeControlProperties<DataGrid>();
            dtgrdHome.ColumnHeaderStyle = ChangeControlProperties<DataGridColumnHeader>();
            ChangeHeaderText(new PupilHeader());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dtgrdHome.ItemsSource = DataService.GetData<PupilVM>();
            ChangeHeaderText(new PupilHeader());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dtgrdHome.ItemsSource = DataService.GetData<WorkerVM>();
            ChangeHeaderText(new WorkerHeader());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dtgrdHome.ItemsSource = DataService.GetData<PurchaseVM>();
            ChangeHeaderText(new PurchaseHeader());
        }
        private void ChangeHeaderText(Header header)
        {
            for (int i = 0; i < dtgrdHome.Columns.Count; i++)
            {
                dtgrdHome.Columns[i].Header = header.Name[i];
            }
        }

        private static Style ChangeControlProperties<T>() where T : class
        {
            return new Style()
            {
                TargetType = typeof(T),
                Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center),
                        new Setter(BackgroundProperty, Brushes.Yellow)
                    }
            };
        }


        private void dtgrdHome_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var currentViewModelKVP = GetCurrentViewModelAndType();
                var viewModelType = currentViewModelKVP.Key;
                var changedViewModel = ((ItemCollection)currentViewModelKVP.Value).SourceCollection;
                string caption = "Bildiriş!";
                string message = "Əminsinizmi?";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                var result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    DataService.UpdateData(changedViewModel, viewModelType, false);
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var currentViewModelKVP = GetCurrentViewModelAndType();
            object viewModel = GetInstance(currentViewModelKVP.Key);
            var dataFromCurrentDataGrid = GetDataFromCurrentDataGrid().Cast<object>().ToList(); ;
            dataFromCurrentDataGrid.Add(viewModel);
            dtgrdHome.ItemsSource = dataFromCurrentDataGrid;
        }

        private KeyValuePair<string, object> GetCurrentViewModelAndType()
        {
            var viewModelType = dtgrdHome.Items.CurrentItem.GetType().Name;
            var viewModel = dtgrdHome.Items;
            return new KeyValuePair<string, object>(viewModelType, viewModel);
        }
        private object GetInstance(string viewModelName)
        {
            var projectName = "KH.DataAccessLayer";
            var viewModelNamespace = $"{projectName}.ViewModels";
            var assembly = Assembly.Load(projectName);
            Type type = assembly.GetType($"{viewModelNamespace}.{viewModelName}");
            return Activator.CreateInstance(type);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var currentDataGrid = GetDataFromCurrentDataGrid();
            DataService.UpdateData(currentDataGrid, nameof(PupilVM), false);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            List<object> list = new List<object>() { dtgrdHome.SelectedItem };
            IEnumerable ienumerableList = list;
            var selectedItem = ienumerableList;
            var currentViewModelKVP = GetCurrentViewModelAndType();
            DataService.UpdateData(selectedItem, currentViewModelKVP.Key, true);
        }
        private IEnumerable GetDataFromCurrentDataGrid()
        {
            return dtgrdHome.ItemsSource;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
