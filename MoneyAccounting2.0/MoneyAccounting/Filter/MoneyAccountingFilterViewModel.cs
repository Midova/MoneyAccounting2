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

			TextInpunt = new Command(CommentFilterEvent);


			CategorysFilter = categorysTransaction;
		}

		#region Filter

		public enum TypeFilter
		{
			All,
			Bank,
			Cash
		}

		private TypeFilter _TypeAccountFilter = TypeFilter.All;

		private AccountType _AccountType
		{
			get
			{
				if (IsTypeBank)
					return AccountType.Bank;
				return AccountType.Cash;
			}
		}

		public TypeFilter TypeAccountFilter
		{
			get { return _TypeAccountFilter; }
			set
			{
				if (value == _TypeAccountFilter)
					return;
				_TypeAccountFilter = value;
				RaisePropertyChanged(nameof(_TypeAccountFilter));
				RaisePropertyChanged(nameof(IsTypeAll));
				RaisePropertyChanged(nameof(IsTypeBank));
				RaisePropertyChanged(nameof(IsTypeCash));

			}
		}

		public bool IsTypeAll
		{
			get { return _TypeAccountFilter == TypeFilter.All; }
			set { _TypeAccountFilter = value ? TypeFilter.All : _TypeAccountFilter; }
		}
			

		public bool IsTypeBank
		{
			get { return _TypeAccountFilter == TypeFilter.All; }
			set { _TypeAccountFilter = value ? TypeFilter.All : _TypeAccountFilter; }
		}

		public bool IsTypeCash
		{
			get { return _TypeAccountFilter == TypeFilter.All; }
			set { _TypeAccountFilter = value ? TypeFilter.All : _TypeAccountFilter; }
		}



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
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.Comment.Contains(CommentFilter)) && (transaction.KindAccount == _AccountType));
					else
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.KindAccount == _AccountType));
				}
				else
				{
					if (!string.IsNullOrEmpty(CommentFilter))
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Comment.Contains(CommentFilter)) && (transaction.KindAccount == _AccountType));
					else
						return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.KindAccount == _AccountType));
				}
			}
			

			
		}


		public ICommand TextInpunt { get; private set; }

		public event EventHandler TextInputEvent;

		private void CommentFilterEvent()
		{
			var handler = TextInputEvent;
			if (handler != null)
				TextInputEvent(this, new EventArgs());
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
