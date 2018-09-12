using System;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        public Dictionary<string, CaminClone> Clones = new Dictionary<string, CaminClone>()
        {
            {"1", new CaminClone()}
        };

        public string Execute(string query)
        {
            var command = query.Split(' ');
            switch (command[0])
            {
                case "learn":
                    return Clones[command[1]].Learn(command[2]);
                case "rollback":
                    return Clones[command[1]].Rollback();
                case "relearn":
                    return Clones[command[1]].Relearn();
                case "clone":
                    return MakeClone(command[1]);
                case "check":
                    return Clones[command[1]].GetLastLearnedProgram();
            }
            return null;
        }

        private string MakeClone(string stringCloneNumber)
        {
            int intCloneNumber = Clones.Count;
            intCloneNumber++;
            string stringNewCloneNumber = intCloneNumber.ToString();

            var learnedPrograms = (CustomList)Clones[stringCloneNumber].LearnedPrograms.Clone();
            var rollBackedPrograms = (CustomList)Clones[stringCloneNumber].RollBackedPrograms.Clone();

            Clones.Add(stringNewCloneNumber, new CaminClone{ 
                LearnedPrograms = learnedPrograms, 
                RollBackedPrograms = rollBackedPrograms
            });
            return null;
        }
    }

    public class CaminClone
    {
        public CaminClone()
        {
            this.LearnedPrograms = new CustomList();
            this.RollBackedPrograms = new CustomList();
        }
        public CustomList LearnedPrograms;
        public CustomList RollBackedPrograms;

        public string Learn(String program)
        {
            this.LearnedPrograms.Add(program);
            return null;
        }

        public string Rollback()
        {
            string lastLearnedProgram = this.LearnedPrograms.GetTail();
            this.RollBackedPrograms.Add(lastLearnedProgram);
            this.LearnedPrograms.Remove(lastLearnedProgram);
            return null;
        }

        public string Relearn()
        {
            string lastRollBackedProgram = this.RollBackedPrograms.GetTail();
            this.LearnedPrograms.Add(lastRollBackedProgram);
            this.RollBackedPrograms.Remove(lastRollBackedProgram);
            return null;
        }

        public string GetLastLearnedProgram()
        {
            var lastProgram = LearnedPrograms.GetTail();
            if (lastProgram == null) return "basic";
            return lastProgram;
        }
      
    }

    public class CloneProgram
    {
        public string Value { get; set; } = null;
        public CloneProgram Next { get; set; } = null;
    }

    public class CustomList : ICloneable
    {
        CloneProgram head;
        CloneProgram tail;

        public void Add(string value)
        {
            var item = new CloneProgram { Value = value };
            if (head == null)
                tail = head = item;
            else
            {
                tail.Next = item;
                tail = item;
            }
        }

        public void Remove(string value)
        {
            CloneProgram previous = null;
            CloneProgram current = head;
            while(current != null)
            {
                if(current.Value.Equals(value))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                        {
                            previous.Next = null;
                            tail = previous;
                        }
                    }
                    else
                    {
                        head = current.Next;
                        if(head == null)
                        {
                            tail = null;
                        }
                    }
                    //break;
                }
                previous = current;
                current = current.Next;
            }
        }

        public string GetTail()
        {
            if (tail == null) return null;
            return tail.Value;
        }

        public object Clone()
        {
            return new CustomList
            {
                head = this.head,
                tail = this.tail
            };
        }
    }
}