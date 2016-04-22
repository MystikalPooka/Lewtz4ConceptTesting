using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LewtzTesting.Visitors;

namespace LewtzTesting.Data_Structure
{
    public class MagicItem : ItemDecorator
    {
        private ItemNode _baseItem;

        private Table _tableOfAbilitiesToRoll;
        private List<Ability> _appliedAbilities;

        MagicItem(ItemNode item)
        {
            _baseItem = item;
            this.Name = item.Name;
            this.Probability = item.Probability;
            this.Cost = item.Cost;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
