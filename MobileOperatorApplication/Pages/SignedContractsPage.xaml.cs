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
    /// Логика взаимодействия для SignedContractsPage.xaml
    /// </summary>
    public partial class SignedContractsPage : Page
    {
        OracleProvider Provider;
        Employee Employee;
        IEnumerable<Contract> Contracts;

        public SignedContractsPage(OracleProvider oracleProvider, Employee employee)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Employee = employee;
            FillWithInfo(null, null);
        }

        void FillWithInfo(object sender, EventArgs e)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository(Provider);

            Contracts = employeeRepository.GetAllSignedContracts(Employee.ID);

            ContractsStack.Children.Clear();

            for (int i = 0; i < Contracts.Count(); i++)
            {
                ContractsStack.Children.Add(GetContractRow(Contracts.ElementAt(i)));
            }
        }

        Grid GetContractRow(Contract contract)
        {
            ContractRepository contractRepository = new ContractRepository(Provider);
            ClientRepository clientRepository = new ClientRepository(Provider);
            TariffPlanRepository tariffRepository = new TariffPlanRepository(Provider);

            TariffPlan tariff = tariffRepository.Get(contract.TARIFF_ID);
            Client client = clientRepository.Get(contract.CLIENT_ID);
            float balance = contractRepository.GetContractBalance(contract.ID);

            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(0.5, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            for (int i = 0; i < 6; i++)
            {
                ColumnDefinition cd2 = new ColumnDefinition();
                cd2.Width = new GridLength(1, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd2);
            }

            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 7; i++)
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
            textBlocks.ElementAt(2).Text = client.FULL_NAME;
            textBlocks.ElementAt(3).Text = Employee.FULL_NAME;
            textBlocks.ElementAt(4).Text = contract.SIGNING_DATETIME.ToString();
            textBlocks.ElementAt(5).Text = tariff.TARIFF_AMOUNT.ToString();
            textBlocks.ElementAt(6).Text = balance.ToString();

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }
    }
}
