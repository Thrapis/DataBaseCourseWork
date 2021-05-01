﻿using MobileOperatorApplication.Model;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        OracleProvider Provider;

        public LoginPage(OracleProvider oracleProvider)
        {
            InitializeComponent();
            Provider = oracleProvider;
        }

        void EnterApplication(object sender, EventArgs e)
        {
            if (sender != EnterButton && (e as KeyEventArgs).Key != Key.Enter)
                return;

            try
            {
                AccountInfo account = Provider.GetAccount(Login.Text, Password.Password);
                ClientRepository clientRepository = new ClientRepository(Provider);
                Client client = clientRepository.Get(account.LOGIN);
                if (account != null)
                    (Window.GetWindow(this) as MainWindow).CloseLoginPage(account, client);
            }
            catch { }

            Message.Text = "Неверный логин или пароль";
        }
    }
}
