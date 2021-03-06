﻿using MoneyAccounting.EditTransactionMade;
using MoneyAccounting.EditTransactionTemplate;
using MoneyAccounting.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyAccounting
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

			var windowService = new EditorWindowService();
			windowService.Add(typeof(AddTransactionMadeViewModel), typeof(AddTransactionMadeView));
			windowService.Add(typeof(EditTransactionMadeViewModel), typeof(EditTransactionMadeView));
			windowService.Add(typeof(TemplateTransactionListViewModel), typeof(TemplateTransactionListView));
			windowService.Add(typeof(DeleteTransactionMadeViewModel), typeof(DeleteTransactionMadeView));
			windowService.Add(typeof(TemplateTransactionShowWindowViewModel), typeof(TemplateTransactionShowWindowView));
			
			var context = new MoneyAccountingViewModel(openFileService, saveFileService, windowService);

			mainWindow.DataContext = context;
			MainWindow = mainWindow;
			MainWindow.Show();
		}
	}
}
