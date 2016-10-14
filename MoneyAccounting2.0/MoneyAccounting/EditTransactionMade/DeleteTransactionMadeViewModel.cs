using Catel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionLibrary;

namespace MoneyAccounting.EditTransactionMade
{
	/// <summary>
	/// класс: удаление транзакции из списка совершенных транзакций
	/// </summary>
	public class DeleteTransactionMadeViewModel : ObservableObject
	{
		/// <summary>
		/// Инициализация класса удаления транзакции из списка совершенных транзакций
		/// </summary>
		/// <param name="operation">объект для удаления</param>
		public void Initialize(TransactionMade transactionMade, IEditorWindowService editroWindowService)
		{
			TransactionMade = new TransactionMade(transactionMade.Amount, transactionMade.Category, transactionMade.DateTime);
			_EditroWindowService = editroWindowService;
		}

		/// <summary>
		/// Получает выбранную транзакцию
		/// </summary>
		public TransactionMade TransactionMade { get; private set; }

		// <summary>
		/// поле: сервис изменение окна. Открытие окна в зависимости от типа.
		/// </summary>
		private IEditorWindowService _EditroWindowService; //убрала readonly

	}
}
