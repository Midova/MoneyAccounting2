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

			return result;
		}
	}
}
