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






			//выборка транзакций по фильтру.
			ApplyDataRangeCommand = new Command(ApplyDataRange);
			//очищаем фильтр.
			CleareFilterCommand = new Command(CleareFilter);

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
		/// Свойство: Получает или задет начало периода.
		/// </summary>
		public DateTime StartDateFilter { get; set; }

		/// <summary>
		/// Свойство:Получает или задает конец периода.
		/// </summary>
		public DateTime EndDateFilter { get; set; }

		/// <summary>
		/// выбранная категория
		/// </summary>
		public string CategoryFilter { get; set; }

		/// <summary>
		/// выбранное описание 
		/// </summary>
		public string CommentFilter { get; set; }

		/// <summary>
		/// Метод: подходит ли объект под параметры фильра
		/// </summary>
		/// <param name="item">объект- транзакция</param>
		/// <returns>правда или ложь</returns>
		public bool DataFilter(object item)
		{
			var transaction = (TransactionMade)item;

			if (!string.IsNullOrEmpty(CategoryFilter))
			{
				if (!string.IsNullOrEmpty(CommentFilter))
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.Comment.Contains(CommentFilter)));
				else
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter));
			}
			else
			{
				if (!string.IsNullOrEmpty(CommentFilter))
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Comment.Contains(CommentFilter)));
				else
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter));
			}
		}


		public ICommand CleareFilterCommand { get; private set; }

		private void CleareFilter()
		{
			ItemsTransactionMade.Filter = null;			
		}

		/// <summary>
		/// Свойство: Получает диапазон данных.
		/// </summary>
		public ICommand ApplyDataRangeCommand { get; private set; }

		/// <summary>
		/// Метод: Применяет фильтр.
		/// </summary>
		private void ApplyDataRange()
		{
			ItemsTransactionMade.Filter = DataFilter;
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
