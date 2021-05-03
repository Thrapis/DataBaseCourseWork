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
    /// Логика взаимодействия для DebitsAndPaymentsPage.xaml
    /// </summary>
    public partial class DebitsAndPaymentsPage : Page
    {
        OracleProvider Provider;
        Client Client;
        SortedList<long, object> DebitsAndPayments;
        int PageSize = 20;

        public DebitsAndPaymentsPage(OracleProvider oracleProvider, Client client)
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
            DebitsAndPayments = new SortedList<long, object>();
            for (int i = 0; i < contracts.Count(); i++)
            {
                IEnumerable<Debit> debits = contractRepository.GetAllDebits(contracts.ElementAt(i).ID);
                IEnumerable<Payment> payments = contractRepository.GetAllPayments(contracts.ElementAt(i).ID);
                for (int j = 0; j < debits.Count(); j++)
                {
                    Debit debit =  debits.ElementAt(j);
                    while (DebitsAndPayments.ContainsKey(debit.DEBIT_DATETIME.Ticks))
                        debit.DEBIT_DATETIME = debit.DEBIT_DATETIME.AddTicks(1);
                    DebitsAndPayments.Add(debit.DEBIT_DATETIME.Ticks, debit);
                }
                for (int j = 0; j < payments.Count(); j++)
                {
                    Payment payment = payments.ElementAt(j);
                    while (DebitsAndPayments.ContainsKey(payment.PAYMENT_DATETIME.Ticks))
                        payment.PAYMENT_DATETIME = payment.PAYMENT_DATETIME.AddTicks(1);
                    DebitsAndPayments.Add(payment.PAYMENT_DATETIME.Ticks, payment);
                }
            }

            PageCounter.Minimum = 1;
            PageCounter.Maximum = (int)Math.Ceiling((double)DebitsAndPayments.Count / PageSize);
            PageCounter.Value = 1;
        }

        void PageChanged(object sender, EventArgs e)
        {
            if (PageCounter.Value == null)
                return;
            DebitsAndPaymentsStack.Children.Clear();
            for (int i = ((int)PageCounter.Value - 1) * PageSize; i < DebitsAndPayments.Count() && i < (int)PageCounter.Value * PageSize; i++)
            {
                DebitsAndPaymentsStack.Children.Add(GetChangeRow(DebitsAndPayments.Reverse().ElementAt(i).Value));
            }
        }

        Grid GetChangeRow(object debitOrPayment)
        {
            Grid gridRow = new Grid();
            gridRow.Background = new SolidColorBrush(Colors.LightGray);
            gridRow.Height = 30;

            ColumnDefinition cd1 = new ColumnDefinition();
            cd1.Width = new GridLength(0.6, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd1);
            ColumnDefinition cd2 = new ColumnDefinition();
            cd2.Width = new GridLength(0.8, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd2);
            ColumnDefinition cd3 = new ColumnDefinition();
            cd3.Width = new GridLength(1, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd3);
            ColumnDefinition cd4 = new ColumnDefinition();
            cd4.Width = new GridLength(2, GridUnitType.Star);
            gridRow.ColumnDefinitions.Add(cd4);


            List<TextBlock> textBlocks = new List<TextBlock>();

            for (int i = 0; i < 4; i++)
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

            if (debitOrPayment.GetType() == typeof(Debit))
            {
                Debit debit = debitOrPayment as Debit;
                textBlocks.ElementAt(0).Text = debit.CONTRACT_ID.ToString();
                textBlocks.ElementAt(1).Text = "-" + debit.DEBIT_AMOUNT.ToString() + " BYN";
                textBlocks.ElementAt(2).Text = debit.DEBIT_DATETIME.ToString();
                textBlocks.ElementAt(3).Text = debit.REASON;
            }
            else if (debitOrPayment.GetType() == typeof(Payment))
            {
                Payment payment = debitOrPayment as Payment;
                textBlocks.ElementAt(0).Text = payment.CONTRACT_ID.ToString();
                textBlocks.ElementAt(1).Text = payment.PAYMENT_AMOUNT.ToString() + " BYN";
                textBlocks.ElementAt(2).Text = payment.PAYMENT_DATETIME.ToString();
                textBlocks.ElementAt(3).Text = "None";
            }

            for (int i = 0; i < textBlocks.Count; i++)
            {
                gridRow.Children.Add(textBlocks.ElementAt(i));
            }

            return gridRow;
        }
    }
}
