using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Service
{
	public interface IShowWindowService
	{
		/// <summary>
		/// Добавление связи в словарь
		/// </summary>
		/// <param name="viewModel">Модель представления данных</param>
		/// <param name="window">Окно</param>
		void Add(Type viewModel, Type window);

		/// <summary>
		/// Метод который заставляет окно показываться
		/// </summary>
		/// <param name="viewModelValue">Выбраная модель представление данных</param>
		/// <returns>Истина- пользователь нажал Ок. Ложь- пользователь нажал Cansel</returns>
		bool? ShowDialog(object viewModelValue);
	}
}
