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
using KH.DataAccessLayer;
using KH.DataAccessLayer.Models;
using KH.DataAccessLayer.Services.Abstract;
using KH.DataAccessLayer.Services.Concrete;

namespace KinderHouseApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private ILoginService LoginService { get; set; }
        private ElnurhContext Context;
        public LoginWindow()
        {
            LoginService = new LoginService();
            Context = new();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var userName = TxtBoxUname.Text;
            var passWord = TxtBoxPass.Text;
            var message = "";
            User user = new();
            if (!LoginService.Login(userName, passWord, out user))
            {
                string caption = "Xəta!!!";
                message = "İstifadəçi adı və ya parol səhvdir!";
                InvokeMessageBox(message, caption);
            }
            HomeWindow homeWindow = new();
            LoginWindow loginWindow = new();
            homeWindow.Show();
            loginWindow.Hide();
        }

        private static void InvokeMessageBox(string message, string caption)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Warning;

            MessageBox.Show(message, caption, button, icon, MessageBoxResult.Yes);
        }
    }
}
