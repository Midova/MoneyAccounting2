using System;
using System.Collections.ObjectModel;

namespace Transaction.Data
{
	/// <summary>
	/// Кошелек
	/// </summary>
	public class Purse
	{
		public Purse()
		{
			NamePurse = string.Empty;
			MoneyOperations = new ObservableCollection<MoneyOperation>();
			OperationsTemplate = new ObservableCollection<OperationTemplate>();
		}
		
		public Purse(string namePurse) : this()
		{
			NamePurse = namePurse;
		}

		/// <summary>
		/// Получает название кошелька
		/// </summary>
		public string NamePurse { get; private set; }

		/// <summary>
		/// Получает список операций
		/// </summary>
		public ObservableCollection<MoneyOperation> MoneyOperations { get; private set; }

		/// <summary>
		/// Получает список шаблонов
		/// </summary>
		public ObservableCollection<OperationTemplate> OperationsTemplate { get; private set; }

		/// <summary>
		/// Получает баланс на данный момент.
		/// </summary>
		/// <returns>итоговое значение</returns>
		public double GetBalance()
		{
			double balance = 0D;
			foreach (MoneyOperation operation in MoneyOperations)
				if (operation.DateTime <= DateTime.Now)
					balance += operation.Value;

			return balance;
		}

		/// <summary>
		/// Добавляет в список операций заданную оперцию
		/// </summary>
		/// <param name="moneyOperation">заданная операция</param>
		public void AddMoneyOperation(MoneyOperation moneyOperation)
		{
			MoneyOperations.Add(moneyOperation);
		}

		/// <summary>
		/// Добавляет в список шаблонов заданный шаблон
		/// </summary>
		/// <param name="operationTemplate">заданный шаблон</param>
		public void AddOperationTemplate(OperationTemplate operationTemplate)
		{
			OperationsTemplate.Add(operationTemplate);
		}
	}
}
