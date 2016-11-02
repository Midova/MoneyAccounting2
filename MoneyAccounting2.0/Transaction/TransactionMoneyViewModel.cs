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
using Catel.Data;

namespace Transaction
{
	public class TransactionMoneyViewModel : ObservableObject
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

			//создаем оболочку для работы со списком транзакций
			_ItemsMoneyOperations = new ListCollectionView(_Purse.MoneyOperations);
			
			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);

			//сохранение в файл
			SavePurseCommand = new Command(SavePurse);
			SaveAsPurseCommand = new Command(SaveAsPurse);
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

		private ListCollectionView _ItemsMoneyOperations;

		public ListCollectionView ItemsMoneyOperations
		{
			get { return _ItemsMoneyOperations; }			
		}

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
			string path;
			var result = _FileOpenDialogService.OpenProjectFile(out path);
			if(result != true)
				return;

			_ItemsMoneyOperations.DetachFromSourceCollection();
			_ItemsMoneyOperations = null;
			
			_Purse = Purse.LoadPurse(path);

			_ItemsMoneyOperations = new ListCollectionView(_Purse.MoneyOperations);
			RaisePropertyChanged(nameof(ItemsMoneyOperations));
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
			string path;
			var result = _FileSaveDialogService.SaveProjectFile(out path);

			if (result != true)
				return;

			_Purse.SavePurse(path, _Purse);
		}



		#endregion
	}
}
