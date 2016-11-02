
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Catel.Data;
using System;

namespace Transaction.Data
{
	/// <summary>
	/// Кошелек
	/// </summary>
	public class Purse : ObservableObject
	{
		public Purse()
		{
			NamePurse = string.Empty;

			// проблема вот тут. тебя ввело в заблуждение обертка которую мы использовали.
			// для изначального случая это очень неплохо работало
			// почему это не работает так как ты ожидаешь?
			// делов том что конструкция new ObservableCollection<MoneyOperation>(_MoneyOperations)
			// не создает как таковой обертки, она просто копирует элементы из коллекции _MoneyOperations
			// в коллекцию MoneyOperations. Так как сейчас _MoneyOperations пуста то и пуста MoneyOperations
			// тоесть по факту это абсолютно разные никак не связанные коллекции
			// все что здесь происходит это то что _MoneyOperations служит источником данных для MoneyOperations
			// и никакой речи о синхронизации тут не идет тк эти коллекции не продоставляют такой функционал
			// Объясню почему раньше работало: Дело в том что раньше тут был тип ReadonlyObservableCollection
			// или один из ему подобных. Как раз эти типы и являются обетками, на сколько я помню, кстати так же как
			// и ListCollectionView. Как сделать так что бы заработало. Нужно вспомнить что свойство может быть не авто а развернутым
			// тоесть в этом случае нужно просто развернуть св-во MoneyOperations
			// посмотреть можно как сделано свойство для уведомления об изменинях там где применияется
			// RasePropertyChanged
			MoneyOperations = new ObservableCollection<MoneyOperation>();
			//MoneyOperations = new ObservableCollection<MoneyOperation>(_MoneyOperations);

			//_MoneyOperations.CollectionChanged += _MoneyOperations_CollectionChanged;

			// А вот тут все будет работать кстати так как нет приватного поля и посути 
			// это обычно авто св-во.
			OperationsTemplate = new ObservableCollection<OperationTemplate>();
		}

		private void _MoneyOperations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			RaisePropertyChanged(nameof(Balance));
		}

		public Purse(string namePurse) : this()
		{
			NamePurse = namePurse;
		}

		/// <summary>
		/// Получает название кошелька.
		/// </summary>
		public string NamePurse { get;  set; }

		/// <summary>
		/// Cписок операций.
		/// </summary>
		public ObservableCollection<MoneyOperation> MoneyOperations { get; private set; }

		/// <summary>
		/// Получает список операций.
		/// </summary>
		//public ObservableCollection<MoneyOperation> MoneyOperations { get; private set; }

		/// <summary>
		/// Получает список шаблонов.
		/// </summary>
		public ObservableCollection<OperationTemplate> OperationsTemplate { get; private set; }

		/// <summary>
		/// Получает список категорий.
		/// </summary>
		public IReadOnlyList<string> Categorys =>
			MoneyOperations.SelectMany(operation => operation.Categorys).Distinct().ToList();

		/// <summary>
		/// Получает баланc.
		/// </summary>
		public double Balance => MoneyOperations.Select(operation => operation.Value).Sum();

		/// <summary>
		/// Получает баланс на данный момент.
		/// </summary>
		/// <returns>итоговое значение</returns>
		public double GetBalance()
		{
			double balance = 0D;

			foreach (MoneyOperation operation in MoneyOperations)
				balance += operation.Value;

			return balance;
		}
		
		/// <summary>
		/// Добавляет в список операций заданную оперцию
		/// </summary>
		/// <param name="moneyOperation">заданная операция</param>
		public void AddMoneyOperation(MoneyOperation moneyOperation)
		{
			MoneyOperations.Add(moneyOperation);
		}

		/// <summary>
		/// Добавляет в список шаблонов заданный шаблон
		/// </summary>
		/// <param name="operationTemplate">заданный шаблон</param>
		public void AddOperationTemplate(OperationTemplate operationTemplate)
		{
			OperationsTemplate.Add(operationTemplate);
		}

		/// <summary>
		/// Загрузка кошелька
		/// </summary>
		/// <param name="namePurse">у</param>
		/// <param name="path">Путь к файлу</param>
		/// <returns></returns>
		public static Purse LoadPurse(string path)
		{
			var xmlFileSerializer = new XmlSerializer(typeof(Purse));

			/// но тут должна быть ошибка
			if (!File.Exists(path))
				return null;

			Purse result = null;
			using (var reader = File.OpenRead(path))
				result = (Purse)xmlFileSerializer.Deserialize(reader);
			
			return result;
		}

		/// <summary>
		/// Сохранение кошелька в файл.
		/// </summary>
		/// <param name="path">путь к файлу</param>
		/// <param name="purse">кошелк</param>
		public void SavePurse(string path, Purse purse)
		{
			var xmlFileSerializer = new XmlSerializer(typeof(Purse));			

			if (!string.IsNullOrWhiteSpace(path))
			{
				var writer = new StreamWriter(path);
				xmlFileSerializer.Serialize(writer, purse);
				writer.Close();
			}
		}
	}
}
