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
		public MoneyAccountingViewModel()
		{ }

		public MoneyAccountingViewModel(IOpenProjectFileService fileOpenDialogService,
			ISaveProjectFileService fileSaveDialogService, IEditorWindowService editroWindowService)
		{
			_FileOpenDialogService = fileOpenDialogService;
			_FileSaveDialogService = fileSaveDialogService;
			_EditroWindowService = editroWindowService;

			//создаем и заполняем тестовый кошелк
			_Purse = Example.ComposePurse();

			//в приватное поле передаем список транзакций из "кошелька"
			_TransactionMade = _Purse.MadeTransaction;

			//создаем оболочку для работы со списком транзакций
			ItemsTransactionMade = new ListCollectionView(_TransactionMade);
			
			AddTransactionMadeCommand = new Command(AddTransactionMade);

			Filter = new MoneyAccountingFilterViewModel();
			Filter.PropertyChanged += Filter_PropertyChanged;		
			Filter.OnFileterApplyed += Filter_OnFileterApplyed;
			Filter.OnFilterCleared += Filter_OnFilterCleared;

			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);
		}
		
		#region Infrastructure

		/// <summary>
		/// поле: сервис открытие файла
		/// </summary>
		private readonly IOpenProjectFileService _FileOpenDialogService;
		
		/// <summary>
		/// поле: сервис сохранение файла
		/// </summary>
		private readonly ISaveProjectFileService _FileSaveDialogService;

		/// <summary>
		/// поле: сервис изменение окна. Открытие окна в зависимости от типа.
		/// </summary>
		private readonly IEditorWindowService _EditroWindowService;

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

		/// <summary>
		/// Получает обработчик добавления соверенной транзакции в список транзакций
		/// </summary>
		public ICommand AddTransactionMadeCommand { get; private set; }

		/// <summary>
		/// Метод: добавление соверенной транзакции в список транзакций
		/// </summary>
		private void AddTransactionMade()
		{
			var addition = new AddTransactionMadeViewModel();
			var current = new TransactionMade();

			addition.Initialize(current);

			if (_EditroWindowService.ShowDialog(addition) ?? false)
			{
				current = addition.TransactionMade;
				_Purse.MadeTransaction.Add(current);
			}
		}

		#endregion

		#region Filter

		/// <summary>
		/// Property: Gets filter for cerrent purse.
		/// </summary>
		public MoneyAccountingFilterViewModel Filter { get; private set; }

		/// <summary>
		/// Updates filter in transaction collection.
		/// </summary>
		private void Filter_OnFilterCleared(object sender, EventArgs e)
		{
			ItemsTransactionMade.Filter = null;
		}

		/// <summary>
		/// Updates filter in transaction collection.
		/// </summary>
		private void Filter_OnFileterApplyed(object sender, EventArgs e)
		{
			ItemsTransactionMade.Filter = Filter.DataFilter;			
		}

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
