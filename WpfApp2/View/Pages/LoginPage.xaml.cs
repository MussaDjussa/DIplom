﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Windows.Threading;

namespace WpfApp2.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer dispatcherTimerDialog = new DispatcherTimer();

        public LoginPage()
        {
            InitializeComponent();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToShortTimeString();
            Date.Text = DateTime.Now.ToLongDateString();
            
            if(DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 17)
            {
                Greeting.Text = "Добрый день";
            }
            if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour > 12 )
            {
                Greeting.Text = "Добрый вечер";
            }
            if (DateTime.Now.Hour >= 21)
            {
                Greeting.Text = "Доброй ночи";
            }
            if(DateTime.Now.Hour <= 5)
            {
                Greeting.Text = "Доброй ночи";
            }

            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour <= 12)
            {
                Greeting.Text = "Доброй утро";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
        private void Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(LoginBox.Text, "[^A-z-0-9]"))
            {
                Dialog.IsOpen = true;
                LoginBox.Text = LoginBox.Text.Remove(LoginBox.Text.Length - 1);
                LoginBox.SelectionStart = LoginBox.Text.Length;
            }
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(Password.Text, "[^A-z-0-9]"))
            {
                Dialog.IsOpen = true;
                Password.Text = Password.Text.Remove(Password.Text.Length - 1);
                Password.SelectionStart = Password.Text.Length;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Dialog.IsOpen = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var findUser = App.db.User.ToList().Find(q=>q.Login == LoginBox.Text && q.Password == Password.Text);
            if(findUser != null)
            {
                Username.Text =  $"Здравствуйте, {findUser.Name}";
                DialogSuccess.IsOpen = true;
                dispatcherTimerDialog.Tick += DispatcherTimerDialog_Tick;
                dispatcherTimerDialog.Interval = TimeSpan.FromMilliseconds(850);
                dispatcherTimerDialog.Start();

                switch(findUser.RoleID)
                {
                    case 1:

                        break;
                    case 2:

                        break;
                }
            }
        }

        private void DispatcherTimerDialog_Tick(object sender, EventArgs e)
        {
            DialogSuccess.IsOpen = false;
            dispatcherTimerDialog.Stop();
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            DialogFail.IsOpen = false;
        }

        private void Success_Click(object sender, RoutedEventArgs e)
        {
            DialogSuccess.IsOpen = false;
        }
    }
}
