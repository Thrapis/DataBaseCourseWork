using MobileOperatorApplication.Model;
using MobileOperatorApplication.Oracle;
using MobileOperatorApplication.Repository;
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

namespace MobileOperatorApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        OracleProvider Provider;
        Client Client;
        public AccountPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillWithInfo();
        }

        void FillWithInfo()
        {
            ClientRepository clientRepository = new ClientRepository(Provider);
            
            FullName.Text = Client.FULL_NAME;
            Login.Text = Client.ACCOUNT_LOGIN;
            PassNum.Text = Client.PASSPORT_NUMBER.Substring(0, 3) + "******";
            ContractsCount.Text = clientRepository.GetAllContracts(Client.ID).Count().ToString();
            ServicesCount.Text = clientRepository.GetAllServices(Client.ID).Count().ToString();
        }
    }
}
