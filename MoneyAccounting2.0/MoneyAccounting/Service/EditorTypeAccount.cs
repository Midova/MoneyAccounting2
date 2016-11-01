using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyAccounting.Service
{
	public class EditorTypeAccount
	{
		public EditorTypeAccount()
		{
			_TypeAccount = new Dictionary<Type, Type>();
		}

		/// <summary>
		/// словарь привязка типа аккаунта к списку.
		/// </summary>
		private readonly Dictionary<Type, Type> _TypeAccount;

		/// <summary>
		/// добавление связки 
		/// </summary>
		/// <param name="keyEnum"></param>
		/// <param name="accountType"></param>
		public void Add(Type keyEnum, Type accountType)
		{
			if (_TypeAccount.ContainsKey(keyEnum))
				_TypeAccount[keyEnum] = accountType;
			else
				_TypeAccount.Add(keyEnum, accountType);
		}
	}
}
