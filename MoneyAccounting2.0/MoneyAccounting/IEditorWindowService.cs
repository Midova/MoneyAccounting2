using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAccounting
{
	public interface IEditorWindowService
	{
		void Add(Type editor, Type window);

		bool? ShowDialog(object editValue);

	}
}
