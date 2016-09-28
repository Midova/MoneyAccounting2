using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TransactionLibrary;


namespace MoneyAccounting
{
	public class MoneyAccountingViewModel 
	{
		
		public MoneyAccountingViewModel(IOpenProjectFileService fileOpenDialogService, ISaveProjectFileService fileSaveDialogService)
		{
			_FileOpenDialogService = fileOpenDialogService;
			_FileSaveDialogService = fileSaveDialogService;

			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);//подключить Catel. MVVM

			//сохранение в файл.
			
		}

		private readonly IOpenProjectFileService _FileOpenDialogService;

		private readonly ISaveProjectFileService _FileSaveDialogService;

		//поле кошелек
		private Purse _Purse;

		public ICommand LoadPurseCommand { get; private set; }

		private void LoadPurse()
		{

		}

	}
}
