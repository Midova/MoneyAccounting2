namespace Transaction.Service
{
	public interface IOpenProjectFileService
	{
		/// <summary>
		/// Открывает дилог выбора файла.
		/// </summary>
		/// <param name="path">Возвращает пуь к фацйлу.</param>
		/// <returns>Истина - пользовавтель выбрал файлж Лож - отмена выбора.</returns>
		bool? OpenProjectFile(out string path);
	}
}
