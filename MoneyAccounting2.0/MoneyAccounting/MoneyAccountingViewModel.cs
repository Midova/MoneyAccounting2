using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TransactionLibrary;
using Catel.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace MoneyAccounting
{
	public class MoneyAccountingViewModel 
	{
		
		public MoneyAccountingViewModel(IOpenProjectFileService fileOpenDialogService, ISaveProjectFileService fileSaveDialogService)
		{
			_FileOpenDialogService = fileOpenDialogService;
			_FileSaveDialogService = fileSaveDialogService;

			//создаем и заполняем тестовый кошелк
			_Purse = Example.ComposePurse();

			//в приватное поле передаем список транзакций из "кошелька"
			_TransactionMade = _Purse.MadeTransaction;

			//создаем оболочку для работы со списком транзакций
			ItemsTransactionMade = new ListCollectionView(_TransactionMade);
			
			
			
			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);
			
						
			
		}

		#region Infrastructure
		
		/// <summary>
		/// поле: открытие файла
		/// </summary>
		private readonly IOpenProjectFileService _FileOpenDialogService;
		
		/// <summary>
		/// поле: сохранение файла
		/// </summary>
		private readonly ISaveProjectFileService _FileSaveDialogService;
		
		/// <summary>
		/// поле: кошелек
		/// </summary>
		private Purse _Purse;
		
		/// <summary>
		/// поле: список операций
		/// </summary>
		private ObservableCollection<TransactionMade> _TransactionMade;

		/// <summary>
		/// Свойство: получает список соверешных транзакций. Это оболочка для работы со списком операций.
		/// </summary>
		public ListCollectionView ItemsTransactionMade { get; private set; }

		#endregion

		#region Working with Transaction


		#endregion
		
		#region Load\Save Project

		/// <summary>
		/// Получает обработчик загрузки данных из файла
		/// </summary>
		public ICommand LoadPurseCommand { get; private set; }

		/// <summary>
		/// Метод загрузки данных из файла
		/// </summary>
		private void LoadPurse()
		{

		}

		#endregion

	}
}
