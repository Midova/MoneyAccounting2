using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAccounting
{
	public class AddTransactionMadeViewModel : INotifyPropertyChanged
	{
		public bool ShowDialog()
		{
			var view = new AddTransactionMadeView
			{
				DataContext = this
			};

			var result = view.ShowDialog();
			view.DataContext = null;
			view = null;
			return result ?? false;
		}


		public event PropertyChangedEventHandler PropertyChanged;
	}
}
