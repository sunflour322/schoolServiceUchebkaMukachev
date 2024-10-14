using schoolServiceUchebkaMukachev.Controls;
using schoolServiceUchebkaMukachev.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

namespace schoolServiceUchebkaMukachev.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientMainPage.xaml
    /// </summary>
    public partial class ClientMainPage : Page
    {
        
        
        public List<ClientService> currentUserServices;
        public ClientMainPage()
        {
            InitializeComponent();
            

            // Устанавливаем минимальное значение для DiscountRs, если найденное минимальное больше нуля
            currentUserServices = App.db.ClientService.Where(x => x.ClientID == App.client.ID).ToList();
            UpdatePage(currentUserServices);
            
            
        }
        
        public void UpdatePage(List<ClientService> clientServices)
        {
            ServiceWpar.Children.Clear();
            foreach (var item in clientServices)
            {
                ServiceWpar.Children.Add(new ClientControl(item));
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
