using System;
using System.Collections.Generic;
using LewtzTesting.Visitors;
using System.Linq;

namespace LewtzTesting.Data_Structure
{
    public class MagicItem : Item
    {
        private List<Component> appliedAbilities;
        public TableDatabase ReferenceDictionary { private get; set; }

        public MagicItem(TableDatabase referenceDict)
        {
            appliedAbilities = new List<Component>();
            ReferenceDictionary = referenceDict;
        }

        protected MagicItem(MagicItem item)
        {
            appliedAbilities = new List<Component>();
            ReferenceDictionary = item.ReferenceDictionary;

            Types |= item.Types;
            Cost = item.Cost;
            Probability = item.Probability;
        }

        public MagicItem()
        {
            appliedAbilities = new List<Component>();
            Types |= ItemTypes.Magic;
        }

        private Table getSortedTableFromDictionaryString(string tableName)
        {
            var table = ReferenceDictionary.GetTableFromString(tableName);
            table.RemoveChildrenNotMatchingTypes(Types);
            return table;
        }

        public void Build()
        {
            RollAllAbilities();
            SetItemTypesFromAppliedAbilities();
            RollSpecialAbilities();
        }

        private void RollAllAbilities()
        {
            var buildTable = getSortedTableFromDictionaryString("magic base");
            if (buildTable.Name != "Table Not Found" && buildTable != null)
            {
                var abilitiesVisitor = new GetLootVisitor();
                buildTable.Accept(abilitiesVisitor);

                buildTable.RollCount = 1;
                appliedAbilities.AddRange(abilitiesVisitor.GetLootBag());
            }
        }

        private void SetItemTypesFromAppliedAbilities()
        {
            var baseItemTypes = appliedAbilities.Last().Types;
            Types |= (baseItemTypes & ~ItemTypes.Ability);
        }

        private void RollSpecialAbilities()
        {
            var abilitiesToRoll =
                                from ability in appliedAbilities
                                where ability.Name.ToLower().Contains("special abilities")
                                select ability;

            foreach(Component ability in abilitiesToRoll)
            {
                var typeToRoll = Types & ~(ItemTypes.Magic | ItemTypes.Magic_Major | ItemTypes.Magic_Medium | ItemTypes.Magic_Minor);
                var rollTable = getSortedTableFromDictionaryString(typeToRoll.ToString().ToLower() + " special abilities");
                Console.WriteLine(typeToRoll.ToString().ToLower() + " special abilities ...: " + rollTable);

                //Special abilities are NOT loaded on startup because I have to do edge cases: armor and shield special abilitie are 
                //in ONE roll, not 2.
            }
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