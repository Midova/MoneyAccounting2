using Catel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TransactionLibrary;

namespace MoneyAccounting
{
	public class AddTransactionMadeViewModel : ObservableObject
	{
		public void Initialize(ObservableCollection<TransactionMade> templateTransacrion, List<string> categorysTransaction)
		{
			TransactionMade = new TransactionMade();
			TemplateTransacrion = templateTransacrion;
			CategorysTransaction = new ListCollectionView(categorysTransaction);
		}

		/// <summary>
		/// 
		/// </summary>
		private bool _IsAddTemlate;

		private AccountType _TypeAccountFilter;

		/// <summary>
		/// 
		/// </summary>
		public bool IsTypeBank
		{
			get {return _TypeAccountFilter == AccountType.Bank; }
			set
			{
				_TypeAccountFilter = value ? AccountType.Bank : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeBank));
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsTypeCash
		{
			get { return _TypeAccountFilter == AccountType.Cash; }
			set
			{
				_TypeAccountFilter = value ? AccountType.Cash : _TypeAccountFilter;
				RaisePropertyChanged(nameof(IsTypeCash));
			}
		}

		public AccountType AccountTyp()
		{			
				if (IsTypeBank)
					return AccountType.Bank;
				return AccountType.Cash;		
		}

		public TransactionMade TransactionMade { get; private set; }		

		public ObservableCollection<TransactionMade> TemplateTransacrion { get;private set; }

		public ListCollectionView CategorysTransaction { get; private set; }

	}
}
