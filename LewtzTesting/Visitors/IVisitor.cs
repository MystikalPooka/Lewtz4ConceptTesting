using LewtzTesting.Data_Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LewtzTesting.Visitors
{
    public interface IVisitor
    {
        void Visit(Table table);
        void Visit(Item item);
        void Visit(MagicItem item);
        void Visit(Ability ability);
    }
}
