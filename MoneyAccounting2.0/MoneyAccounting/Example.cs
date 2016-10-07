using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionLibrary;

namespace MoneyAccounting
{
	public class Example
	{
		public static Purse ComposePurse()
		{
			var result = new Purse("Example");
			
			result.MadeTransaction.Add(new TransactionMade(3, "категория1"));
			result.MadeTransaction.Add(new TransactionMade(5, "категория2"));
			result.MadeTransaction.Add(new TransactionMade(3, "категория2", new DateTime(2016, 04, 01)));
			result.MadeTransaction.Add(new TransactionMade(4, "категория1", new DateTime(2016, 04, 02),"дом", AccountType.Bank));

			return result;
		}
	}
}
