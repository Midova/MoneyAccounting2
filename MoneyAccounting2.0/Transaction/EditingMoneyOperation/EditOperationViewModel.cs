using Catel.Data;
using Catel.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using Transaction.Data;
using Transaction.Service;

namespace Transaction.EditingMoneyOperation
{
	public class EditOperationViewModel : ObservableObject
	{
		public EditOperationViewModel(IShowWindowService showWindowService)
		{
			_ShowWindowService = showWindowService;
		}

		public void Initialize(MoneyOperation moneyOperation, ObservableCollection<OperationTemplate> operationTemplate, ObservableCollection<string> categorys)
		{
			MoneyOperation = moneyOperation;
			_TemplateOperation = operationTemplate;
			_Categorys = categorys;

			_MoneyType = new ObservableCollection<MoneyType>();
			_MoneyType.Add(Data.MoneyType.Cash);
			_MoneyType.Add(Data.MoneyType.Bank);

			MoneyType = new ListCollectionView(_MoneyType);

			ShowWindowCommentCommand = new Command(ShowWindowComment);
		}

		/// <summary>
		/// Сервис показа окна.
		/// </summary>
		private readonly IShowWindowService _ShowWindowService;

		/// <summary>
		/// Сисок категорий.
		/// </summary>
		private ObservableCollection<string> _Categorys;

		/// <summary>
		/// Список шаблонов.
		/// </summary>
		private ObservableCollection<OperationTemplate> _TemplateOperation;

		/// <summary>
		/// Выбранная денежная операция.
		/// </summary>
		public MoneyOperation MoneyOperation { get; set; }		

		/// <summary>
		/// Список типов денег
		/// </summary>
		private ObservableCollection<MoneyType> _MoneyType;

		/// <summary>
		/// Обертка для списка типов денег
		/// </summary>
		public ListCollectionView MoneyType { get; private set; }

		/// <summary>
		/// Обработчик показ окна с категориями
		/// </summary>
		public ICommand ShowWindowCommentCommand { get; private set; }

		/// <summary>
		/// Показ окна с категориями
		/// </summary>
		private void ShowWindowComment()
		{
			var window = new CategorysListViewModel();
			window.Initialize(_Categorys, MoneyOperation.Categories);

			if (_ShowWindowService.ShowDialog(window) ?? false)
			{
				MoneyOperation.Categories = null;
				MoneyOperation.Categories = (ObservableCollection<string>)window.SelectedItems;
			}
		}
	}
}
