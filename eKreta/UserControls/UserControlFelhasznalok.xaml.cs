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
using eKreta.Models;
using eKreta.Services;

namespace eKreta.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlFelhasznalok.xaml
    /// </summary>
    public partial class UserControlFelhasznalok : UserControl
    {
        List<Models.Felhasznalo> felhasznalok;
        Felhasznalo kivalasztottFelhasznalo;
        public UserControlFelhasznalok()
        {
            InitializeComponent();
            jogosultsagComboBox.ItemsSource = Enum.GetValues(typeof(Szerepkor));
            AdatbazisLekerdezes();
            felhasznalok = new List<Models.Felhasznalo>();
        }

        private void AdatbazisLekerdezes()
        {
            //Elemek 0-ra
            var felhasznaloRepo = new GenericRepository<Models.Felhasznalo>(App.databasePath);
            var lekerdezes = felhasznaloRepo.GetAll();

            mentesBtn.Visibility = Visibility.Visible;
            modBtn.Visibility = Visibility.Hidden;
            torlesBtn.Visibility = Visibility.Hidden;
        }

        private void datagridFelhasznalo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mentesBtn.Visibility = Visibility.Hidden;
            modBtn.Visibility = Visibility.Visible;
            torlesBtn.Visibility = Visibility.Visible;

            if (datagridFelhasznalo.SelectedItem != null)
            {
                kivalasztottFelhasznalo = (Models.Felhasznalo)datagridFelhasznalo.SelectedItem;
                felhasznaloNevTxt.Text = kivalasztottFelhasznalo.FelhasznaloNev;
                teljesNevTxt.Text = kivalasztottFelhasznalo.TeljesNev;
                jogosultsagComboBox.Text = ((Szerepkor)kivalasztottFelhasznalo.Szerepkor).ToString();
            }
        }

        private void mentesBtn_Click(object sender, RoutedEventArgs e)
        {
            string kivalasztottSzerepkorNev = jogosultsagComboBox.SelectedItem.ToString();
            Szerepkor kivalasztottSzerepkor = (Szerepkor)Enum.Parse(typeof(Szerepkor), kivalasztottSzerepkorNev);
            int kivalasztottSzerepkorId = (int)kivalasztottSzerepkor;

            Models.Felhasznalo ujFelhasznalo = new Felhasznalo(felhasznaloNevTxt.Text, teljesNevTxt.Text, PasswordHelper.HashPassword(jelszoTxt.Password), kivalasztottSzerepkorId);

            var felhasznaloRepo = new GenericRepository<Models.Felhasznalo>(App.databasePath);
            felhasznaloRepo.Insert(ujFelhasznalo);
            AdatbazisLekerdezes();
        }

        private void torlesBtn_Click(object sender, RoutedEventArgs e)
        {
            var felhasznaloRepo = new GenericRepository<Models.Felhasznalo>(App.databasePath);
            felhasznaloRepo.Delete(kivalasztottFelhasznalo);
            AdatbazisLekerdezes();
        }

        private void modBtn_Click(object sender, RoutedEventArgs e)
        {
            kivalasztottFelhasznalo.FelhasznaloNev = felhasznaloNevTxt.Text;
            kivalasztottFelhasznalo.TeljesNev = teljesNevTxt.Text;
            string kivalasztottSzerepkorNev = jogosultsagComboBox.SelectedItem.ToString();
            Szerepkor kivalasztottSzerepkor = (Szerepkor)Enum.Parse(typeof(Szerepkor), kivalasztottSzerepkorNev);
            kivalasztottFelhasznalo.Szerepkor = (int)kivalasztottSzerepkor;
            if (jelszoTxt.Password != string.Empty)
            {
                kivalasztottFelhasznalo.Jelszo = PasswordHelper.HashPassword(jelszoTxt.Password);
            }

            var felhasznaloRepo = new GenericRepository<Models.Felhasznalo>(App.databasePath);
            felhasznaloRepo.Update(kivalasztottFelhasznalo);
            AdatbazisLekerdezes();

        }
    }
}
