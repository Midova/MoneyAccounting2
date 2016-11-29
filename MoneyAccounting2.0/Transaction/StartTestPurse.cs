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
			//result.AddOperationTemplate(new OperationTemplate());

			var operations = result.MoneyOperations;

			result.Categorys.Add("входящий остаток");
			result.Categorys.Add("зарплата");
			result.Categorys.Add("дом");
			result.Categorys.Add("еда");
			result.Categorys.Add("ремонт");
			result.Categorys.Add("врач");


			var temp = new OperationTemplate();
			temp.Initialaze( 8,new ObservableCollection<string> { "дом", "ремонт" });

			result.OperationsTemplate.Add(temp);
			result.OperationsTemplate.Add(new OperationTemplate());


			return result;
		}
	}
}
