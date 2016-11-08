using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

			var init = new MoneyOperation.Initializer { Value = 3, Categorys = new ObservableCollection<string>(), DateTime = new DateTime(2016, 04, 01), MoneyType = MoneyType.Bank };
			init.Categorys.Add("дом");
			init.Categorys.Add("ремонт");
			result.AddMoneyOperation(new MoneyOperation(init));
			result.AddMoneyOperation(new MoneyOperation());
			result.AddOperationTemplate(new OperationTemplate());

			var operations = result.MoneyOperations;

			return result;
		}
	}
}
