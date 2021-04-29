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
    /// Логика взаимодействия для ContractsPage.xaml
    /// </summary>
    public partial class ContractsPage : Page
    {
        OracleProvider Provider;
        Client Client;

        public ContractsPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            GetContractBalances(null, null);
        }

        void GetContractBalances(object sender, EventArgs e)
        {
            ClientRepository clientRepository = new ClientRepository(Provider);

            IEnumerable<Contract> contracts = clientRepository.GetAllContracts(Client.ID);

            ContractsStack.Children.Clear();

            foreach (Contract contract in contracts)
            {
                ContractsStack.Children.Add(GetContractRow(contract));
            }
        }

        Grid GetContractRow(Contract contract)
        {
            ContractRepository contractRepository = new ContractRepository(Provider);
            EmployeeRepository employeeRepository = new EmployeeRepository(Provider);
            TariffPlanRepository tariffRepository = new TariffPlanRepository(Provider);

            TariffPlan tariff = tariffRepository.Get(contract.TARIFF_ID);
            Employee employee = employeeRepository.Get(contract.EMPLOYEE_ID);
            float balance = contractRepository.GetContractBalance(contract.ID);

            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(0.5, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            for (int i = 0; i < 5; i++)
            {
                ColumnDefinition cd2 = new ColumnDefinition();
                cd2.Width = new GridLength(1, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd2);
            }

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 6; i++)
            {
                TextBlock tb = new TextBlock();
                tb.TextWrapping = TextWrapping.Wrap;
                tb.FontFamily = new FontFamily("Unispace");
                tb.FontSize = 12;
                Grid.SetColumn(tb, i);
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                textBlocks.Add(tb);
            }

            textBlocks.ElementAt(0).Text = contract.ID.ToString();
            textBlocks.ElementAt(1).Text = tariff.TARIFF_NAME;
            textBlocks.ElementAt(2).Text = Client.FULL_NAME;
            textBlocks.ElementAt(3).Text = employee.FULL_NAME;
            textBlocks.ElementAt(4).Text = contract.SIGNING_DATETIME.ToString();
            textBlocks.ElementAt(5).Text = balance.ToString();

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }
    }
}
