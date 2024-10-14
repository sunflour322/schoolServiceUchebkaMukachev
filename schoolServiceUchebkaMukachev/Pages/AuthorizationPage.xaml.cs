using schoolServiceUchebkaMukachev.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace schoolServiceUchebkaMukachev.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<Client> clients { get; set; }
        
        public AuthorizationPage()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if(LoginTb.Text == "0000" && PasswordTb.Text == "0000")
            {
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                string login = LoginTb.Text.Trim();
                string password = PasswordTb.Text.Trim();
                clients = new List<Client>(App.db.Client.ToList());
                App.client = clients.FirstOrDefault(i => i.FirstName == login && i.Password == password);
                if (App.client != null)
                {
                    
                    NavigationService.Navigate(new ClientMainPage());
                }
                else
                    MessageBox.Show("Такого сотрудника нет!!!");
            }
        }

        
    }
}
