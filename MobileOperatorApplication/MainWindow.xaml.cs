using MobileOperatorApplication.Oracle;
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
			string oracleConnection = "DATA SOURCE = 192.168.43.153:1521 / orcl;" +
				" USER ID=c##baa; PASSWORD=12345; Pooling = False;";
			OracleProvider straightOracleProvider = new OracleProvider(oracleConnection);
			foreach(var post in straightOracleProvider.GetPosts())
            {
				Console.WriteLine(post);
            }
			Console.WriteLine(straightOracleProvider.GetHash("123", "123123131"));
		}
	}
}
