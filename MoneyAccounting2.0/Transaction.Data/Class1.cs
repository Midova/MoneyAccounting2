using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Data
{
    public class Class1
    {
		public Class1()
		{
			var initializer = new Class1.Initializer() { };
			Initialize(initializer);
		}

		public void Initialize(Initializer initializer)
		{
			FirstName = initializer.FirstName;
			SecondName = initializer.SecondName;
			Family = initializer.Family;
			PosteCode = initializer.PosteCode;
			Age = initializer.Age;
			Address = initializer.Address;
		}

		/// <summary>
		/// Инициализатор содержит поля для заполнения свойств целевого класса. Это вложенный класс
		/// </summary>
		public class Initializer
		{
			public string FirstName;
			public string SecondName;
			public string Family;
			public string PosteCode;
			public string Age;
			public string Address;
		}

		public string FirstName { get; private set; }

		public string SecondName { get; private set; }

		public string Family { get; private set; }
		
		public string PosteCode { get; private set; }

		public string Age { get; private set; }

		public string Address { get; private set; }
	}
}
