using Catel.Data;
using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Transaction.Data;
using Transaction.Service;

namespace Transaction.EditingMoneyOperation
{
	/// <summary>
	/// Класс добавления новой операции в список операций
	/// </summary>
	public class AddOperationViewModel : ObservableObject
	{
		public AddOperationViewModel()
		{ }

		public AddOperationViewModel(IShowWindowService showWindowService)
		{
			_ShowWindowService = showWindowService;
			AvailableMoneyTypes = new List<MoneyType> { MoneyType.Bank, MoneyType.Cash };
		}

		/// <summary>
		/// Инициализация класса добавления новой операции в список совершенных операций
		/// </summary>
		/// <param name="operationTemplate"></param>
		/// <param name="categorys"></param>
		public void Initialize(ObservableCollection<string> categorys, MoneyOperation moneyOperation)
		{
			_MoneyOperation = moneyOperation;			
			_Categorys = categorys;

			ShowWindowCommentCommand = new Command(ShowWindowComment);			
		}

		/// <summary>
		/// Сервис показа окна.
		/// </summary>
		private readonly IShowWindowService _ShowWindowService;

		/// <summary>
		/// список категорий
		/// </summary>
		private ObservableCollection<string> _Categorys;
		
		/// <summary>
		/// новая денежная операция
		/// </summary>
		private MoneyOperation _MoneyOperation;

		/// <summary>
		/// Получает задает значение операции.
		/// </summary>
		public double Value
		{
			get { return _MoneyOperation.Value; }
			set
			{
				_MoneyOperation.Value = value;
				RaisePropertyChanged(nameof(IsDebit));
			}
		}

		public string IsDebit
		{
			get { return _MoneyOperation.IsDebit; }
		}

		/// <summary>
		/// Получает строку выбранныч категорий.
		/// </summary>
		public string Categoies => string.Join(", ", _MoneyOperation.Categories);

		/// <summary>
		/// Получает или задает тип денег для операции.
		/// </summary>
		public MoneyType MoneyType
		{
			get { return _MoneyOperation.MoneyType; }
			set { _MoneyOperation.MoneyType = value; }
		}
		
		/// <summary>
		/// Получает или задает занчение даты для операции.
		/// </summary>						
		public DateTime Date
		{
			get { return _MoneyOperation.DateTime; }
			set { _MoneyOperation.DateTime = value; }
		}
		
		/// <summary>
		/// Поле: схранять ли транзакцию в шаблон
		/// </summary>
		public bool IsAddTemlate { get; set; }

		/// <summary>
		/// Список типов денег
		/// </summary>
		public List<MoneyType> AvailableMoneyTypes { get; }
		
		/// <summary>
		/// Обработчик показ окна с категориями
		/// </summary>
		public ICommand ShowWindowCommentCommand { get; private set; }

		/// <summary>
		/// Показ окна с категориями
		/// </summary>
		private void ShowWindowComment()
		{
			var window = new CategorysListViewModel();
			window.Initialize(_Categorys, _MoneyOperation.Categories);

			if(_ShowWindowService.ShowDialog(window)?? false)
			{				
				_MoneyOperation.Categories = (ObservableCollection<string>) window.SelectedItems;
				RaisePropertyChanged(nameof(Categoies));
			}
		}
	}
}
