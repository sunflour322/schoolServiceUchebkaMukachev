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
    /// Логика взаимодействия для NearRecordPage.xaml
    /// </summary>
    public partial class NearRecordPage : UserControl
    {
        private Service ser;
        private ClientService _clientService;
        private Client _client;
        
        public NearRecordPage(ClientService clientService)
        {
            InitializeComponent();

            ser = App.db.Service.FirstOrDefault(x => x.ID == clientService.ServiceID);
            _client = App.db.Client.FirstOrDefault(x => x.ID == clientService.ClientID);
            _clientService = App.db.ClientService.FirstOrDefault(x => x.ServiceID == ser.ID && x.ClientID == _client.ID);
            TitleServiceTB.Text = ser.Title.ToString();

            // Получить путь к папке "ресурс" относительно папки, в которой находится исполняемый файл
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "schoolServiceUchebkaMukachev/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            // Заменяем обратные слеши на прямые слеши
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));
            CostAndTimeTB.Text = ser.DurationInMinutes.ToString() + " минут";
            ClientTb.Text = _client.FirstName + " " + _client.LastName + " " + _client.Patronymic;
            DateTb.Text = _clientService.StartTime.ToString();

            // Отображение времени до начала записи
            DateTime now = DateTime.Now;
            TimeSpan timeUntilStart = _clientService.StartTime - now;

            if (timeUntilStart.TotalHours >= 1)
            {
                TimeUntilStartTB.Text = $"{timeUntilStart.Hours} час(ов) и {timeUntilStart.Minutes} минут до начала";
            }
            else if (timeUntilStart.TotalMinutes > 0)
            {
                TimeUntilStartTB.Text = $"{timeUntilStart.Minutes} минут до начала";
            }
            else
            {
                TimeUntilStartTB.Text = "Запись уже началась";
            }

            
            if (timeUntilStart.TotalMinutes > 0 && timeUntilStart.TotalMinutes < 60)
            {
                TimeUntilStartTB.Foreground = new SolidColorBrush(Colors.Red);
            }

        }
    }
}
