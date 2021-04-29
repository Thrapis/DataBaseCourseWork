using MobileOperatorApplication.Data;
using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Pages;
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
        OracleProvider Provider;
        AccountInfo Account;
        Client Client;

        public MainWindow()
        {
            InitializeComponent();

            Provider = new OracleProvider();

           // ContractRepository contractRepository = new ContractRepository(Provider);

            //Console.WriteLine(contractRepository.Get(1));

            /*ClientRepository repository = new ClientRepository();

            for (int i = 0; i < 100; i++)
            {
                IEnumerable<TariffPlan> tariffPlans = repository.GetTariffRecommendations(i, 10);
                Console.WriteLine("Client " + i + ": " + tariffPlans.Count());
            }*/

            OpenLoginPage();
        }

        public void OpenLoginPage()
        {
            Main.Content = new LoginPage(Provider);
        }
        public void CloseLoginPage(AccountInfo account)
        {
            Account = account;
            ClientRepository clientRepository = new ClientRepository(Provider);
            Client = clientRepository.Get(Account.LOGIN);
            Main.Content = null;
        }

        public void OpenAccountPage(object sender, EventArgs e)
        {
            Main.Content = new AccountPage();
        }
        public void OpenContractsPage(object sender, EventArgs e)
        {
            Main.Content = new ContractsPage(Provider, Client);
        }
    }
}
