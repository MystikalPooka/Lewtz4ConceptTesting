using LewtzTesting.Visitors;
using System.Collections.Generic;
using System.Linq;

namespace LewtzTesting.Data_Structure
{
    public class Table : Component
    {
        private List<Component> _children;
        public int RollCount {get; set; }

        public Table()
        {
            Name = "Not Named";
            Probability = 0;
            Book = "";
            RollCount = 1;
            _children = new List<Component>();
        }

        public Table(string name, int prob = 0, string book = "", int rollCount = 1)
        {
            Name = name;
            Probability = prob;
            Book = book;
            _children = new List<Component>();
            RollCount = rollCount;
        }

        public void Add(Component comp)
        {
            _children.Add(comp);
        }

        public void Remove(Component comp)
        {
            _children.Remove(comp);
        }

        public IList<Component> GetChildren()
        {
            return _children.AsReadOnly();
        }

        public void Sort()
        {
            _children.Sort((x, y) => x.Probability.CompareTo(y.Probability));
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return base.ToString() + " | # Entries: " + _children.Count;
        }
    }
}