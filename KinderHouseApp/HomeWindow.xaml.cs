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
using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Repositories.Concrete;
using KH.DataAccessLayer.Repositories.Abstract;

namespace KinderHouseApp
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private ElnurhContext _context;
        private IPupilRepository _pupilRepository;
        private ISectorRepository _sectorRepository;
        private IPurchaseRepository _purchaseRepository;
        private IWorkerRepository _workerRepository;
        private ILessonRepository _lessonRepository;
        private IPositionRepository _positionRepository;
        private IService _service;
        private static BaseViewModel selectedModel;
        public HomeWindow()
        {
            _context ??= new();
            _pupilRepository ??= new PupilRepository(_context);
            _sectorRepository ??= new SectorRepository(_context);
            _purchaseRepository ??= new PurchaseRepository(_context);
            _workerRepository ??= new WorkerRepository(_context);
            _lessonRepository ??= new LessonRepository(_context);
            _positionRepository ??= new PositionRepository(_context);
            CreateDynamicService(null, _pupilRepository, _sectorRepository);
            Loaded += HomeWindow_Loaded;
            InitializeComponent();
        }
        private void HomeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dtgrdHome.ItemsSource = _service.GetAll();
            dtgrdHome.Style = ChangeControlProperties<DataGrid>();
            dtgrdHome.ColumnHeaderStyle = ChangeControlProperties<DataGridColumnHeader>();
            ChangeHeaderText(new PupilHeader());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateDynamicService(null, _pupilRepository, _sectorRepository);
            dtgrdHome.ItemsSource = _service.GetAll();
            ChangeHeaderText(new PupilHeader());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateDynamicService("WorkerService", _workerRepository, _lessonRepository, _positionRepository);
            dtgrdHome.ItemsSource = _service.GetAll();
            ChangeHeaderText(new WorkerHeader());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CreateDynamicService("PurchaseService", _purchaseRepository, _pupilRepository);
            dtgrdHome.ItemsSource = _service.GetAll();
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
                string caption = "Bildiriş!";
                string message = "Əminsinizmi?";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Information;
                var result = MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    var isUpdated =_service.Update(selectedModel);
                    if (isUpdated)
                    {
                        MessageBox.Show("Uğurla silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Səhv baş verdi.");
                    }
                    dtgrdHome.ItemsSource = _service.GetAll();
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
            var isUpdated = _service.UpdateAll(currentDataGrid);
            if (isUpdated)
            {
                MessageBox.Show("Uğurla silindi.");
            }
            else
            {
                MessageBox.Show("Səhv baş verdi.");
            }
            dtgrdHome.ItemsSource = _service.GetAll();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var isUpdated = _service.Delete(selectedModel);
            if (isUpdated)
            {
                MessageBox.Show("Uğurla silindi.");
            }
            else
            {
                MessageBox.Show("Səhv baş verdi.");
            }
            dtgrdHome.ItemsSource = _service.GetAll();
        }
        private IEnumerable GetDataFromCurrentDataGrid()
        {
            return dtgrdHome.ItemsSource;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CreateDynamicService(string? serviceName = null, params IRepository[] repositories)
        {
            serviceName = serviceName ?? "PupilService";
            var projectName = "KH.DataAccessLayer";
            var serviceNamespace = $"{projectName}.Services.Concrete";
            var assembly = Assembly.Load(projectName);
            Type type = assembly.GetType($"{serviceNamespace}.{serviceName}");
            _service = Activator.CreateInstance(type, repositories) as IService;
        }

        private void dtgrdHome_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            selectedModel = dtgrdHome.SelectedItem as BaseViewModel;
        }
    }
}
