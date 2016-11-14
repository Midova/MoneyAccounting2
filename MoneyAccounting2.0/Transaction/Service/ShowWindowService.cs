using System;
using System.Collections.Generic;
using System.Windows;

namespace Transaction.Service
{
	public class ShowWindowService : IShowWindowService
	{
		public ShowWindowService()
		{
			_ShowWindow = new Dictionary<Type, Type>();
		}

		/// <summary>
		/// Привязка модели представления данных к окну.
		/// </summary>
		private readonly Dictionary<Type, Type> _ShowWindow;

		public void Add(Type viewModel, Type window)
		{
			//если указаная модель представления данных уже содержится
			//в словаре, то модель представления данных привязывается к окну
			if (_ShowWindow.ContainsKey(viewModel))
				_ShowWindow[viewModel] = window;
			//если нет- то добавляется в словарь модль и окно
			else
				_ShowWindow.Add(viewModel, window);
		}

		public bool? ShowDialog(object viewModelValue)
		{
			bool? result = null;
			var viewModelValueType = viewModelValue.GetType();
			Type windowType;

			if (_ShowWindow.TryGetValue(viewModelValueType, out windowType))
			{
				var window = (Window)Activator.CreateInstance(windowType);

				window.DataContext = viewModelValue;
				result = window.ShowDialog();
			}

			return result;
		}
	}
}
