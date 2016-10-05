using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionLibrary;

namespace MoneyAccounting
{
	public class MoneyAccountingFilterViewModel
	{
		/// <summary>
		/// Свойство: Получает или задет начало периода.
		/// </summary>
		public DateTime StartDateFilter { get; set; }

		/// <summary>
		/// Свойство:Получает или задает конец периода.
		/// </summary>
		public DateTime EndDateFilter { get; set; }

		/// <summary>
		/// выбранная категория
		/// </summary>
		public string CategoryFilter { get; set; }

		/// <summary>
		/// выбранное описание 
		/// </summary>
		public string CommentFilter { get; set; }

		/// <summary>
		/// Метод подходит ли объект под параметры фильра
		/// </summary>
		/// <param name="item">объект- транзакция</param>
		/// <returns>правда или ложь</returns>
		public bool DataFilter(object item)
		{
			var transaction = (TransactionMade)item;

			if (!string.IsNullOrEmpty(CategoryFilter))
			{
				if (!string.IsNullOrEmpty(CommentFilter))
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter) && (transaction.Comment.Contains(CommentFilter)));
				else
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Category == CategoryFilter));
			}
			else
			{
				if (!string.IsNullOrEmpty(CommentFilter))
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter) && (transaction.Comment.Contains(CommentFilter)));
				else
					return ((transaction.DateTime <= EndDateFilter && transaction.DateTime >= StartDateFilter));
			}
		}


	}
}
