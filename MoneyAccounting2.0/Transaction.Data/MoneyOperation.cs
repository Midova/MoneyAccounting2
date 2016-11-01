using Catel.Data;
using System;
using System.Collections.Generic;

namespace Transaction.Data
{
	/// <summary>
	/// Денежная операция
	/// </summary>
	public class MoneyOperation : ObservableObject
	{
		/// <summary>
		/// Инициализация операции.
		/// </summary>
		public MoneyOperation()
		{
			Value = 0D;
			Categorys = new List<string>();
			DateTime = DateTime.Now;
			MoneyType = MoneyType.Cash;
		}
				
		/// <summary>
		/// Инициализация операции.
		/// </summary>
		/// <param name="initializer">Это вложенный класс для заполнения свойств целевого класса.</param>
		public MoneyOperation(Initializer initializer)
		{
			Value = initializer.Value;
			Categorys = initializer.Categorys;
			DateTime = initializer.DateTime;
			MoneyType = initializer.MoneyType;
		}

		/// <summary>
		/// Инициализация операции.
		/// </summary>
		/// <param name="moneyOperation">денежная операция</param>
		public MoneyOperation(MoneyOperation moneyOperation)
		{
			Value = moneyOperation.Value;
			Categorys = moneyOperation.Categorys;
			DateTime = moneyOperation.DateTime;
			MoneyType = moneyOperation.MoneyType;
		}

		/// <summary>
		/// Инициализатор содержит поля для заполнения свойств целевого класса. Это вложенный класс.
		/// </summary>
		public class Initializer
		{
			public double Value;
			public List<string> Categorys;
			public DateTime DateTime;
			public MoneyType MoneyType;
		}

		/// <summary>
		/// Значение операции.
		/// </summary>
		private double _Value;

		/// <summary>
		/// Получает и задает значение данной операции.
		/// </summary>
		public double Value
		{
			get { return _Value; }
			set
			{
				if (value == _Value)
					return;

				_Value = value;
				RaisePropertyChanged(nameof(Value));
				RaisePropertyChanged(nameof(IsDebit));
			}
		}

		/// <summary>
		/// Cписок категорий для данной операции.
		/// </summary>
		private List<string> _Categorys;
		
		/// <summary>
		/// Получает и задает список категорий для данной операции.
		/// </summary>
		public List<string> Categorys
		{
			get { return _Categorys; }
			set
			{
				if (value == _Categorys)
					return;

				_Categorys = value;
				RaisePropertyChanged(nameof(Categorys));
			}
		}

		/// <summary>
		/// Дата данной операции.
		/// </summary>
		private DateTime _DataTime;
		
		/// <summary>
		/// Получает и задает дату данной операции.
		/// </summary>
		public DateTime DateTime
		{
			get { return _DataTime; }
			set
			{
				if (value == _DataTime)
					return;

				_DataTime = value;
				RaisePropertyChanged(nameof(DateTime));
			}
		}

		/// <summary>
		/// Тип денег данной операции.
		/// </summary>
		private MoneyType _MoneyType;

		/// <summary>
		/// Получает и задает тип денег данной операции.
		/// </summary>
		public MoneyType MoneyType
		{
			get { return _MoneyType; }
			set
			{
				if (_MoneyType == value)
					return;

				_MoneyType = value;
				RaisePropertyChanged(nameof(MoneyType));
			}
		}

		/// <summary>
		/// Получает значение приход или расход данная операция.
		/// </summary>
		public string IsDebit
		{
			get
			{
				return _Value >= 0 ? "Income" : "Expense";
			}
		}
	}
}
