using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
	public class SaveProjectFileService : ISaveProjectFileService
	{
		public string SaveProjectFile()
		{
			var dialog = new SaveFileDialog()
			{
				DefaultExt = ".budget"
			};

			var result = dialog.ShowDialog();

			return result == true ? dialog.FileName : string.Empty;
		}
	}
}
