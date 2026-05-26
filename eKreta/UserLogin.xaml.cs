using eKreta.Models;
using eKreta.Services;
using SQLite;
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
using System.Windows.Shapes;

namespace eKreta
{
    /// <summary>
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string felhasznalonev = loginUserTxt.Text;
            string jelszo = PasswordHelper.HashPassword(loginPassTxt.Password);
            if (string.IsNullOrEmpty(felhasznalonev) || string.IsNullOrEmpty(jelszo))
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
                {
                    var user = conn.Table<Felhasznalo>().FirstOrDefault(f => f.FelhasznaloNev == felhasznalonev && f.Jelszo == jelszo);
                    if (user != null)
                    {
                        if (user.Jelszo == jelszo)
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Belépés megtagadva!");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Belépés megtagadva!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Kérem adjon meg minden adatot!");
            }
        }
    }
}
