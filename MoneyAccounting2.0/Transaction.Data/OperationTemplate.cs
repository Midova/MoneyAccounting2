using Catel.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Transaction.Data
{
	/// <summary>
	/// Шаблон операции
	/// </summary>
	public class OperationTemplate : ObservableObject
	{
		/// <summary>
		/// Инициализация шаблона.
		/// </summary>
		public OperationTemplate()
		{
			Value = 0D;
			Categorys = new ObservableCollection<string>();
		}

		/// <summary>
		/// Инициализация шаблона.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="categorys"></param>
		public void Initialaze (double value, ObservableCollection<string> categorys)
		{
			Value = value;
			Categorys = categorys;
		}

		/// <summary>
		/// Значение для данного шаблона.
		/// </summary>
		private double _Value;

		/// <summary>
		/// Получает и задает значение для данного шаблона.
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
			}
		}

		/// <summary>
		/// Cписок категорий для данного шаблона.
		/// </summary>
		private ObservableCollection<string> _Categorys;

		/// <summary>
		/// Получает и задает список категорий для данного шаблона.
		/// </summary>
		public ObservableCollection<string> Categorys
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
		/// Создание операции по подобию шалона.
		/// </summary>
		/// <returns>операция по данному шаблону</returns>
		public MoneyOperation CreateOperation()
		{		
			var ope = new MoneyOperation.Initializer
			{
				Value = Value,
				Categorys = Categorys
			};

			 var operation = new MoneyOperation(ope);

			return operation;
		}
	}
}
