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
using System.Windows.Shapes;

namespace MoneyAccounting.View
{
	/// <summary>
	/// Логика взаимодействия для DeleteTransactionMadeView.xaml
	/// </summary>
	public partial class DeleteTransactionMadeView : Window
	{
		public DeleteTransactionMadeView()
		{
			InitializeComponent();
		}

		private void YesButtom_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();		
		}
	}
}
