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
    /// Логика взаимодействия для ClientControl.xaml
    /// </summary>
    public partial class ClientControl : UserControl
    {
        private Service ser;
        private ClientService _clientService;
        public ClientControl(ClientService clientService)
        {
            InitializeComponent();
            
            ser = App.db.Service.FirstOrDefault(x => x.ID == clientService.ServiceID);
            
            _clientService = App.db.ClientService.FirstOrDefault(x => x.ServiceID == ser.ID);
            TitleServiceTB.Text = ser.Title.ToString();

            // Получить путь к папке "ресурс" относительно папки, в которой находится исполняемый файл
            var imagesBD = App.db.ServicePhoto.FirstOrDefault(x => x.ID == ser.ServicePhotoID).PhotoPath.ToString();
            string folderName = "schoolServiceUchebkaMukachev/Resource";
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string fullPath = System.IO.Path.Combine(projectDirectory, folderName, imagesBD);

            //Заменяем обратные слеши на прямые слеши
            ImageService.Source = new BitmapImage(new Uri(fullPath, UriKind.Absolute));

            if (ser.Discount != 0)
            {
                //Зачёркнутый текст
                textDecorate.Text = $"{ser.Cost.Value.ToString("0.#")}";
                textDecorate.TextDecorations = TextDecorations.Strikethrough;

                CostAndTimeTB.Text = $"{((((double?)ser.Cost) - ((double?)ser.Cost) * ser.Discount / 100)).ToString()} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = $"* скидка {ser.Discount.ToString()}%";
            }
            else
            {
                CostAndTimeTB.Text = $"{ser.Cost.Value.ToString("0.#")} рублей за {ser.DurationInMinutes.ToString()} минут";
                DiscountTB.Text = "";
            }
            StartTimeTb.Text = "Время начала " + Convert.ToString(_clientService.StartTime);
            
        }
        
    }
}
