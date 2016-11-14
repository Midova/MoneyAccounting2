using System.Windows;

namespace Transaction.EditingMoneyOperation
{
	/// <summary>
	/// Логика взаимодействия для AddOperationMadeView.xaml
	/// </summary>
	public partial class AddOperationView : Window
	{
		public AddOperationView()
		{
			InitializeComponent();
		}

		private void OkButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
			Close();
		}
	}
}
