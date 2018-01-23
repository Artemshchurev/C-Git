using System;

namespace Names
{
	internal static class HeatmapTask
	{

		public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
		{
			/*
			Подготовьте данные для построения карты интенсивностей, у которой по оси X — число месяца, по Y — номер месяца, 
			а интенсивность точки (она отображается цветом и размером) обозначает количество рожденных людей в это число этого месяца.
			Не учитывайте людей, родившихся первого числа любого месяца.

			В качестве подписей (label) по X используйте число месяца. 
			Поскольку данные за первые числа месяца учитывать не нужно, то начинайте подписи со второго числа.
			В качестве подписей по Y используйте номер месяца (январь — 1, февраль — 2, ...).

			Таким образом, данные для карты интенсивностей должны быть в виде двумерного массива 30 на 12 —  от 2 до 31 числа и от января до декабря.
			*/
            var minDay = int.MaxValue;
            var maxDay = int.MinValue;
            foreach (var item in names)
            {
                minDay = Math.Min(minDay, item.BirthDate.Day);
                maxDay = Math.Max(maxDay, item.BirthDate.Day);
            }
            var days = new string[maxDay - minDay];
            for (int y = 0; y < days.Length; y++)
                days[y] = (y + minDay + 1).ToString();
            var minMonth = int.MaxValue;
            var maxMonth = int.MinValue;
            foreach (var item in names)
            {
                minMonth = Math.Min(minMonth, item.BirthDate.Month);
                maxMonth = Math.Max(maxMonth, item.BirthDate.Month);
            }
            var months = new string[maxMonth - minMonth + 1];
            for (int y = 0; y < months.Length; y++)
                months[y] = (y + minMonth).ToString();
            var matrix = new double[days.Length, months.Length];
            foreach (var item in names)
            {
                if (item.BirthDate.Day != 1)
                {
                    //birthsCounts[item.BirthDate.Day - minDay]++;
                    matrix[item.BirthDate.Day - minDay - 1, item.BirthDate.Month - minMonth]++;
                }
            }
            foreach(var item in days){
                Console.Write(item + " ");
            }Console.WriteLine();
			//return new HeatmapData("Пример карты интенсивностей",
				//new double[,] { { 1, 2, 3 }, { 2, 3, 4 }, { 3, 4, 4 }, { 4, 4, 4 } }, new[] { "a", "b", "c", "d" }, new[] { "X", "Y", "Z" });
            return new HeatmapData("Пример карты интенсивностей", matrix, days, months);
		}
	}
}