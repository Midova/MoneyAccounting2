using Catel.MVVM;
using System;
using System.Linq;
using System.Windows.Input;
using Transaction.Data;
using Transaction.Service;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Catel.Data;
using Transaction.EditingMoneyOperation;

namespace Transaction
{
	public class TransactionMoneyViewModel : ObservableObject
	{
		public TransactionMoneyViewModel()
		{ }

		public TransactionMoneyViewModel(IOpenProjectFileService fileOpenDialogService,
			ISaveProjectFileService fileSaveDialogService, IShowWindowService showWindowService)
		{
			_FileOpenDialogService = fileOpenDialogService;
			_FileSaveDialogService = fileSaveDialogService;
			_ShowWindowService = showWindowService;

			_Purse = StartTestPurse.TestPurse();

			_Purse.MoneyOperations.CollectionChanged += MoneyOperations_CollectionChanged;
			
			//Заполняем список представлений операций.
			FillViewModelOperations();


			//создаем оболочку для работы со списком представлений операций.
			_ItemsMoneyOperations = new ListCollectionView(_ViewModelOperations);
												
			//загрузки из файла.
			LoadPurseCommand = new Command(LoadPurse);
			//сохранение в файл.		
			SaveAsPurseCommand = new Command(SaveAsPurse);
			//закрытие главного окна.
			CloseMainWindowCommand = new Command(CloseMainWindow);

			AddMoneyOperationCommand = new Command(AddMoneyOperation);
			EditMoneyOperationCommand = new Command(EditMoneyOperation);
			DeleteMoneyOperationCommand = new Command(DeleteMoneyOperation, CanDeleteMoneyOperation);

		}		
		#region Infrastructure
		/// <summary>
		/// поле: сервис открытие файла.
		/// </summary>
		private readonly IOpenProjectFileService _FileOpenDialogService;

		/// <summary>
		/// поле: сервис сохранение файла.
		/// </summary>
		private readonly ISaveProjectFileService _FileSaveDialogService;

		/// <summary>
		/// поле: сервис показа окна.
		/// </summary>
		private readonly IShowWindowService _ShowWindowService;

		/// <summary>
		/// поле: кошелек.
		/// </summary>
		private Purse _Purse;

		/// <summary>
		/// Список представлений операций
		/// </summary>
		private readonly ObservableCollection<MoneyOperationViewModel> _ViewModelOperations =
			new ObservableCollection<MoneyOperationViewModel>();
		#endregion

		/// <summary>
		/// поле: список операций.
		/// </summary>
		private ListCollectionView _ItemsMoneyOperations;
		
		/// <summary>
		/// Свойство: получает списоксовершеных транзакций. Оболочка для работы со списком операций.
		/// </summary>
		public ListCollectionView ItemsMoneyOperations
		{
			get { return _ItemsMoneyOperations; }			
		}

		/// <summary>
		/// Получает баланс кошелька.
		/// </summary>
		public double Balance
		{
			get { return _Purse.Balance; }
		}

		/// <summary>
		/// Зполнение списка предсталения операций.
		/// </summary>
		public void FillViewModelOperations()
		{
			if (_ViewModelOperations.Any())
				_ViewModelOperations.Clear();

			foreach (var item in _Purse.MoneyOperations)
			{
				var result = new MoneyOperationViewModel();
				result.Initialization(item);
				_ViewModelOperations.Add(result);
			}
		}

		#region Load/Save 

		/// <summary>
		/// Получает обработчик загрузки данных из файла.
		/// </summary>
		public ICommand LoadPurseCommand { get; private set; }

		/// <summary>
		/// Метод загрузки данных из файла.
		/// </summary>
		private void LoadPurse()
		{
			string path;
			var result = _FileOpenDialogService.OpenProjectFile(out path);
			if(result != true)
				return;
						
			_Purse = Purse.LoadPurse(path);
			_Purse.MoneyOperations.CollectionChanged += MoneyOperations_CollectionChanged;
			FillViewModelOperations();			

			RaisePropertyChanged(nameof(Balance));
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
			
			Purse.SavePurse(path, _Purse);
		}

		/// <summary>
		/// Обработчик закрытия окна
		/// </summary>
		public ICommand CloseMainWindowCommand { get; private set; }

		/// <summary>
		/// Вызывается когда пользователь инициировал закрытие окна.
		/// </summary>
		public event EventHandler OnWindowClosed;

		/// <summary>
		/// Закрытие окна с отпиской от событий.
		/// </summary>
		private void CloseMainWindow()
		{
			_ItemsMoneyOperations.DetachFromSourceCollection();
			_ItemsMoneyOperations = null;

			var handler = OnWindowClosed;
			if (handler != null)
				OnWindowClosed(this, new EventArgs());
		}

		#endregion

		#region EditingMoneyOperation

		/// <summary>
		/// 
		/// </summary>
		public ICommand AddMoneyOperationCommand { get; private set; }

		/// <summary>
		/// Метод: добавление операции в списоке операций
		/// </summary>
		private void AddMoneyOperation()
		{
			var addition = new AddOperationViewModel(_ShowWindowService);
			var editor = new MoneyOperation();
			addition.Initialize(_Purse.Categorys, editor);

			if (_ShowWindowService.ShowDialog(addition) ?? false)
			{				
				_Purse.AddMoneyOperation(editor);

				if (addition.IsAddTemlate)
				{
					var template = new OperationTemplate();
					template.Initialaze(editor.Value, editor.Categories);
					_Purse.AddOperationTemplate(template);
				}

				var operationViewModel = new MoneyOperationViewModel();
				operationViewModel.Initialization(editor);
				_ViewModelOperations.Add(operationViewModel);
			}
		}

		public ICommand EditMoneyOperationCommand { get; private set; }

		/// <summary>
		/// Метод: редактировани операции в списоке операций
		/// </summary>
		private void EditMoneyOperation()
		{
			var editor = new AddOperationViewModel(_ShowWindowService);

			var currentView = (MoneyOperationViewModel)ItemsMoneyOperations.CurrentItem;

			var current = new MoneyOperation();

			foreach (var item in _Purse.MoneyOperations)
			{
				if (item.Id == currentView.Id)
					current = item;
			}

			editor.Initialize(_Purse.Categorys, current);
			if (_ShowWindowService.ShowDialog(editor)??false)
			{
				var operationViewModel = new MoneyOperationViewModel();
				operationViewModel.Initialization(current);
				_ViewModelOperations.Insert(_Purse.MoneyOperations.IndexOf(current), operationViewModel);
				_ViewModelOperations.RemoveAt(_Purse.MoneyOperations.IndexOf(current)+1);
				RaisePropertyChanged(nameof(Balance));
			}
		}

		/// <summary>
		/// Получает обработчик удаления операции из списка
		/// </summary>
		public ICommand DeleteMoneyOperationCommand { get; private set; }

		/// <summary>
		/// Метод: можно ли совершить удаление
		/// </summary>
		/// <returns>Истина- список операций не пустой и выбранна операция. Ложь- иначе </returns>
		private bool CanDeleteMoneyOperation()
		{
			return ItemsMoneyOperations != null && ItemsMoneyOperations.CurrentItem != null;
		}

		/// <summary>
		/// Метод: удаление операции из списка
		/// </summary>
		private void DeleteMoneyOperation()
		{
			var remover = new DeleteOperationViewModel(_ShowWindowService);

			var currentView = (MoneyOperationViewModel)ItemsMoneyOperations.CurrentItem;

			var current = new MoneyOperation();

			foreach (var item in _Purse.MoneyOperations)
			{
				if (item.Id == currentView.Id)
					current = item;
			}

			remover.Initialize(current);

			if (_ShowWindowService.ShowDialog(remover) ?? false)
			{
				_ViewModelOperations.RemoveAt(_Purse.MoneyOperations.IndexOf(current));
				_Purse.MoneyOperations.RemoveAt(_Purse.MoneyOperations.IndexOf(current));				
			}
		}

		#endregion

		private void MoneyOperations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(nameof(Balance));
		}
	}
}
