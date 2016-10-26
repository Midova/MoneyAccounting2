
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
			_MoneyOperations = new ObservableCollection<MoneyOperation>();
			
			MoneyOperations = new ReadOnlyObservableCollection<MoneyOperation>(_MoneyOperations);

			_MoneyOperations.CollectionChanged += _MoneyOperations_CollectionChanged;

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
		public string NamePurse { get; private set; }

		/// <summary>
		/// Cписок операций.
		/// </summary>
		private readonly ObservableCollection<MoneyOperation> _MoneyOperations;

		/// <summary>
		/// Получает список операций.
		/// </summary>
		public ReadOnlyCollection<MoneyOperation> MoneyOperations { get; private set; }

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
			_MoneyOperations.Add(moneyOperation);
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
		public Purse LoadPurse(string path)
		{
			var purse = new Purse();

			var xmlFileSerializer = new XmlSerializer(typeof(Purse));

			/// но тут должна быть ошибка
			if (!File.Exists(path))
				return purse;

			var reader = File.OpenRead(path);
			var reSerializerFile = (Purse)xmlFileSerializer.Deserialize(reader);

			purse.NamePurse = reSerializerFile.NamePurse;
			purse._MoneyOperations.Clear();
			purse.OperationsTemplate.Clear();
						
			foreach (MoneyOperation item in reSerializerFile.MoneyOperations)
			{
				purse.AddMoneyOperation(item);
			}

			foreach (OperationTemplate item in reSerializerFile.OperationsTemplate)
			{
				purse.AddOperationTemplate(item);
			}

			reader.Close();

			return purse;
		}

		/// <summary>
		/// Сохранение кошелька в файл.
		/// </summary>
		/// <param name="path">путь к файлу</param>
		/// <param name="purse">кошелк</param>
		private void SavePurse(string path, Purse purse)
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
