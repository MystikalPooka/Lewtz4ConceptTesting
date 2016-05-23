using System;
using System.Collections.Generic;
using LewtzTesting.Data_Structure;
using System.Linq;

namespace LewtzTesting.Visitors
{
    class GetLootVisitor : IVisitor
    {
        private IList<Component> lootBag;
        public List<Component> GetLootBag()
        {
            return (List<Component>)lootBag;
        }

        private static Random rand = new Random();
        public GetLootVisitor()
        {
            lootBag = new List<Component>();
        }

        
        public void Visit(Table table)
        {
            var children = table.GetChildren();
            int maxProb = children.Max(x => x.Probability);
            
            for(int i = 0; i < table.RollCount; ++i)
            {
                int roll = rand.Next(0, maxProb);
                foreach (Component comp in children)
                {
                    if (comp.Probability > roll)
                    {
                        if (comp.Name.Contains("roll again")) table.RollCount++;
                        comp.Accept(this);
                        break;
                    }
                }
            }
        }

        public void Visit(Ability ability)
        {
            lootBag.Add(ability);
        }

        public void Visit(MagicItem item)
        {
            var itemToBuild = new MagicItem(item.ReferenceDictionary);
            itemToBuild.Build();
            lootBag.Add(itemToBuild);
            //copies the same exact thing
        }

        public void Visit(MundaneItem item)
        {
            lootBag.Add(item);
        }
    }
}
