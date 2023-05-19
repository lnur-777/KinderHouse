using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.Services.Concrete;
using KH.DataAccessLayer.ViewModels;
using KinderHouseApp.Tools.Abstract;
using KinderHouseApp.Tools.Concrete;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

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
                var viewModelType = dtgrdHome.Items.CurrentItem.GetType().Name;
                var changedViewModel = dtgrdHome.Items.SourceCollection;
                string caption = "Bildiriş!";
                string message = "Əminsinizmi?";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                var result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    DataService.UpdateData(changedViewModel,viewModelType);
                }
            }
        }
    }
}
