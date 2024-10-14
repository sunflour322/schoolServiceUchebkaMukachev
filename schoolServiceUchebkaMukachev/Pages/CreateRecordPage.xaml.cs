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


            // Устанавливаем минимальное значение для DiscountRs, если найденное минимальное больше нуля
            currentUserServices = App.db.ClientService.ToList();
            UpdatePage(currentUserServices);


        }

        public void UpdatePage(List<ClientService> clientServices)
        {
            ServiceWpar.Children.Clear();
            foreach (var item in clientServices)
            {
                ServiceWpar.Children.Add(new RecordControl(item));
            }
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
