using LewtzTesting.Visitors;
using System.Collections.Generic;
using System.Linq;

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

        public void Sort()
        {
            _children.Sort((x, y) => x.Probability.CompareTo(y.Probability));
        }

        private System.Random rand = new System.Random();
        public List<Component> RollLoot()
        {
            List<Component> rolledLootList = new List<Component>();

            int maxProb = _children.Max(x => x.Probability);

            int roll = rand.Next(0, maxProb);
            foreach (Component comp in _children)
            {
                if (comp.Probability > roll)
                {
                    if(comp is Table)
                    {
                        rolledLootList.AddRange(((Table)comp).RollLoot());
                    }
                    if(comp is Item)
                    {
                        rolledLootList.Add((Item)comp);
                    }

                    if(comp is MagicItem)
                    {
                        ((MagicItem)comp).SetAbilityTable();
                        ((MagicItem)comp).Build();
                        rolledLootList.Add(comp);
                    }

                    if(comp.Name.Contains("roll again"))
                    {
                        rolledLootList.AddRange(this.RollLoot());
                        if (comp.Name.Contains("twice"))
                        {
                            rolledLootList.AddRange(this.RollLoot());
                        }
                    }
                    return rolledLootList;
                }   
            }
            return new List<Component>();
        }

        public List<Component> RollLoot(int count)
        {
            List<Component> rolledLootList = new List<Component>();
            for (int i = 0; i < count; ++i)
            {
                rolledLootList.AddRange(RollLoot());
            }
            return rolledLootList;
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