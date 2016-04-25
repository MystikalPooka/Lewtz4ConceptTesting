using System;
using System.Collections.Generic;
using System.Linq;
using LewtzTesting.Visitors;

namespace LewtzTesting.Data_Structure
{
    public class MagicItem : ItemDecorator
    {
        private ItemNode _baseItem;
        private Table _abilityTable;
        private List<Component> _appliedAbilities;

        MagicItem(ItemNode item, Table abilityTable)
        {
            _baseItem = item;
            this.Name = item.Name;
            this.Probability = item.Probability;
            this.Cost = item.Cost;
            _abilityTable = abilityTable;
            _appliedAbilities = new List<Component>();
            Build();
        }

        public void Build()
        {
            if(_abilityTable.Name.ToLower().Contains("magic base"))
            {
                var firstRolledElement = _abilityTable.RollLoot().FirstOrDefault();
                Types = firstRolledElement.Types;
                _abilityTable = firstRolledElement as Table;
            }
            _appliedAbilities.AddRange(_abilityTable.RollLoot());
        }

        public void SetAbilityTable(Table table)
        {
            _abilityTable = table;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {

            return base.ToString() + getAbilityNames();
        }

        private string getAbilityNames()
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
