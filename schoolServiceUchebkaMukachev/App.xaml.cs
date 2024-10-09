using schoolServiceUchebkaMukachev.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace schoolServiceUchebkaMukachev
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SchoolServiceMukachevEntities db = new SchoolServiceMukachevEntities();
        public static Client client;
    }
}
