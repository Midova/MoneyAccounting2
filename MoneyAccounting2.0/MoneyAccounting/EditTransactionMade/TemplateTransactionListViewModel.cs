using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MoneyAccounting.EditTransactionMade
{
	public class TemplateTransactionListViewModel
	{
		public void Initialize(List<string> categorysTransaction)
		{
			var categorysTransactionRemive = new List<string>(categorysTransaction);
			categorysTransactionRemive.RemoveAt(0);

			//categorysTransaction.RemoveAt(0);
			CategorysTransaction = new ListCollectionView(categorysTransactionRemive);
		}
		
		public ListCollectionView CategorysTransaction { get; private set; }
	}
}
