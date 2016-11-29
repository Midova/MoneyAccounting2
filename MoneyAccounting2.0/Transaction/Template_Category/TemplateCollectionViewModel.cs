using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Data;
using Transaction.Service;

namespace Transaction.Template_Category
{
	public class TemplateCollectionViewModel
	{
		
		public TemplateCollectionViewModel()
		{ }

		public TemplateCollectionViewModel(IShowWindowService showWindowService)
		{
			_ShowWindowService = showWindowService;
		}

		public void Initialize(ObservableCollection<OperationTemplate> templateOperation)
		{
			TemplateOperation = templateOperation;

		}		

		private readonly IShowWindowService _ShowWindowService;

		public ObservableCollection<OperationTemplate> TemplateOperation { get; private set; }
	}
}
