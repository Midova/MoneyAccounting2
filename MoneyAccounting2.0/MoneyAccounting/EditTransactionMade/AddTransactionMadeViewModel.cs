using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionLibrary;

namespace MoneyAccounting
{
	public class AddTransactionMadeViewModel 
	{
		public void Initialize(TransactionMade transaction)
		{
			TransactionMade = new TransactionMade(transaction.Amount, transaction.Category, transaction.DateTime, transaction.Comment, transaction.KindAccount);
		}

		public TransactionMade TransactionMade { get;  set; }

		public bool IsAddTemlate { get; set; }
	}
}
