using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{

	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
            //Fix me!
            int count = 1;
            double prevMax = 0;
            LinkedList<double> myList = new LinkedList<double>();
            foreach(var item in data)
            {
                myList.AddLast(item.OriginalY);
                if(count <= windowWidth)
                {
                    item.MaxY = item.OriginalY > prevMax ? (prevMax = item.OriginalY) : prevMax;
                }
                else{
                    if(item.OriginalY >= prevMax){
                        // Максимум текущего эл-та больше предыдущего
                        item.MaxY = prevMax = item.OriginalY;
                        myList.RemoveFirst();
                    }
                    else if(myList.First.Value < prevMax)
                    {
                        // Первый эл-т не является максимумом
                        item.MaxY = prevMax;
                        myList.RemoveFirst();
                    }
                    else
                    {
                        // Первый эл-т является максимумом
                        if(myList.Count > windowWidth) myList.RemoveFirst();
                        item.MaxY = prevMax = GetMaxDouble(myList);
                    }
                    while(myList.Count > 1 && myList.First.Value < prevMax)
                    {
                        myList.RemoveFirst();
                    }
                }
                yield return item;
                count++;
            }
        }
        private static double GetMaxDouble(LinkedList<double> data){
            if (data == null) return 0;
            double max = 0;
            foreach (var item in data)
            {
                if (item > max)
                {
                    max = item;
                }
            }
            return max;
        }
    }
}