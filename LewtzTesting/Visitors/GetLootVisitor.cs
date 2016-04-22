using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void Visit(Table table)
        {
            throw new NotImplementedException();
        }
    }
}
