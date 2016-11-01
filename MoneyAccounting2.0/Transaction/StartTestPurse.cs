using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;

namespace Transaction
{
	public class StartTestPurse
	{
		public static Purse TestPurse()
		{
			var result = new Purse("Test");

			var init = new MoneyOperation.Initializer { Value = 3, Categorys = new List<string>(), DateTime = new DateTime(2016, 04, 01), MoneyType = MoneyType.Bank };
			
			result.AddMoneyOperation(new MoneyOperation(init));
			result.AddMoneyOperation(new MoneyOperation());
			result.AddOperationTemplate(new OperationTemplate());

			var operations = result.MoneyOperations;

			return result;
		}
	}
}
