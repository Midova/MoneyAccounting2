using Catel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
			Id = Guid.NewGuid();

			Value = 0D;
			Categories = new ObservableCollection<string>();
			DateTime = DateTime.Now;
			MoneyType = MoneyType.Cash;
		}
				
		/// <summary>
		/// Инициализация операции.
		/// </summary>
		/// <param name="initializer">Это вложенный класс для заполнения свойств целевого класса.</param>
		public MoneyOperation(Initializer initializer)
		{
			Id = Guid.NewGuid();
			Value = initializer.Value;
			Categories = initializer.Categorys;
			DateTime = initializer.DateTime;
			MoneyType = initializer.MoneyType;
		}

		/// <summary>
		/// Инициализация операции.
		/// </summary>
		/// <param name="moneyOperation">денежная операция</param>
		public MoneyOperation(MoneyOperation moneyOperation)
		{
			Id = Guid.NewGuid();
			Value = moneyOperation.Value;
			Categories = moneyOperation.Categories;
			DateTime = moneyOperation.DateTime;
			MoneyType = moneyOperation.MoneyType;
		}

		/// <summary>
		/// Инициализатор содержит поля для заполнения свойств целевого класса. Это вложенный класс.
		/// </summary>
		public class Initializer
		{
			public double Value;
			public ObservableCollection<string> Categorys;
			public DateTime DateTime;
			public MoneyType MoneyType;
		}

		/// <summary>
		/// 
		/// </summary>
		public Guid Id { get; }

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
		private ObservableCollection<string> _Categories;
		
		/// <summary>
		/// Получает и задает список категорий для данной операции.
		/// </summary>
		public ObservableCollection<string> Categories
		{
			get { return _Categories; }
			set
			{
				if (value == _Categories)
					return;

				_Categories = value;
				RaisePropertyChanged(nameof(Categories));
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
