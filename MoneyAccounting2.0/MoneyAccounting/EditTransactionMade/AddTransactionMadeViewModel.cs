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
	public class AddTransactionMadeViewModel 
	{
		public void Initialize(ObservableCollection<TransactionMade> templateTransacrion, List<string> categorysTransaction)
		{
			TransactionMade = new TransactionMade();
			TemplateTransacrion = templateTransacrion;
			CategorysTransaction = new ListCollectionView(categorysTransaction);
		}

		public TransactionMade TransactionMade { get;  set; }

		public bool IsAddTemlate { get; set; }

		public ObservableCollection<TransactionMade> TemplateTransacrion { get; set; }

		public ListCollectionView CategorysTransaction { get; set; }

	}
}
