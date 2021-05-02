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
        Employee Employee;

        public MainWindow()
        {
            InitializeComponent();

            Provider = new OracleProvider();

            //Console.WriteLine(DataGeneration.GetAllDataCount());

            //ContractRepository contractRepository = new ContractRepository(Provider);

            //Console.WriteLine(contractRepository.Get(95));

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
            Overlay.Content = new LoginPage(Provider);
        }
        public void CloseLoginPage(AccountInfo account, Client client)
        {
            Account = account;
            Client = client;
            SignedContractsB.Visibility = Visibility.Collapsed;
            OpenAccountPage(null, null);
            Overlay.Content = null;
        }
        public void CloseLoginPage(AccountInfo account, Employee employee)
        {
            Account = account;
            Employee = employee;
            RecommendationsB.Visibility = Visibility.Collapsed;
            AccountB.Visibility = Visibility.Collapsed;
            ContractsB.Visibility = Visibility.Collapsed;
            ServicesB.Visibility = Visibility.Collapsed;
            PhoneNumbersB.Visibility = Visibility.Collapsed;
            OpenSignedContractsPage(null, null);
            Overlay.Content = null;
        }


        public void OpenPaymentForContractPage(Contract contract)
        {
            Overlay.Content = new PaymentForContractPage(Provider, contract);
        }
        public void ClosePaymentForContractPage()
        {
            Overlay.Content = null; 
        }
        public void OpenExecuteContractPage()
        {
            Overlay.Content = new ExecuteСontractPage(Provider, Client);
        }
        public void OpenExecuteContractPage(TariffPlan tariffPlan)
        {
            Overlay.Content = new ExecuteСontractPage(Provider, Client, tariffPlan);
        }
        public void CloseExecuteContractPage()
        {
            Overlay.Content = null;
        }
        public void OpenExecuteServicePage()
        {
            Overlay.Content = new ExecuteServicePage(Provider, Client);
        }
        public void OpenExecuteServicePage(Contract contract, ServiceDescription serviceDescription)
        {
            Overlay.Content = new ExecuteServicePage(Provider, Client, contract, serviceDescription);
        }
        public void CloseExecuteServicePage()
        {
            Overlay.Content = null;
        }



        public void OpenAccountPage(object sender, EventArgs e)
        {
            Main.Content = new AccountPage(Provider, Client);
        }
        public void OpenContractsPage(object sender, EventArgs e)
        {
            Main.Content = new ContractsPage(Provider, Client);
        }
        public void OpenServicesPage(object sender, EventArgs e)
        {
            Main.Content = new ServicesPage(Provider, Client);
        }
        public void OpenRecommendationsPage(object sender, EventArgs e)
        {
            Main.Content = new RecommendationsPage(Provider, Client);
        }
        public void OpenPhoneNumbersPage(object sender, EventArgs e)
        {
            Main.Content = new PhoneNumbersPage(Provider, Client);
        }
        public void OpenSignedContractsPage(object sender, EventArgs e)
        {
            Main.Content = new SignedContractsPage(Provider, Employee);
        }
    }
}
