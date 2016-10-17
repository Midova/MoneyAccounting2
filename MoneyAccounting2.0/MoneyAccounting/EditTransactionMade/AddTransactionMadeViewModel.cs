using Catel.Data;
using Catel.MVVM;
using MoneyAccounting.EditTransactionMade;
using MoneyAccounting.EditTransactionTemplate;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using TransactionLibrary;

namespace MoneyAccounting
{
	/// <summary>
	/// Класс добавление новой транзакции в список совершенных транзакций
	/// </summary>
	public class AddTransactionMadeViewModel : ObservableObject
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="editorWindowService">сервис изменение окна. Открытие окна в зависимости от типа</param>
		public AddTransactionMadeViewModel(IEditorWindowService editorWindowService)
		{
			_EditorWindowService = editorWindowService;
		}
		
		/// <summary>
		/// Инициализация класса добавления новой  транзакции в список совершенных транзакций
		/// </summary>
		/// <param name="templatesTransacrion">список шаблонов транзакций</param>
		/// <param name="categorysTransaction">список категорий транзакций</param>		
		public void Initialize(ObservableCollection<TransactionTemplate> templatesTransacrion, List<string> categorysTransaction)
		{
			TransactionMade = new TransactionMade();
			TemplateTransacrion = templatesTransacrion;
			CategorysTransaction = categorysTransaction;

			ShowWindowCommentCommand = new Command(ShowWindowComment);
			TemplateWorkAddCommand = new Command(TemplateWorkAdd);		
		}

		/// <summary>
		/// Поле: схранять ли транзакцию в шаблон
		/// </summary>
		public bool IsAddTemlate { get; set; }

		/// <summary>
		/// Поле: тип расчета (наличный/ безналичный)
		/// </summary>
		private AccountType _TypeAccountFilter;

		/// <summary>
		/// Выбрана ли эта кнопка
		/// </summary>
		public bool IsTypeBank
		{
			get {return _TypeAccountFilter == AccountType.Bank; }
			set
			{
				_TypeAccountFilter = value ? AccountType.Bank : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeBank));
			}
		}

		/// <summary>
		/// Выбрана ли эта кнопка
		/// </summary>
		public bool IsTypeCash
		{
			get { return _TypeAccountFilter == AccountType.Cash; }
			set
			{
				_TypeAccountFilter = value ? AccountType.Cash : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeCash));
			}
		}
		
		/// <summary>
		/// Получает транзакцию
		/// </summary>
		public TransactionMade TransactionMade { get; private set; }

		/// <summary>
		/// Получает список шаблонов транзакции
		/// </summary>
		public ObservableCollection<TransactionTemplate> TemplateTransacrion { get;private set; }

		/// <summary>
		/// Получает список шаблонов
		/// </summary>
		public List<string> CategorysTransaction { get; private set; }

		/// <summary>
		/// поле: сервис изменение окна. Открытие окна в зависимости от типа.
		/// </summary>
		private readonly IEditorWindowService _EditorWindowService; //убрала 

		/// <summary>
		/// Задает тип расчета (наличные или безналичные)
		/// </summary>
		/// <returns>Bank/Cash</returns>
		public AccountType AccountTyp()
		{
			if (IsTypeBank)
				return AccountType.Bank;
			return AccountType.Cash;
		}
		
		/// <summary>
		/// Получает метод выбора шаблона
		/// </summary>
		public ICommand ShowWindowCommentCommand { get; private set; }

		/// <summary>
		/// Метод: добавление совершенной транзакции в список транзакций
		/// </summary>
		private void ShowWindowComment()
		{
			var window = new TemplateTransactionListViewModel();
			window.Initialize(CategorysTransaction);

			if ( _EditorWindowService.ShowDialog(window) ?? false)
			{
				if (window.CategorysTransaction.CurrentItem!=null)
				TransactionMade.Category = (string)window.CategorysTransaction.CurrentItem;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public ICommand TemplateWorkAddCommand { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		private void TemplateWorkAdd()
		{
			var TemplateShow = new TemplateTransactionShowWindowViewModel(_EditorWindowService);
			TemplateShow.Initialize(TemplateTransacrion);

			if (_EditorWindowService.ShowDialog(TemplateShow) ?? false)
				TransactionMade = (TransactionMade)TemplateShow.CreateTransactionMade();
		}
	}
}
