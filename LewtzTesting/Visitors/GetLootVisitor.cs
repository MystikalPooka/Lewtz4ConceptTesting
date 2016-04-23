using System;
using System.Collections.Generic;
using LewtzTesting.Data_Structure;

namespace LewtzTesting.Visitors
{
    class GetLootVisitor : IVisitor
    {
        private IList<Item> lootBag;

        public GetLootVisitor()
        {
            lootBag = new List<Item>();
        }

        public void Visit(Table table)
        {
            Random rand = new Random();

            var randIndex = rand.Next();
        }

        public void Visit(Ability ability)
        {
            throw new NotImplementedException();
        }

        public void Visit(MagicItem item)
        {
            throw new NotImplementedException();
        }

        public void Visit(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
