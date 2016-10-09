using System;
using System.Collections.Generic;
using System.Windows;

namespace MoneyAccounting
{
	public class EditorWindowService : IEditorWindowService
	{
		public EditorWindowService()
		{
			_EditorWindows = new Dictionary<Type, Type>();
		}

		public void Add(Type editor, Type window)
		{
			if (_EditorWindows.ContainsKey(editor))
				_EditorWindows[editor] = window;
			else
				_EditorWindows.Add(editor, window);
		}

		/// <summary>
		/// словарь привязка редактор к окну.
		/// </summary>
		private readonly Dictionary<Type, Type> _EditorWindows;

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool? ShowDialog(object editValue)
		{
			bool? result = null;
			var editValueType = editValue.GetType();
			Type windowType;

			if (_EditorWindows.TryGetValue(editValueType, out windowType))
			{				
				var window = (Window) Activator.CreateInstance(windowType);
			
				window.DataContext = editValue;
				result = window.ShowDialog();
			}
			return result;
		}
	}
}
