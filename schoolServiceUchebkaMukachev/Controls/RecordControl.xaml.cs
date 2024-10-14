using schoolServiceUchebkaMukachev.DB;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace schoolServiceUchebkaMukachev.Controls
{
    /// <summary>
    /// Логика взаимодействия для RecordControl.xaml
    /// </summary>
    public partial class RecordControl : UserControl
    {
        private Service ser;
        private ClientService _clientService;
        private Client _client;
        public RecordControl(ClientService clientService)
        {
            InitializeComponent();
            
            ser = App.db.Service.FirstOrDefault(x => x.ID == clientService.ServiceID);
            var clients = App.db.Client.ToList();
            ClientComboBox.ItemsSource = clients;
            _clientService = App.db.ClientService.FirstOrDefault(x => x.ServiceID == ser.ID);
            TitleServiceTB.Text = ser.Title.ToString();

            // Получить путь к папке "ресурс" относительно папки, в которой находится исполняемый файл
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "schoolServiceUchebkaMukachev/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            //Заменяем обратные слеши на прямые слеши
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            CostAndTimeTB.Text = ser.DurationInMinutes.ToString() + " минут";
           

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClientService clientService = new ClientService();
            clientService.ClientID = _client.ID;
            clientService.ServiceID = ser.ID;
            clientService.StartTime = DateTime.Today;
            clientService.Comment = null;
            App.db.ClientService.Add(clientService);
            App.db.SaveChanges();
        }

        private void ClientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedClient = (Client)ClientComboBox.SelectedItem;
            

            if (selectedClient != null)
            {
                
                var dbClient = App.db.Client.FirstOrDefault(c => c.LastName == selectedClient.LastName 
                                                                && c.FirstName == selectedClient.FirstName
                                                                && c.Patronymic == selectedClient.Patronymic);
                _client = dbClient;
                if (dbClient != null)
                {
                    MessageBox.Show($"Клиент найден в базе данных: {dbClient.LastName} {dbClient.FirstName} {dbClient.Patronymic}");
                    
                }
                else
                {
                    MessageBox.Show("Клиент не найден в базе данных.");
                }
            }
        }
    }
}
