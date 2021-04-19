using MobileOperatorApplication.Data;
using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace MobileOperatorApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //OracleProvider oracleProvider = new OracleProvider();
            //OracleProvider oracleProvider2 = new OracleProvider();
            //Console.WriteLine(oracleProvider.GetAccount("Thrapis", "Mrlololoshka12"));
            //Console.WriteLine(oracleProvider2.GetAccount("Thrapis", "Mrlololoshka12"));
            //Console.WriteLine(new DateTime(2001,2,8,14,30,45).ToString("yyyy-MM-dd HH:mm:ss"));
            //DataGeneration.GeneratePosts();

            //Console.WriteLine("Generated: " + DataGeneration.GeneratePayments(max_payments: 70));
            //PaymentRepository repository = new PaymentRepository();
            //Console.WriteLine(repository.GetAll().Count());

            Console.WriteLine(DataGeneration.GetAllDataCount());

            //Console.WriteLine(new TimeSpan(1, 22, 34, 45).ToString(@"dd\ hh\:mm\:ss"));
        }
    }
}
