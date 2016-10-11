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
using System.ComponentModel;
using MoneyAccounting.EditTransactionMade;

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

			_TransactionMade.CollectionChanged += _TransactionMade_CollectionChanged;


			FillingCategoryList();
			CategorysTransaction = new ListCollectionView(_CategorysTransaction);
			
			AddTransactionMadeCommand = new Command(AddTransactionMade);
			EditTransactionMadeCommand = new Command(EditTransactionMade);

			Filter = new MoneyAccountingFilterViewModel(CategorysTransaction);

			//выбор категории
			Filter.CategorysFilter.CurrentChanged += CategorysFilter_CurrentChanged;
			Filter.PropertyChanged += Filter_PropertyChanged;
			Filter.OnFilterChanged += Filter_OnFilterApplyed;	

			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);
		}

		private void _TransactionMade_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			FillingCategoryList();
			CategorysTransaction = new ListCollectionView(_CategorysTransaction);
		}

		private void Filter_OnFilterApplyed(object sender, MoneyAccountingFilterViewModel.Filter e)
		{			
			ItemsTransactionMade.Filter = e.FilterRule;
			throw new NotImplementedException();
		}

		private void Filter_OnChoiceTypeAccount(object sender, EventArgs e)
		{
			ItemsTransactionMade.Filter = Filter.GetFilter;
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


		/// <summary>
		/// список категорий операций
		/// </summary>
		private List<string> _CategorysTransaction;

		/// <summary>
		/// получает список категорий операций
		/// </summary>
		public ListCollectionView CategorysTransaction { get; private set; }

		public ObservableCollection<TransactionMade> TemplateTransacrion { get; set; }


		/// <summary>
		/// Заполняет список категорий
		/// </summary>
		private void FillingCategoryList()
		{
			_CategorysTransaction = new List<string>();
			_CategorysTransaction.Add(string.Empty);

			foreach (var item in _TransactionMade)
				_CategorysTransaction.Add(item.Category);

			_CategorysTransaction = _CategorysTransaction.Distinct().ToList();
		}

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
			

			addition.Initialize(TemplateTransacrion, _CategorysTransaction);

			if (_EditroWindowService.ShowDialog(addition) ?? false)
			{
				var current = new TransactionMade(addition.TransactionMade.Amount, (string)addition.CategorysTransaction.CurrentItem,addition.TransactionMade.DateTime,addition.TransactionMade.Comment);

				_Purse.MadeTransaction.Add(current);
			}
		}

		public ICommand EditTransactionMadeCommand { get; private set; }

		private void EditTransactionMade()
		{
			var editor = new EditTransactionMadeViewModel();
			var current = (TransactionMade)ItemsTransactionMade.CurrentItem;

			editor.Initialize(current, _CategorysTransaction);

			if (_EditroWindowService.ShowDialog(editor) ?? false)
			{

			}
		}

		#endregion

		#region Filter

		/// <summary>
		/// Property: Gets filter for cerrent purse.
		/// </summary>
		public MoneyAccountingFilterViewModel Filter { get; private set; }

		private void Filter_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//if (e.PropertyName == "IsTypeAll" || e.PropertyName == "IsTypeBank" || e.PropertyName == "IsTypeBank")
			//	ItemsTransactionMade.Filter = Filter.DataFilter;

			ItemsTransactionMade.Filter = Filter.GetFilter;
		}

		private void CategorysFilter_CurrentChanged(object sender, EventArgs e)
		{
			ItemsTransactionMade.Filter = Filter.GetFilter;
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
