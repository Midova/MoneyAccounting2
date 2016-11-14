using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Service;

namespace Transaction.EditingMoneyOperation
{
	/// <summary>
	/// Класс удаления выбранной операции из списка операций
	/// </summary>
	public class DeleteOperationViewModel
	{
		public DeleteOperationViewModel()
		{ }

		public DeleteOperationViewModel(IShowWindowService showWindowService)
		{
			_ShowWindowService = showWindowService;
		}

		public void Initialize(MoneyOperation moneyoperation)
		{
			_MoneyOperation = moneyoperation;
		}

		private IShowWindowService _ShowWindowService;

		private MoneyOperation _MoneyOperation;

		public double Value
		{
			get { return _MoneyOperation.Value; }
			set	{ _MoneyOperation.Value = value; }
		}		

		/// <summary>
		/// Получает строку выбранныч категорий.
		/// </summary>
		public string Categoies => string.Join(", ", _MoneyOperation.Categories);
		
		/// <summary>
		/// Получает или задает занчение даты для операции.
		/// </summary>						
		public DateTime Date
		{
			get { return _MoneyOperation.DateTime; }
			set { _MoneyOperation.DateTime = value; }
		}
	}
}
