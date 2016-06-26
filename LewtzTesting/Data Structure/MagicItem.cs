using System.Collections.Generic;
using LewtzTesting.Visitors;
using System.Linq;
using System;

namespace LewtzTesting.Data_Structure
{
    public class MagicItem : Item
    {
        private List<Component> appliedAbilities;
        public TableDictionary ReferenceDictionary { protected get; set; }

        private Table buildTable;

        public MagicItem(TableDictionary referenceDict)
        {
            appliedAbilities = new List<Component>();
            ReferenceDictionary = referenceDict;

            buildTable = getSortedTableFromDictionaryString("magic base");
        }

        public MagicItem(MagicItem item)
        {
            appliedAbilities = new List<Component>();
            ReferenceDictionary = item.ReferenceDictionary;

            Types |= item.Types;
            Cost = item.Cost;
            Probability = item.Probability;

            buildTable = getSortedTableFromDictionaryString("magic base");
        }

        public MagicItem()
        {
            appliedAbilities = new List<Component>();
            Types |= ItemTypes.Magic;

            buildTable = new Table("Table Not Found");
        }

        private Table getSortedTableFromDictionaryString(string tableName)
        {
            var table = ReferenceDictionary.GetTableFromString(tableName);
            table.RemoveChildrenNotMatchingTypes(Types);
            return table;
        }

        public void Build()
        {
            buildTable = getSortedTableFromDictionaryString("magic base");
            buildTable = GetBaseItemTable();
        }

        private Table GetBaseItemTable()
        {
            if (buildTable.Name != "Table Not Found" && buildTable != null)
            {
                var abilitiesVisitor = new GetLootVisitor();
                buildTable.Accept(abilitiesVisitor);

                buildTable.RollCount = 1;

                var abilityBag = abilitiesVisitor.GetLootBag();
                ItemTypes typesOfItemAbility = ItemTypes.None;
                if (abilityBag != null)
                {
                    typesOfItemAbility = abilityBag.FirstOrDefault().Types;
                }
                
                appliedAbilities.AddRange(abilityBag);
                ReferenceDictionary.GetMagicTableFromItemTypes(typesOfItemAbility);
                //find type, then return the correct table from the dictionary to roll on
            }
            return null;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override object Clone()
        {
            return new MagicItem(this);
        }

        public override string ToString()
        {
            return base.ToString() + " " + Types.ToString() + " Abilities: \r\n \t" + GetAbilityNames();
        }

        private string GetAbilityNames()
        {
            string abilities = "";
            foreach(Component comp in appliedAbilities)
            {
                abilities += comp.Name + "\r\n";
            }
            return abilities;
        }
    }
}