using System;
using System.Collections.Generic;

namespace Clones
{
	public class CloneVersionSystem : ICloneVersionSystem
    {
        public Dictionary<string, Clone> clones = new Dictionary<string, Clone>()
        {
            {"1", new Clone()}
        };

		public string Execute(string query)
		{
            var command = query.Split(' ');
            switch(command[0]){
                case "learn":
                    return clones[command[1]].Learn(command[2]);
                case "check":
                    return clones[command[1]].GetLastLearnedProgram();

            }
 			return null;
		}
	}
    public class Clone
    {
        public Queue learnedPrograms;
        public Queue rollBackedPrograms;
        public string Learn(String program)
        {
            this.learnedPrograms.Enqueue(program);
            return null;
        }
        public string GetLastLearnedProgram(){
            var lastProgram = learnedPrograms.GetTail();
            if (lastProgram == null) return "basic";
            return lastProgram;
        }
    }

    public class CloneProgram
    {
        public string Value { get; set; }
        public CloneProgram Next { get; set; }
    }

    public class Queue
    {
        CloneProgram head;
        CloneProgram tail;

        public void Enqueue(string value)
        {
            if (head == null)
                tail = head = new CloneProgram { Value = value, Next = null };
            else
            {
                var item = new CloneProgram { Value = value, Next = null };
                tail.Next = item;
                tail = item;
            }
        }

        public string Dequeue()
        {
            if (head == null) throw new InvalidOperationException();
            var result = head.Value;
            head = head.Next;
            if (head == null)
                tail = null;
            return result;
        }

        public string GetTail(){
            return tail.Value;
        }
    }
}
