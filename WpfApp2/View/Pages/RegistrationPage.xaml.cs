using System;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public RegistrationPage()
        {
            InitializeComponent();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Time.Text = DateTime.Now.ToShortTimeString();
            Date.Text = DateTime.Now.ToLongDateString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
            if (string.IsNullOrEmpty(NameBox.Text) || string.IsNullOrWhiteSpace(LoginBox.Text)
                || string.IsNullOrEmpty(Password.Text))
            {
                DialogEmpty.IsOpen = true;
            }
            else
            {
                if(LoginBox.Text == Password.Text)
                {
                    DialogDifferent.IsOpen = true;
                }
                else
                {
                    if(LoginBox.Text.Length < 2 || Password.Text.Length < 2)
                    {
                        DialogLength.IsOpen = true;
                    }
                    else
                    {
                        var findUser = App.db.User.ToList().Find(q => q.Login == LoginBox.Text);
                        if (findUser == null)
                        {
                            App.db.User.Add(new Model.User
                            {
                                Name = NameBox.Text,
                                Password = Password.Text,
                                Login = LoginBox.Text,
                                RoleID = 2,
                                isActive = true
                            });
                            App.db.SaveChanges();
                            DialogSuccess.IsOpen = true;
                        }
                        else
                        {
                            DialogError.IsOpen = true;
                        }
                    }

                }
            }
        }

        private void CloseButton_Click_1(object sender, RoutedEventArgs e)
        {
            DialogFail.IsOpen = false;
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch(NameBox.Text, "[^А-я-A-z]"))
            {
                Dialog2.IsOpen = true;
                NameBox.Text = NameBox.Text.Remove(NameBox.Text.Length - 1);
                NameBox.SelectionStart = NameBox.Text.Length;
            }
        }

        private void CloseButton_Click_2(object sender, RoutedEventArgs e)
        {
            Dialog2.IsOpen = false;
        }

        private void DialogEmptyButton_Click(object sender, RoutedEventArgs e)
        {
            DialogEmpty.IsOpen = false;
        }

        private void Success_Click(object sender, RoutedEventArgs e)
        {
            DialogEmpty.IsOpen = false;
            NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogEmpty.IsOpen = false;
        }

        private void DialogErrorButton_Click(object sender, RoutedEventArgs e)
        {
            DialogError.IsOpen = false;
        }

        private void Different_Click(object sender, RoutedEventArgs e)
        {
            DialogDifferent.IsOpen = false;
        }

        private void MinLength_Click(object sender, RoutedEventArgs e)
        {
            DialogLength.IsOpen = false;
        }
    }
}
