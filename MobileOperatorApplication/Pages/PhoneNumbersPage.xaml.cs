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
    /// Логика взаимодействия для PhoneNumbersPage.xaml
    /// </summary>
    public partial class PhoneNumbersPage : Page
    {
        OracleProvider Provider;
        Client Client;
        public PhoneNumbersPage(OracleProvider oracleProvider, Client client)
        {
            InitializeComponent();
            Provider = oracleProvider;
            Client = client;
            FillWithInfo(null, null);
        }

        void FillWithInfo(object sender, EventArgs e)
        {
            ClientRepository clientRepository = new ClientRepository(Provider);
            IEnumerable<Contract> contracts = clientRepository.GetAllContracts(Client.ID);
            ContractRepository contractRepository = new ContractRepository(Provider);
            for (int i = 0; i < contracts.Count(); i++)
            {
                IEnumerable<PhoneNumber> phoneNumbers = contractRepository.GetAllPhoneNumbers(contracts.ElementAt(i).ID);
                for (int j = 0; j < phoneNumbers.Count(); j++)
                {
                    PhoneNumbersStack.Children.Add(GetPhonenUmberRow(phoneNumbers.ElementAt(j)));
                }
            }
        }

        Grid GetPhonenUmberRow(PhoneNumber phoneNumber)
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);
            gridRow.Height = 30;

            for (int i = 0; i < 2; i++)
            {
                ColumnDefinition cd1 = new ColumnDefinition();
                cd1.Width = new GridLength(0.5, GridUnitType.Star);
                gridRow.ColumnDefinitions.Add(cd1);
            }
            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd2);


            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 3; i++)
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

            textBlocks.ElementAt(0).Text = phoneNumber.ID.ToString();
            textBlocks.ElementAt(1).Text = phoneNumber.CONTRACT_ID.ToString();
            textBlocks.ElementAt(2).Text = phoneNumber.PHONE_NUMBER;

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }
    }
}
