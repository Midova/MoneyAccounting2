using Microsoft.Win32;
using MoneyAccounting.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAccounting
{
	public class OpenProjectFileService : IOpenProjectFileService
	{
		public string OpenProjectFile()
		{
			var dialog = new OpenFileDialog()
			{
				//свыбор только одного варианта
				Multiselect = false,
				//FileName = Settings.Default.FilePath, спросить у Володи FilePath
				//расширение файла по умолчанию
				DefaultExt = ".budget",
				//показывает только файлы с нашим расширением
				Filter = "Budget file|*.budget"
			};

			var result = dialog.ShowDialog();
			// !!!! сохраним в настройки путь к последнему открутому файлу
			// Settings.Default.FilePath = dialog.FileName;
			return result == true ? dialog.FileName : string.Empty;

		}
	}
}
