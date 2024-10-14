using schoolServiceUchebkaMukachev.Controls;
using schoolServiceUchebkaMukachev.DB;
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

namespace schoolServiceUchebkaMukachev.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreateRecordPage.xaml
    /// </summary>
    public partial class CreateRecordPage : Page
    {
        public List<ClientService> currentUserServices;
        public CreateRecordPage()
        {
            InitializeComponent();


            
            currentUserServices = App.db.ClientService.ToList();
            RecordPage(currentUserServices);


        }

        public void RecordPage(List<ClientService> clientServices)
        {
            ServiceWpar.Children.Clear();
            foreach (var item in clientServices)
            {
                ServiceWpar.Children.Add(new RecordControl(item));
            }
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Now.Date; // Начало сегодняшнего дня
            DateTime endOfTomorrow = today.AddDays(2).Date.AddTicks(-1); // Конец завтрашнего дня (23:59:59)

            var filteredRecords = App.db.ClientService
                .Where(x => x.StartTime >= today && x.StartTime <= endOfTomorrow) // Убедитесь, что год 2024
                .ToList();
            NearRecord(filteredRecords);

        }
        public void NearRecord(List<ClientService> clientServices)
        {
            ServiceWpar.Children.Clear();
            foreach (var item in clientServices)
            {
                ServiceWpar.Children.Add(new NearRecordPage(item));
            }
        }
    }
}
