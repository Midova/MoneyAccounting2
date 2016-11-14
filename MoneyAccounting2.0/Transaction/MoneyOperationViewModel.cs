using Catel.Data;
using System;
using Transaction.Data;

namespace Transaction
{
	public class MoneyOperationViewModel : ObservableObject
	{
		public MoneyOperationViewModel()
		{

		}

		public void Initialization(MoneyOperation moneyOperation)
		{
			_Operation = moneyOperation;
			_Operation.Categories.CollectionChanged += Categorys_CollectionChanged;
		}
		
		private void Categorys_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(nameof(Categorys));
		}

		private MoneyOperation _Operation;

		/// <summary>
		/// Получает идентификатор
		/// </summary>
		public Guid Id => _Operation.Id;

		/// <summary>
		/// Получает значение операции
		/// </summary>
		public double Value => _Operation.Value;

		///// <summary>
		///// Список категорий
		///// </summary>
		//private string _Categorys;

		/// <summary>
		/// Получает список категорий
		/// </summary>
		public string Categorys => string.Join(",", _Operation.Categories);

		/// <summary>
		/// Получает дату операции
		/// </summary>
		public DateTime DataTime => _Operation.DateTime;

		/// <summary>
		/// Получает тип операции
		/// </summary>
		public MoneyType MoneyType => _Operation.MoneyType;

		/// <summary>
		/// Получает значение "приход" или "расход"
		/// </summary>
		public string IsDebit => _Operation.IsDebit;
	}
}
