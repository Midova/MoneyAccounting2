using Catel.Data;
using Catel.MVVM;
using System;
using System.Windows.Data;
using System.Windows.Input;
using TransactionLibrary;

namespace MoneyAccounting
{
	public class MoneyAccountingFilterViewModel : ObservableObject
	{
		public MoneyAccountingFilterViewModel(ListCollectionView categorysTransaction)
		{
			StartDateFilter = DateTime.Now.AddDays(-30);
			EndDateFilter = DateTime.Now;
			_TypeAccountFilter = TypeFilter.All;
			

			//выборка транзакций по фильтру.
			ApplyDataRangeCommand = new Command(ApplyDataRange);
			//очищаем фильтр.
			CleareFilterCommand = new Command(CleareFilter);

			
			CategorysFilter = categorysTransaction;
		}

		#region Filter

		#region radiobutton

		public enum TypeFilter
		{
			All,
			Bank,
			Cash
		}

		private TypeFilter _TypeAccountFilter = TypeFilter.All;

		public AccountType AccountTypeFilter
		{
			get
			{
				if (IsTypeBank)					
					return AccountType.Bank;										
				return AccountType.Cash;
			}			
		}		

		public bool IsTypeAll
		{
			get { return _TypeAccountFilter == TypeFilter.All; }
			set
			{
				_TypeAccountFilter = value ? TypeFilter.All : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeAll));
			}
		}
		
		public bool IsTypeBank
		{
			get { return _TypeAccountFilter == TypeFilter.Bank; }
			set
			{
				_TypeAccountFilter = value ? TypeFilter.Bank : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeBank));
			}
		}

		public bool IsTypeCash
		{
			get { return _TypeAccountFilter == TypeFilter.Cash; }			
			set
			{
				_TypeAccountFilter = value ? TypeFilter.Cash : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeCash));
			}
		}

		#endregion

		#region Infrastructure

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
		
		/// <summary>
		/// выбранная категория
		/// </summary>
		public ListCollectionView CategorysFilter { get; set; }

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
				RaisePropertyChanged(nameof(CommentFilter));
			}
		}
		
		public class Filter
		{
			public Predicate<object> FilterRule { get; set; }
		}

		public bool GetFilter(object item)
		{
			return FilterByCategory(item) && FilterByPeriod(item)...
		}

		private bool FilterByPeriod(object item)
		{
			if ((StartDateFilter > EndDateFilter) || (StartDateFilter == DateTime.MinValue && EndDateFilter == DateTime.MinValue))
				return true;
			
			throw new NotImplementedException();
		}

		private bool FilterByCategory(object item)
		{
			var currentCategoryString = (string) CategorysFilter.CurrentItem;
			if (string.IsNullOrWhiteSpace(currentCategoryString))
				return true;

			var transaction = (TransactionMade) item;
			return transaction.Category.Contains(currentCategoryString);			
		}

		/// <summary>
		/// Метод: подходит ли объект под параметры фильра
		/// </summary>
		/// <param name="item">объект- транзакция</param>
		/// <returns>правда или ложь</returns>
		public bool DataFilter(object item)
		{
			var transaction = (TransactionMade)item;
			var CategoryFilter = (string)CategorysFilter.CurrentItem;

			if (IsTypeAll)
			{
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
			else
			{
				if (!string.IsNullOrEmpty(CategoryFilter))
				{
					if (!string.IsNullOrEmpty(CommentFilter))
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.Comment.Contains(CommentFilter)) && (transaction.KindAccount == AccountTypeFilter));
					else
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.KindAccount == AccountTypeFilter));
				}
				else
				{
					if (!string.IsNullOrEmpty(CommentFilter))
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Comment.Contains(CommentFilter)) && (transaction.KindAccount == AccountTypeFilter));
					else
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.KindAccount == AccountTypeFilter));
				}
			}			
		}

		#endregion

		#region Events

		public ICommand CleareFilterCommand { get; private set; }
		
		private void CleareFilter()
		{

		}

		/// <summary>
		/// Свойство: Получает диапазон данных.
		/// </summary>
		public ICommand ApplyDataRangeCommand { get; private set; }

		public event EventHandler<Filter> OnFilterApplyed;

		/// <summary>
		/// Метод: Применяет фильтр.
		/// </summary>
		private void ApplyDataRange()
		{

		}
		#endregion

		#endregion
	}
}
