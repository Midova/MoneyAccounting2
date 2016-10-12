using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TransactionLibrary;

namespace MoneyAccounting.EditTransactionMade
{
	class EditTransactionMadeViewModel
	{
		public void Initialize(TransactionMade transactionMade, List<string> categorysTransaction)
		{
			TransactionMade = new TransactionMade(transactionMade.Amount, transactionMade.Category, transactionMade.DateTime, transactionMade.Comment, transactionMade.KindAccount);
			
			CategorysTransaction = new ListCollectionView(categorysTransaction);
		}

		public TransactionMade TransactionMade { get; private set; }		

		public ListCollectionView CategorysTransaction { get; private set; }
	}
}
