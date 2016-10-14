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
using TransactionLibrary;

namespace MoneyAccounting.EditTransactionMade
{
	/// <summary>
	/// Класс: редактирование транзакции из списка совершенных транзакций
	/// </summary>
	public class EditTransactionMadeViewModel : ObservableObject
	{
		/// <summary>
		/// Инициализация класса редактирования транзакции из списка совершенных транзакций
		/// </summary>
		/// <param name="transactionMade">выбранная транзакция</param>
		/// <param name="categorysTransaction">список категорий транзакций</param>
		/// <param name="editroWindowService">сервис изменение окна. Открытие окна в зависимости от типа</param>
		public void Initialize(TransactionMade transactionMade, List<string> categorysTransaction, IEditorWindowService editroWindowService)
		{
			TransactionMade = new TransactionMade(transactionMade.Amount, transactionMade.Category, transactionMade.DateTime, transactionMade.Comment, transactionMade.KindAccount);
						
			CategorysTransaction = categorysTransaction;

			ShowWindowTemplateCommand = new Command(ShowWindowTemplatee);

			_EditroWindowService = editroWindowService;
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
			get { return _TypeAccountFilter == AccountType.Bank; }
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
		public ObservableCollection<TransactionMade> TemplateTransacrion { get; private set; }

		/// <summary>
		/// Получает список шаблонов
		/// </summary>
		public List<string> CategorysTransaction { get; private set; }

		/// <summary>
		/// поле: сервис изменение окна. Открытие окна в зависимости от типа.
		/// </summary>
		private IEditorWindowService _EditroWindowService; //убрала readonly

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
		public ICommand ShowWindowTemplateCommand { get; private set; }

		/// <summary>
		/// Метод: добавление совершенной транзакции в список транзакций
		/// </summary>
		private void ShowWindowTemplatee()
		{
			var window = new TemplateTransactionListViewModel();
			window.Initialize(CategorysTransaction);
			if (_EditroWindowService.ShowDialog(window) ?? false)
			{
				if (window.CategorysTransaction.CurrentItem != null)
					TransactionMade.Category = (string)window.CategorysTransaction.CurrentItem;
			}
		}
	}
}
