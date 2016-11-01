using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Transaction.Data;
using Transaction.Properties;
using Transaction.Service;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Transaction
{
	public class TransactionMoneyViewModel
	{
		public TransactionMoneyViewModel()
		{ }

		public TransactionMoneyViewModel(IOpenProjectFileService fileOpenDialogService,
			ISaveProjectFileService fileSaveDialogService)
		{
			_FileOpenDialogService = fileOpenDialogService;
			_FileSaveDialogService = fileSaveDialogService;

			_Purse = StartTestPurse.TestPurse();


			//в приватное поле передаем список транзакций из "кошелька"
			_MoneyOperations = _Purse.MoneyOperations;
			_OperationsTemplate = _Purse.OperationsTemplate;

			_MoneyOperations.CollectionChanged += _MoneyOperations_CollectionChanged;

			//создаем оболочку для работы со списком транзакций
			ItemsMoneyOperations = new ListCollectionView(_MoneyOperations);


			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);

			//сохранение в файл
			SavePurseCommand = new Command(SavePurse);
			SaveAsPurseCommand = new Command(SaveAsPurse);
		}

		private void _MoneyOperations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			
		}
		/// <summary>
		/// поле: сервис открытие файла
		/// </summary>
		private readonly IOpenProjectFileService _FileOpenDialogService;

		/// <summary>
		/// поле: сервис сохранение файла
		/// </summary>
		private readonly ISaveProjectFileService _FileSaveDialogService;

		/// <summary>
		/// поле: кошелек
		/// </summary>
		private Purse _Purse;

		private ObservableCollection<MoneyOperation> _MoneyOperations;

		private ObservableCollection<OperationTemplate> _OperationsTemplate;

		public ListCollectionView ItemsMoneyOperations { get; private set; }

		#region Load/Save 

		/// <summary>
		/// Получает обработчик загрузки данных из файла
		/// </summary>
		public ICommand LoadPurseCommand { get; private set; }

		/// <summary>
		/// Метод загрузки данных из файла
		/// </summary>
		private void LoadPurse()
		{
			var path = _FileOpenDialogService.OpenProjectFile();
			
			var purse = new Purse();

			_Purse = purse.LoadPurse(path);


		}

		/// <summary>
		/// Обработчик сохранения данных в файл
		/// </summary>
		public ICommand SavePurseCommand { get; private set; }

		/// <summary>
		/// сохраниение данных в файл (из того из которого загрузили)
		/// </summary>
		private void SavePurse()
		{

		}

		/// <summary>
		/// Обработчик сохранения данных в новый файл.
		/// </summary>
		public ICommand SaveAsPurseCommand { get; private set; }
		

		/// <summary>
		/// сахранение данных в файл, который мы выбираем
		/// </summary>
		private void SaveAsPurse()
		{
			var path = _FileSaveDialogService.SaveProjectFile();

			_Purse.SavePurse(path, _Purse);
		}



		#endregion
	}
}
