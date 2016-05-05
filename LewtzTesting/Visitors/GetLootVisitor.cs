using System;
using System.Collections.Generic;
using LewtzTesting.Data_Structure;
using System.Linq;

namespace LewtzTesting.Visitors
{
    class GetLootVisitor : IVisitor
    {
        private IList<ItemNode> lootBag;
        
        public GetLootVisitor()
        {
            lootBag = new List<ItemNode>();
        }

        private Random rand = new Random();
        public void Visit(Table table)
        {
            var children = table.GetChildren();
            int maxProb = children.Max(x => x.Probability);
            int roll = rand.Next(0, maxProb);

            for(int i = 0; i < table.RollCount; ++i)
            {
                foreach (Component comp in children)
                {
                    if (comp.Probability > roll)
                    {
                        comp.Accept(this);
                        break;
                    }
                }
            }
        }

        public void Visit(Ability ability)
        {
            throw new NotImplementedException();
        }

        public void Visit(MagicItem item)
        {
            item.Build();
            lootBag.Add(item);
        }

        public void Visit(Item item)
        {
            lootBag.Add(item);
        }
    }
}
