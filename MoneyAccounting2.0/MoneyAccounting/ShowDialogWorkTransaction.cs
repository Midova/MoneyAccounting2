using Catel.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TransactionLibrary;

namespace MoneyAccounting
{
	public abstract class EditorWindowService : IEditorWindowService
	{
		public void Add(Type editor, Type window)
		{
			if (edtiorWindows.ContainsKey(editor))
			{
				edtiorWindows[editor] = window;
			}
			else
				edtiorWindows.Add(editor, window);
		}

		private Dictionary<Type, Type> edtiorWindows;

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool? ShowDialog(object editValue)
		{
			bool? result = null;
			var editValueType = editValue.GetType();
			Type windowType;
			if(edtiorWindows.TryGetValue(editValueType, out windowType) && windowType == typeof(Window))
			{
				var window = (Window) Activator.CreateInstance(windowType);
				window.DataContext = editValue;
				result = window.ShowDialog();
			}

			return result;

			//var view = new AddTransactionMadeView
			//{
			//	DataContext = this
			//};

			//var result = view.ShowDialog();
			//view.DataContext = null;
			//view = null;
			//return result ?? false;
		}
	}
}
