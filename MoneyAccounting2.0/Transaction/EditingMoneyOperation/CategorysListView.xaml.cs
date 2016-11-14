
using System.Windows;

namespace Transaction.EditingMoneyOperation
{
	/// <summary>
	/// Логика взаимодействия для CategorysListView.xaml
	/// </summary>
	public partial class CategorysListView : Window
	{
		public CategorysListView()
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
