using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAccounting
{
	public class SaveProjectFileService : ISaveProjectFileService
	{
		public string SaveProjectFile()
		{
			//новое окно сохранения файла
			var dialog = new SaveFileDialog()
			{
				// расширение по умолчанию
				DefaultExt = ".budget"
			};
			var result = dialog.ShowDialog();
			//возвращаем result- если есть полный путь файла или пустую строку
			return result == true ? dialog.FileName : string.Empty;
		}
	}
}
