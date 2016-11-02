namespace Transaction.Service
{
	public interface ISaveProjectFileService
	{
		/// <summary>
		/// Открывает дилог выбора файла.
		/// </summary>
		/// <param name="path">Возвращает путь к файлу</param>
		/// <returns>Истина - пользовавтель выбрал файл. Ложь - отмена выбора.</returns>
		bool? SaveProjectFile(out string path);
	}
}
