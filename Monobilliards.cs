using System;
using System.Collections.Generic;

namespace Monobilliards
{
	public class Monobilliards : IMonobilliards
	{
		public bool IsCheater(IList<int> inspectedBalls)
		{
            if (inspectedBalls.Count == 0 || (inspectedBalls.Count == 1 && inspectedBalls[0] == 1)) return false;
            Stack<int> ballsInTheHole = new Stack<int>();
            IList<int> usedBalls = new List<int>();
            for (var i = 0; i < inspectedBalls.Count; i++ )
            {
                if (ballsInTheHole.Count != 0)
                {
                    if (ballsInTheHole.Peek() > inspectedBalls[i]) return true;
                    if(ballsInTheHole.Peek() == inspectedBalls[i])
                    {
                        ballsInTheHole.Pop();
                    }
                }
                if (ballsInTheHole.Count == 0 || ballsInTheHole.Peek() < inspectedBalls[i])
                {
                    for (var j = 1; j <= inspectedBalls[i]; j++)
                    {
                        if(!usedBalls.Contains(j))
                        {
                            ballsInTheHole.Push(j);
                            usedBalls.Add(j);
                        }
                    }
                    if(ballsInTheHole.Count != 0) ballsInTheHole.Pop();
                }

            }
            return false;
		}
	}
}