using System.Collections.Generic;
using LewtzTesting.Visitors;

namespace LewtzTesting.Data_Structure
{
    public class MagicItem : ItemDecorator
    {
        private ItemNode _baseItem;
        private List<Component> _appliedAbilities;

        MagicItem(ItemNode item, Table abilityTable)
        {
            _baseItem = item;
            this.Name = item.Name;
            this.Probability = item.Probability;
            this.Cost = item.Cost;
            _appliedAbilities = new List<Component>();
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {

            return base.ToString() +" Abilities: \r\n \t"+ GetAbilityNames();
        }

        private string GetAbilityNames()
        {
            string abilities = "";
            if(_appliedAbilities.Count > 0)
            {
                foreach(Ability comp in _appliedAbilities)
                {
                    abilities += comp.Name + "\r\n";
                }
            }
            return abilities;
        }
    }
}
