using LewtzTesting.Loaders;
using LewtzTesting.Visitors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LewtzTesting.Data_Structure
{
    public class Table : Component
    {
        private List<Component> _children;

        public Table()
        {
            Name = "Not Named";
            Probability = 0;
            Book = "";

            _children = new List<Component>();
        }

        public Table(string name, int prob = 0, string book = "")
        {
            Name = name;
            Probability = prob;
            Book = book;
            _children = new List<Component>();
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