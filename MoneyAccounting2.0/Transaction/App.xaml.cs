using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Transaction.Service;

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

			var comtext = new TransactionMoneyViewModel(openFileService, saveFileService);


			mainWindow.DataContext = comtext;
			MainWindow = mainWindow;
			MainWindow.Show();
		}
	}
}
