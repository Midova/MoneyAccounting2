using Catel.Data;
using Catel.MVVM;
using System;
using System.Windows.Input;
using TransactionLibrary;

namespace MoneyAccounting
{
	public class MoneyAccountingFilterViewModel : ObservableObject
	{
		public MoneyAccountingFilterViewModel()
		{
			//выборка транзакций по фильтру.
			ApplyDataRangeCommand = new Command(ApplyDataRange);
			//очищаем фильтр.
			CleareFilterCommand = new Command(CleareFilter);
		}

		#region Filter

		private DateTime _StartDateFilter;

		/// <summary>
		/// Свойство: Получает или задет начало периода.
		/// </summary>
		public DateTime StartDateFilter
		{
			get { return _StartDateFilter; }
			set
			{
				if (value == _StartDateFilter)
					return;

				_StartDateFilter = value;
				RaisePropertyChanged(nameof(StartDateFilter));
			}
		}

		private DateTime _EndDateFilter;
		
		/// <summary>
		/// Свойство:Получает или задает конец периода.
		/// </summary>
		public DateTime EndDateFilter
		{
			get { return _EndDateFilter; }
			set
			{
				if (value == _EndDateFilter)
					return;
				_EndDateFilter = value;
				RaisePropertyChanged(nameof(EndDateFilter));
			}
		}

		private string _CategoryFilter;

		/// <summary>
		/// выбранная категория
		/// </summary>
		public string CategoryFilter
		{
			get { return _CategoryFilter; }
			set
			{
				if (value == _CategoryFilter)
					return;
				_CategoryFilter = value;
				RaisePropertyChanged(nameof(CategoryFilter));
			}
		}

		private string _CommentFilter;

		/// <summary>
		/// выбранное описание 
		/// </summary>
		public string CommentFilter
		{
			get { return _CommentFilter; }
			set
			{
				if (value == _CommentFilter)
					return;
				_CommentFilter = value;
				RaisePropertyChanged(nameof(_CommentFilter));
			}
		}

		/// <summary>
		/// Метод: подходит ли объект под параметры фильра
		/// </summary>
		/// <param name="item">объект- транзакция</param>
		/// <returns>правда или ложь</returns>
		public bool DataFilter(object item)
		{
			var transaction = (TransactionMade)item;

			if (!string.IsNullOrEmpty(CategoryFilter))
			{
				if (!string.IsNullOrEmpty(CommentFilter))
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.Comment.Contains(CommentFilter)));
				else
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter));
			}
			else
			{
				if (!string.IsNullOrEmpty(CommentFilter))
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Comment.Contains(CommentFilter)));
				else
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter));
			}
		}


		public ICommand CleareFilterCommand { get; private set; }

		public event EventHandler OnFilterCleared;

		private void CleareFilter()
		{
			var handler = OnFilterCleared;
			if (handler != null)
				OnFilterCleared(this, new EventArgs());
		}

		/// <summary>
		/// Свойство: Получает диапазон данных.
		/// </summary>
		public ICommand ApplyDataRangeCommand { get; private set; }

		public event EventHandler OnFileterApplyed;

		/// <summary>
		/// Метод: Применяет фильтр.
		/// </summary>
		private void ApplyDataRange()
		{
			var handler = OnFileterApplyed;
			if (handler != null)
				OnFileterApplyed(this, new EventArgs());
		}

		#endregion
	}
}
