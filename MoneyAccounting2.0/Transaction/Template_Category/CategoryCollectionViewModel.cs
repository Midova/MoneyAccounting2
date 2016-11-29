using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Transaction.Service;

namespace Transaction.Template_Category
{
	public class CategoryCollectionViewModel
	{
		
		public CategoryCollectionViewModel()
		{ }

		public CategoryCollectionViewModel(IShowWindowService showWindowService)
		{
			_ShowWindowService = showWindowService;
		}

		public void Initialize(ObservableCollection<string> categorysOperation)
		{
			CategorysOperation = 
				new ObservableCollection<CategoryViewModel>(categorysOperation.Select(category => new CategoryViewModel { Value = category }));
		}

		public class CategoryViewModel
		{
			public string Value { get; set; }
		}

		/// <summary>
		/// Сервис показа окна.
		/// </summary>
		private readonly IShowWindowService _ShowWindowService;

		public ObservableCollection<CategoryViewModel> CategorysOperation { get; private set; }
	}
}
