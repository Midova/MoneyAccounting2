using Catel.Data;
using Catel.MVVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TransactionLibrary;

namespace MoneyAccounting.EditTransactionMade
{
	/// <summary>
	/// Класс: редактирование транзакции из списка совершенных транзакций
	/// </summary>
	public class EditTransactionMadeViewModel : ObservableObject
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="editorWindowService"></param>
		public EditTransactionMadeViewModel(IEditorWindowService editorWindowService)
		{
			_EditorWindowService = editorWindowService;
		}

		/// <summary>
		/// Инициализация класса редактирования транзакции из списка совершенных транзакций
		/// </summary>
		/// <param name="transactionMade">выбранная транзакция</param>
		/// <param name="categorysTransaction">список категорий транзакций</param>
		/// <param name="editorWindowService">сервис изменение окна. Открытие окна в зависимости от типа</param>
		public void Initialize(TransactionMade transactionMade, List<string> categorysTransaction)
		{
			TransactionMade = new TransactionMade(transactionMade.Amount, transactionMade.Category, transactionMade.DateTime, transactionMade.Comment, transactionMade.KindAccount);
						
			CategorysTransaction = categorysTransaction;

			ShowWindowCommentCommand = new Command(ShowWindowComment);			
		}
		
		/// <summary>
		/// Поле: тип расчета (наличный/ безналичный)
		/// </summary>
		private AccountType _TypeAccountFilter;

		/// <summary>
		/// Выбрана ли эта кнопка
		/// </summary>
		public bool IsTypeBank
		{
			get { return _TypeAccountFilter == TransactionLibrary.AccountType.Bank; }
			set
			{
				_TypeAccountFilter = value ? TransactionLibrary.AccountType.Bank : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeBank));
			}
		}

		/// <summary>
		/// Выбрана ли эта кнопка
		/// </summary>
		public bool IsTypeCash
		{
			get { return _TypeAccountFilter == TransactionLibrary.AccountType.Cash; }
			set
			{
				_TypeAccountFilter = value ? TransactionLibrary.AccountType.Cash : _TypeAccountFilter;
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
		public ObservableCollection<TransactionMade> TemplateTransacrion { get; private set; }

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
		public AccountType AccountType()
		{
			if (IsTypeBank)
				return TransactionLibrary.AccountType.Bank;
			return TransactionLibrary.AccountType.Cash;
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

			if (_EditorWindowService.ShowDialog(window) ?? false)
			{
				if (window.CategorysTransaction.CurrentItem != null)
					TransactionMade.Category = (string)window.CategorysTransaction.CurrentItem;
			}
		}
	}
}
