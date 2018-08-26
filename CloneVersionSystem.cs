using System;
using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        public Dictionary<string, CaminClone> clones = new Dictionary<string, CaminClone>()
        {
            {"1", new CaminClone()}
        };

        public string Execute(string query)
        {
            var command = query.Split(' ');
            switch (command[0])
            {
                case "learn":
                    return clones[command[1]].Learn(command[2]);
                case "rollback":
                    return clones[command[1]].Rollback();
                case "relearn":
                    return clones[command[1]].Relearn();
                case "clone":
                    return MakeClone(command[1]);
                case "check":
                    return clones[command[1]].GetLastLearnedProgram();

            }
            return null;
        }

        private string MakeClone(string stringCloneNumber)
        {
            int intCloneNumber = int.Parse(stringCloneNumber);
            intCloneNumber++;
            string stringNewCloneNumber = intCloneNumber.ToString();
            CaminClone newClone = (CaminClone)clones[stringCloneNumber].Clone();
            clones.Add(stringNewCloneNumber, newClone);
            return null;
        }

    }
    public class CaminClone : ICloneable
    {
        public CaminClone()
        {
            this.learnedPrograms = new Queue();
            this.rollBackedPrograms = new Queue();
        }
        public Queue learnedPrograms;
        public Queue rollBackedPrograms;

        public string Learn(String program)
        {
            this.learnedPrograms.Enqueue(program);
            return null;
        }

        public string Rollback()
        {
            this.rollBackedPrograms.Enqueue(this.learnedPrograms.Dequeue());
            return null;
        }

        public string Relearn()
        {
            this.learnedPrograms.Enqueue(this.rollBackedPrograms.Dequeue());
            return null;
        }

        public string GetLastLearnedProgram()
        {
            var lastProgram = learnedPrograms.GetTail();
            if (lastProgram == null) return "basic";
            return lastProgram;
        }

        public object Clone()
        {
            return new CaminClone
            {
                learnedPrograms = this.learnedPrograms,
                rollBackedPrograms = this.rollBackedPrograms
            };
        }
    }

    public class CloneProgram
    {
        public string Value { get; set; }
        public CloneProgram Prev { get; set; }
    }

    public class Queue
    {
        CloneProgram head;
        CloneProgram tail;

        public void Enqueue(string value)
        {
            if (head == null)
                tail = head = new CloneProgram { Value = value, Prev = null };
            else
            {
                var item = new CloneProgram { Value = value, Prev = null };
                item.Prev = tail;
                tail = item;
            }
        }

        public string Dequeue()
        {
            if (head == null) throw new InvalidOperationException();
            var result = tail.Value;
            tail = tail.Prev;
            if (tail == null)
                head = null;
            return result;
        }

        public string GetTail()
        {
            if (tail == null) return null;
            return tail.Value;
        }
    }

}