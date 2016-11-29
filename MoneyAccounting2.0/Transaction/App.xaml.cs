using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Transaction.EditingMoneyOperation;
using Transaction.Service;
using Transaction.Template_Category;

namespace Transaction
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var mainWindow = new MainWindow();
			
			var openFileService = new OpenProjectFileService();
			var saveFileService = new SaveProjectFileService();
			var showWindowService = new ShowWindowService();

			showWindowService.Add(typeof(AddOperationViewModel), typeof(AddOperationView));
			showWindowService.Add(typeof(CategorysListViewModel), typeof(CategorysListView));
			showWindowService.Add(typeof(EditOperationViewModel), typeof(EditOperationView));
			showWindowService.Add(typeof(DeleteOperationViewModel), typeof(DeleteOperationView));
			showWindowService.Add(typeof(CategoryCollectionViewModel), typeof(CategoryCollectionView));
			showWindowService.Add(typeof(TemplateCollectionViewModel), typeof(TemplateCollectionView));

			var context = new TransactionMoneyViewModel(openFileService, saveFileService, showWindowService);
			context.OnWindowClosed += Context_OnWindowClosed;
			
			mainWindow.DataContext = context;
			MainWindow.Closing += MainWindow_Closing;
			MainWindow = mainWindow;
			MainWindow.Show();								
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			((TransactionMoneyViewModel)MainWindow.DataContext).OnWindowClosed -= Context_OnWindowClosed;
		}

		/// <summary>
		/// Закрывает главное окно
		/// </summary>
		private void Context_OnWindowClosed(object sender, EventArgs e)
		{
			MainWindow.Close();
		}
	}
}
