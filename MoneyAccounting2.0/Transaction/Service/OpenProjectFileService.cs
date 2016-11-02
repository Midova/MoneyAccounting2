using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
	public class OpenProjectFileService : IOpenProjectFileService
	{
		public bool? OpenProjectFile(out string path)
		{
			var dialog = new OpenFileDialog()
			{
				//выбор только одного варианта
				Multiselect = false,
				//FileName = setting.
				//расширение файда по умолчанию
				DefaultExt = ".budget",
				//показывать только файлы с нашим расширением
				Filter = "Budget file|*.budget"
			};

			var result = dialog.ShowDialog();
			path = dialog.FileName;

			return result;
		}
	}
}
