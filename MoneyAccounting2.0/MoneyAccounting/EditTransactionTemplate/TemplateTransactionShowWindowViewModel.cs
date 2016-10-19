using Catel.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TransactionLibrary;

namespace MoneyAccounting.EditTransactionTemplate
{
	public class TemplateTransactionShowWindowViewModel : ObservableObject
	{

		public TemplateTransactionShowWindowViewModel(IEditorWindowService editorWindowService)
		{
			_EditorWindowService = editorWindowService;
		}

		public void Initialize(ObservableCollection<TransactionTemplate> templateTransacrion)
		{
			_TemplatesTransacrion = templateTransacrion;
			TemplatesTransacrionList = new ListCollectionView(_TemplatesTransacrion);			
		}

		/// <summary>
		/// Получает список шаблонов транзакции
		/// </summary>
		private ObservableCollection<TransactionTemplate> _TemplatesTransacrion;

		public ObservableCollection<TransactionTemplate> TemplatesTransacrion
		{
			get { return _TemplatesTransacrion; }
			set
			{
				if (value == _TemplatesTransacrion)
					return;
				_TemplatesTransacrion = value;
				RaisePropertyChanged(nameof(TemplatesTransacrion));

			}
		}

		/// <summary>
		/// получает список шаблонов
		/// </summary>
		public ListCollectionView TemplatesTransacrionList { get; private set; }

		/// <summary>
		/// поле: сервис изменение окна. Открытие окна в зависимости от типа.
		/// </summary>
		private readonly IEditorWindowService _EditorWindowService;

		public TransactionMade CreateTransactionMade()
		{
			var current = (TransactionTemplate)TemplatesTransacrionList.CurrentItem;
			if (current != null)
				return current.CreateOperation();
			return null;
		}
	}
}
